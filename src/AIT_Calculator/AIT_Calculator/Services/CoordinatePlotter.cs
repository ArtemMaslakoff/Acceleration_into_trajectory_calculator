using AIT_Calculator.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AIT_Calculator.Services
{
    public class CoordinatePlotter
    {
        private readonly Canvas _canvas;
        private double _xMin, _xMax, _yMin, _yMax;
        private double _canvasWidth, _canvasHeight;
        private double _unitScale;
        private readonly bool _keepAspectRatio;
        private double _aspectRatio;

        private readonly Brush _pointBrush = Brushes.Red;
        private readonly Brush _lineBrush = Brushes.Blue;
        private readonly double _pointSize = 5;

        private static readonly Brush[] _palette = new Brush[]
        {
            Brushes.Blue,
            Brushes.Red,
            Brushes.Green,
            Brushes.Purple,
            Brushes.Orange,
            Brushes.Brown,
            Brushes.Magenta,
            Brushes.Cyan
        };

        public CoordinatePlotter(Canvas canvas, double xMin, double xMax, double yMin, double yMax, bool keepAspectRatio = true) : this(canvas, keepAspectRatio)
        {
            Initialize(xMin, xMax, yMin, yMax);
        }
        public CoordinatePlotter(Canvas canvas, bool keepAspectRatio = true)
        {
            _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            _canvasWidth = canvas.ActualWidth;
            _canvasHeight = canvas.ActualHeight;
            _keepAspectRatio = keepAspectRatio;

            // Временные значения по умолчанию
            _xMin = -10;
            _xMax = 10;
            _yMin = -5;
            _yMax = 5;
        }

        public void Initialize(double xMin, double xMax, double yMin, double yMax)
        {
            _aspectRatio = (xMax - xMin) / (yMax - yMin);
            UpdateBounds(xMin, xMax, yMin, yMax);
        }

        public void UpdateBounds(double xMin, double xMax, double yMin, double yMax)
        {
            if (_keepAspectRatio)
            {
                // Корректируем границы для сохранения пропорций
                double currentRatio = (xMax - xMin) / (yMax - yMin);

                if (currentRatio > _aspectRatio)
                {
                    // Ширина данных больше - корректируем высоту
                    double neededHeight = (xMax - xMin) / _aspectRatio;
                    double yCenter = (yMin + yMax) / 2;
                    yMin = yCenter - neededHeight / 2;
                    yMax = yCenter + neededHeight / 2;
                }
                else
                {
                    // Высота данных больше - корректируем ширину
                    double neededWidth = (yMax - yMin) * _aspectRatio;
                    double xCenter = (xMin + xMax) / 2;
                    xMin = xCenter - neededWidth / 2;
                    xMax = xCenter + neededWidth / 2;
                }
            }

            _xMin = xMin;
            _xMax = xMax;
            _yMin = yMin;
            _yMax = yMax;

            RecalculateScale();
            DrawCoordinateAxes();
        }

        private void RecalculateScale()
        {
            double xScale = _canvasWidth / (_xMax - _xMin);
            double yScale = _canvasHeight / (_yMax - _yMin);

            _unitScale = _keepAspectRatio
                ? Math.Min(xScale, yScale)
                : Math.Min(xScale, yScale);
        }

        public void UpdateCanvasSize(double width, double height)
        {
            _canvasWidth = width;
            _canvasHeight = height;
            RecalculateScale();
            DrawCoordinateAxes();
        }

        public void DrawCoordinateAxes()
        {
            // Очищаем только элементы осей и сетки
            var elementsToRemove = _canvas.Children
                .OfType<VisualHost>()
                .Where(vh => vh.IsAxesVisual)
                .ToList();

            foreach (var element in elementsToRemove)
            {
                _canvas.Children.Remove(element);
            }

            var axisVisual = new DrawingVisual();
            using (var dc = axisVisual.RenderOpen())
            {
                // Ось X
                dc.DrawLine(new Pen(Brushes.Black, 1),
                    new Point(0, MapY(0)),
                    new Point(_canvasWidth, MapY(0)));

                // Ось Y
                dc.DrawLine(new Pen(Brushes.Black, 1),
                    new Point(MapX(0), 0),
                    new Point(MapX(0), _canvasHeight));

                // Сетка
                var gridPen = new Pen(Brushes.LightGray, 0.5);
                for (double x = _xMin; x <= _xMax; x += (_xMax - _xMin) / 10)
                {
                    dc.DrawLine(gridPen,
                        new Point(MapX(x), 0),
                        new Point(MapX(x), _canvasHeight));
                }
                for (double y = _yMin; y <= _yMax; y += (_yMax - _yMin) / 10)
                {
                    dc.DrawLine(gridPen,
                        new Point(0, MapY(y)),
                        new Point(_canvasWidth, MapY(y)));
                }
            }
            _canvas.Children.Add(new VisualHost(axisVisual));
        }

        public double MapX(double x) => (x - _xMin) * _unitScale;
        public double MapY(double y) => _canvasHeight - (y - _yMin) * _unitScale;

        public double MapXReverse(double screenX) => screenX / _unitScale + _xMin;
        public double MapYReverse(double screenY) => (_canvasHeight - screenY) / _unitScale + _yMin;

        public void DrawPoints(Point[] points, double step)
        {
            if (points == null || points.Length == 0) return;

            _canvas.Children.Clear();
            DrawCoordinateAxes();

            for (int i = 0; i < points.Length; i++)
            {
                var point = points[i];
                double canvasX = MapX(point.X);
                double canvasY = MapY(point.Y);

                var ellipse = new Ellipse
                {
                    Width = _pointSize,
                    Height = _pointSize,
                    Fill = _pointBrush,
                    Stroke = _pointBrush
                };
                Canvas.SetLeft(ellipse, canvasX - _pointSize / 2);
                Canvas.SetTop(ellipse, canvasY - _pointSize / 2);
                _canvas.Children.Add(ellipse);

                var text = new TextBlock
                {
                    Text = $"{i * step:F2}s",
                    FontSize = 10,
                    Foreground = Brushes.Black
                };
                Canvas.SetLeft(text, canvasX + _pointSize);
                Canvas.SetTop(text, canvasY - 10);
                _canvas.Children.Add(text);
            }
        }

        public void DrawPolyline(Point[] points)
        {
            if (points == null || points.Length < 2) return;

            _canvas.Children.Clear();
            DrawCoordinateAxes();

            var polyline = new Polyline
            {
                Stroke = _lineBrush,
                StrokeThickness = 2,
                Points = new PointCollection()
            };

            foreach (var point in points)
            {
                polyline.Points.Add(new Point(MapX(point.X), MapY(point.Y)));
            }

            _canvas.Children.Add(polyline);
        }

        public void DrawPolyline(ObservableCollection<TableTwoParameterData> pointsData)
        {
            if (pointsData == null || pointsData.Count < 2) return;
            DrawPolyline(pointsData.Select(p => new Point(p.Parameter1, p.Parameter2)).ToArray());
        }

        public void DrawPoints(ObservableCollection<TableTwoParameterData> pointsData, double step)
        {
            if (pointsData == null || pointsData.Count == 0) return;
            DrawPoints(pointsData.Select(p => new Point(p.Parameter1, p.Parameter2)).ToArray(), step);
        }

        public void DrawMultiplePolylines(IEnumerable<ObservableCollection<TableTwoParameterData>> polylinesData)
        {
            if (polylinesData == null || !polylinesData.Any()) return;

            _canvas.Children.Clear();
            DrawCoordinateAxes();

            int colorIndex = 0;
            foreach (var pointsData in polylinesData)
            {
                if (pointsData == null || pointsData.Count < 2) continue;

                var polyline = new Polyline
                {
                    Stroke = _palette[colorIndex % _palette.Length],
                    StrokeThickness = 2,
                    Points = new PointCollection()
                };

                foreach (var point in pointsData)
                {
                    polyline.Points.Add(new Point(MapX(point.Parameter1), MapY(point.Parameter2)));
                }

                _canvas.Children.Add(polyline);
                colorIndex++;
            }
        }

        public void Clear()
        {
            _canvas.Children.Clear();
            DrawCoordinateAxes();
        }

        public void Zoom(double factor, double centerX, double centerY)
        {
            double width = (_xMax - _xMin) / factor;
            double height = (_yMax - _yMin) / factor;

            _xMin = centerX - width / 2;
            _xMax = centerX + width / 2;
            _yMin = centerY - height / 2;
            _yMax = centerY + height / 2;

            RecalculateScale();
            DrawCoordinateAxes();
        }
    }
}