using AIT_Calculator.Models;
using AIT_Calculator.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AIT_Calculator.Views.Visualizations
{
    public partial class Points1And2TrajectoriesPage : UserControl
    {
        public CarDataModel CarDataModel { get; }

        private CoordinatePlotter _plotter1;
        private CoordinatePlotter _plotter2;

        public Points1And2TrajectoriesPage(CarDataModel carDataModel)
        {
            CarDataModel = carDataModel ?? throw new ArgumentNullException(nameof(carDataModel));
            InitializeComponent();
            DataContext = CarDataModel;

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            InitializePlotters();
            CarDataModel.PropertyChanged += CarDataModel_PropertyChanged;
        }

        private void InitializePlotters()
        {
            if (CarDataModel.Point1PositionData == null || CarDataModel.Point2PositionData == null)
                return;

            // Вычисляем общие границы для обоих графиков
            var allX = CarDataModel.Point1PositionData.Select(p => p.Parameter1)
                        .Concat(CarDataModel.Point2PositionData.Select(p => p.Parameter1));
            var allY = CarDataModel.Point1PositionData.Select(p => p.Parameter2)
                        .Concat(CarDataModel.Point2PositionData.Select(p => p.Parameter2));

            if (!allX.Any() || !allY.Any())
                return;

            double xMin = allX.Min();
            double xMax = allX.Max();
            double yMin = allY.Min();
            double yMax = allY.Max();

            // Добавляем 10% padding
            double xPadding = (xMax - xMin) * 0.1;
            double yPadding = (yMax - yMin) * 0.1;

            xMin -= xPadding;
            xMax += xPadding;
            yMin -= yPadding;
            yMax += yPadding; 

            // Инициализируем плоттеры
            _plotter1 = new CoordinatePlotter(Point1Canvas, xMin, xMax, yMin, yMax, true);
            _plotter2 = new CoordinatePlotter(Point2Canvas, xMin, xMax, yMin, yMax, true);

            UpdatePlots();
        }

        private void CarDataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CarDataModel.Point1PositionData) ||
                e.PropertyName == nameof(CarDataModel.Point2PositionData))
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    InitializePlotters();
                }));
            }
        }

        public void UpdatePlots()
        {
            if (_plotter1 == null || _plotter2 == null ||
                CarDataModel.Point1PositionData == null ||
                CarDataModel.Point2PositionData == null)
                return;

            _plotter1.DrawPolyline(CarDataModel.Point1PositionData);
            _plotter2.DrawPolyline(CarDataModel.Point2PositionData);
        }

        public void ClearPlots()
        {
            _plotter1?.Clear();
            _plotter2?.Clear();
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas == null) return;

            if (canvas == Point1Canvas && _plotter1 != null)
            {
                _plotter1.UpdateCanvasSize(e.NewSize.Width, e.NewSize.Height);
                _plotter1.DrawPolyline(CarDataModel.Point1PositionData);
            }
            else if (canvas == Point2Canvas && _plotter2 != null)
            {
                _plotter2.UpdateCanvasSize(e.NewSize.Width, e.NewSize.Height);
                _plotter2.DrawPolyline(CarDataModel.Point2PositionData);
            }
        }
    }
}