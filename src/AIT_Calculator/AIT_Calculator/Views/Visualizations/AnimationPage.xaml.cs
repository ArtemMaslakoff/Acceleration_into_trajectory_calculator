using AIT_Calculator.Models;
using AIT_Calculator.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AIT_Calculator.Views.Visualizations
{
    public partial class AnimationPage : UserControl
    {
        private CoordinatePlotter _plotter;
        private CarAnimationService _animationService;
        private bool _isAnimating;
        private bool _isSliderDragging;

        public CarDataModel CarDataModel { get; }

        public AnimationPage(CarDataModel carDataModel)
        {
            InitializeComponent();
            CarDataModel = carDataModel ?? throw new ArgumentNullException(nameof(carDataModel));

            DataContext = this;
            Loaded += AnimationPage_Loaded;
            Unloaded += AnimationPage_Unloaded;
        }

        private void AnimationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InitializeAnimation();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации анимации: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializeAnimation()
        {
            CleanupResources();

            // Инициализируем плоттер с правильными границами
            var bounds = CalculateDataBounds(CarDataModel);
            _plotter = new CoordinatePlotter(MainCanvas, bounds.xMin, bounds.xMax, bounds.yMin, bounds.yMax, true);

            _animationService = new CarAnimationService(MainCanvas, _plotter);
            _animationService.FrameUpdated += OnFrameUpdatedHandler;

            if (CarDataModel != null)
            {
                _animationService.InitializeAnimation(CarDataModel);
                ProgressSlider.Maximum = _animationService.TotalFrames - 1;
                ProgressSlider.IsEnabled = true;

                // Принудительно рисуем первый кадр
                _animationService.SetCurrentFrame(0);
            }
        }

        private (double xMin, double xMax, double yMin, double yMax) CalculateDataBounds(CarDataModel data)
        {
            if (data == null)
                return (-10, 10, -5, 5); // Значения по умолчанию

            // Инициализируем минимальные и максимальные значения
            double xMin = double.MaxValue;
            double xMax = double.MinValue;
            double yMin = double.MaxValue;
            double yMax = double.MinValue;

            // Проверяем все точки автомобиля (X1-X6 и Y1-Y6)
            for (int i = 0; i < data.X1.Count; i++)
            {
                // Точка 1
                xMin = Math.Min(xMin, data.X1[i]);
                xMax = Math.Max(xMax, data.X1[i]);
                yMin = Math.Min(yMin, data.Y1[i]);
                yMax = Math.Max(yMax, data.Y1[i]);

                // Точка 2
                xMin = Math.Min(xMin, data.X2[i]);
                xMax = Math.Max(xMax, data.X2[i]);
                yMin = Math.Min(yMin, data.Y2[i]);
                yMax = Math.Max(yMax, data.Y2[i]);

                // Точка 3
                xMin = Math.Min(xMin, data.X3[i]);
                xMax = Math.Max(xMax, data.X3[i]);
                yMin = Math.Min(yMin, data.Y3[i]);
                yMax = Math.Max(yMax, data.Y3[i]);

                // Точка 4
                xMin = Math.Min(xMin, data.X4[i]);
                xMax = Math.Max(xMax, data.X4[i]);
                yMin = Math.Min(yMin, data.Y4[i]);
                yMax = Math.Max(yMax, data.Y4[i]);

                // Точка 5
                xMin = Math.Min(xMin, data.X5[i]);
                xMax = Math.Max(xMax, data.X5[i]);
                yMin = Math.Min(yMin, data.Y5[i]);
                yMax = Math.Max(yMax, data.Y5[i]);

                // Точка 6
                xMin = Math.Min(xMin, data.X6[i]);
                xMax = Math.Max(xMax, data.X6[i]);
                yMin = Math.Min(yMin, data.Y6[i]);
                yMax = Math.Max(yMax, data.Y6[i]);
            }

            // Если данные пустые, возвращаем значения по умолчанию
            if (xMin == double.MaxValue)
                return (-10, 10, -5, 5);

            // Добавляем 10% отступ по всем сторонам
            double xPadding = (xMax - xMin) * 0.1;
            double yPadding = (yMax - yMin) * 0.1;

            return (
                xMin - xPadding,
                xMax + xPadding,
                yMin - yPadding,
                yMax + yPadding
            );
        }

        private void OnFrameUpdatedHandler(object sender, FrameUpdatedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (!_isSliderDragging)
                {
                    ProgressSlider.Value = e.FrameNumber;
                }

                FrameInfoText.Text = $"Кадр: {e.FrameNumber}";
                TimeInfoText.Text = $"Время: {e.CurrentTime:F2} сек";
            }));
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (_isAnimating || _animationService == null) return;

            try
            {
                _isAnimating = true;
                _animationService.StartAnimation();
                ProgressSlider.IsEnabled = false;
                BtnStart.IsEnabled = false;
                BtnStop.IsEnabled = true;
            }
            catch (Exception ex)
            {
                _isAnimating = false;
                MessageBox.Show($"Ошибка запуска анимации: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAnimating || _animationService == null) return;

            try
            {
                _isAnimating = false;
                _animationService.StopAnimation();
                ProgressSlider.IsEnabled = true;
                BtnStart.IsEnabled = true;
                BtnStop.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка остановки анимации: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            if (_animationService == null) return;

            try
            {
                _isAnimating = false;
                _animationService.ResetAnimation();
                FrameInfoText.Text = "Кадр: 0";
                TimeInfoText.Text = "Время: 0.00 сек";
                ProgressSlider.Value = 0;
                ProgressSlider.IsEnabled = true;
                BtnStart.IsEnabled = true;
                BtnStop.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сброса анимации: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnToggleTrajectory_Click(object sender, RoutedEventArgs e)
        {
            if (_animationService == null) return;

            try
            {
                _animationService.ToggleTrajectoryVisibility();
                ((Button)sender).Content = _animationService.IsTrajectoryVisible
                    ? "Скрыть траекторию"
                    : "Показать траекторию";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка переключения траектории: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Здесь будет логика экспорта анимации
                // Например, сохранение текущего кадра или всей анимации

                MessageBox.Show("Экспорт анимации будет реализован в будущей версии",
                              "Экспорт",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }
        private void ProgressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_animationService == null || _isAnimating)
                return;

            try
            {
                _isSliderDragging = true;
                int frame = (int)e.NewValue;
                _animationService.SetCurrentFrame(frame);
                FrameInfoText.Text = $"Кадр: {frame}";
                TimeInfoText.Text = $"Время: {frame * _animationService.FrameStep:F2} сек";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка изменения позиции: {ex.Message}");
            }
            finally
            {
                _isSliderDragging = false;
            }
        }

        private void CleanupResources()
        {
            try
            {
                if (_animationService != null)
                {
                    _animationService.FrameUpdated -= OnFrameUpdatedHandler;
                    _animationService.Dispose();
                    _animationService = null;
                }
                _plotter = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка очистки ресурсов: {ex.Message}");
            }
        }

        private void AnimationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            CleanupResources();
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_plotter != null)
            {
                _plotter.UpdateCanvasSize(e.NewSize.Width, e.NewSize.Height);

                if (_animationService != null)
                {
                    _animationService.RedrawCurrentFrame();
                }
            }
        }
    }
}