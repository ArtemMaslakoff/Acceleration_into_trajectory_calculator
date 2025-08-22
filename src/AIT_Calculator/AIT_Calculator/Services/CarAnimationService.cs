using AIT_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AIT_Calculator.Services
{
    public class CarAnimationService : IDisposable
    {
        private readonly Canvas _canvas;
        private readonly CoordinatePlotter _plotter;
        private DispatcherTimer _animationTimer;
        private int _currentFrame;
        private List<Point[]> _frames;
        private const double FrameRate = 60;
        private bool _isDisposed = false;

        // Кэш для визуальных элементов
        private readonly Dictionary<int, DrawingVisual> _visualCache = new Dictionary<int, DrawingVisual>();
        private VisualHost _currentVisualHost;
        private TextBlock _frameTextBlock;

        // Траектория движения
        private PathGeometry _trajectoryGeometry;
        private Path _trajectoryPath;
        public bool IsTrajectoryVisible { get; private set; } = true;

        // Тайминг анимации
        private double _frameStep = 0.01;
        private DateTime _lastFrameTime;

        public double FrameStep => _frameStep;
        public int TotalFrames => _frames?.Count ?? 0;

        public event EventHandler<FrameUpdatedEventArgs> FrameUpdated;

        public CarAnimationService(Canvas canvas, CoordinatePlotter plotter)
        {
            _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            _plotter = plotter ?? throw new ArgumentNullException(nameof(plotter));

            InitializeComponents();
            SetupTimer();
            InitializeTrajectory();
        }

        private void InitializeComponents()
        {
            _frameTextBlock = new TextBlock
            {
                Foreground = Brushes.Black,
                Background = Brushes.White,
                FontSize = 12,
                Margin = new Thickness(10)
            };
            Canvas.SetLeft(_frameTextBlock, 10);
            Canvas.SetTop(_frameTextBlock, 10);
        }

        private void SetupTimer()
        {
            _animationTimer = new DispatcherTimer(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromSeconds(1 / FrameRate)
            };
            _animationTimer.Tick += AnimationTick;
        }

        private void InitializeTrajectory()
        {
            _trajectoryPath = new Path
            {
                Stroke = Brushes.Green,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection { 5, 2 },
                Visibility = IsTrajectoryVisible ? Visibility.Visible : Visibility.Collapsed
            };
        }

        public void InitializeAnimation(CarDataModel carData)
        {
            if (carData == null || carData.X1.Count == 0) return;

            ClearAnimation();

            // Устанавливаем шаг времени
            _frameStep = carData.Step;

            // Подготавливаем кадры анимации
            _frames = PrepareFrames(carData);
            _currentFrame = 0;

            // Рассчитываем границы для масштабирования
            var bounds = CalculateBounds(carData);
            _plotter.UpdateBounds(bounds.minX, bounds.maxX, bounds.minY, bounds.maxY);

            // Предварительно рендерим кадры
            PreRenderFrames();

            // Строим траекторию
            BuildTrajectory(carData);
        }

        private List<Point[]> PrepareFrames(CarDataModel carData)
        {
            var frames = new List<Point[]>();
            int frameCount = GetMinFrameCount(carData);

            for (int i = 0; i < frameCount; i++)
            {
                frames.Add(new[]
                {
                    new Point(carData.X1[i], carData.Y1[i]), // Точка 1
                    new Point(carData.X2[i], carData.Y2[i]), // Точка 2
                    new Point(carData.X3[i], carData.Y3[i]), // Точка 3
                    new Point(carData.X4[i], carData.Y4[i]), // Точка 4
                    new Point(carData.X5[i], carData.Y5[i]), // Точка 5
                    new Point(carData.X6[i], carData.Y6[i])  // Точка 6
                });
            }

            return frames;
        }

        private int GetMinFrameCount(CarDataModel carData)
        {
            return Math.Min(
                carData.X1.Count,
                Math.Min(carData.X2.Count,
                Math.Min(carData.X3.Count,
                Math.Min(carData.X4.Count,
                Math.Min(carData.X5.Count, carData.X6.Count))))
            );
        }

        private (double minX, double maxX, double minY, double maxY) CalculateBounds(CarDataModel carData)
        {
            double minX = double.MaxValue, maxX = double.MinValue;
            double minY = double.MaxValue, maxY = double.MinValue;

            // Анализируем все точки автомобиля
            for (int i = 0; i < carData.X1.Count; i++)
            {
                minX = Math.Min(minX, carData.X1[i]);
                minX = Math.Min(minX, carData.X2[i]);
                minX = Math.Min(minX, carData.X3[i]);
                minX = Math.Min(minX, carData.X4[i]);
                minX = Math.Min(minX, carData.X5[i]);
                minX = Math.Min(minX, carData.X6[i]);

                maxX = Math.Max(maxX, carData.X1[i]);
                maxX = Math.Max(maxX, carData.X2[i]);
                maxX = Math.Max(maxX, carData.X3[i]);
                maxX = Math.Max(maxX, carData.X4[i]);
                maxX = Math.Max(maxX, carData.X5[i]);
                maxX = Math.Max(maxX, carData.X6[i]);

                minY = Math.Min(minY, carData.Y1[i]);
                minY = Math.Min(minY, carData.Y2[i]);
                minY = Math.Min(minY, carData.Y3[i]);
                minY = Math.Min(minY, carData.Y4[i]);
                minY = Math.Min(minY, carData.Y5[i]);
                minY = Math.Min(minY, carData.Y6[i]);

                maxY = Math.Max(maxY, carData.Y1[i]);
                maxY = Math.Max(maxY, carData.Y2[i]);
                maxY = Math.Max(maxY, carData.Y3[i]);
                maxY = Math.Max(maxY, carData.Y4[i]);
                maxY = Math.Max(maxY, carData.Y5[i]);
                maxY = Math.Max(maxY, carData.Y6[i]);
            }

            // Добавляем 10% отступы
            double xPadding = (maxX - minX) * 0.1;
            double yPadding = (maxY - minY) * 0.1;

            return (minX - xPadding, maxX + xPadding, minY - yPadding, maxY + yPadding);
        }

        private void PreRenderFrames()
        {
            _visualCache.Clear();

            for (int i = 0; i < _frames.Count; i++)
            {
                _visualCache[i] = CreateFrameVisual(_frames[i]);
            }
        }

        private DrawingVisual CreateFrameVisual(Point[] points)
        {
            var visual = new DrawingVisual();
            using (var dc = visual.RenderOpen())
            {
                // Соединения линий автомобиля
                var connections = new[]
                {
                    new { From = 4, To = 5 }, // 5-6
                    new { From = 4, To = 3 }, // 5-4
                    new { From = 3, To = 2 }, // 4-3
                    new { From = 2, To = 5 }, // 3-6
                    new { From = 0, To = 1 }  // 1-2
                };

                // Рисуем соединения
                foreach (var conn in connections)
                {
                    dc.DrawLine(
                        new Pen(Brushes.Blue, 2),
                        new Point(_plotter.MapX(points[conn.From].X), _plotter.MapY(points[conn.From].Y)),
                        new Point(_plotter.MapX(points[conn.To].X), _plotter.MapY(points[conn.To].Y)));
                }

                // Рисуем точки автомобиля
                for (int i = 0; i < points.Length; i++)
                {
                    dc.DrawEllipse(
                        Brushes.Red,
                        new Pen(Brushes.Black, 1),
                        new Point(_plotter.MapX(points[i].X), _plotter.MapY(points[i].Y)),
                        3, 3);
                }
            }
            return visual;
        }

        private void BuildTrajectory(CarDataModel carData)
        {
            if (carData.X1.Count == 0) return;

            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure
            {
                StartPoint = new Point(_plotter.MapX(carData.X1[0]), _plotter.MapY(carData.Y1[0])),
                IsClosed = false
            };

            // Упрощаем траекторию (каждые 5 точек для производительности)
            for (int i = 1; i < carData.X1.Count; i += 5)
            {
                pathFigure.Segments.Add(new LineSegment(
                    new Point(_plotter.MapX(carData.X1[i]), _plotter.MapY(carData.Y1[i])),
                    true));
            }

            pathGeometry.Figures.Add(pathFigure);
            _trajectoryGeometry = pathGeometry;
            _trajectoryPath.Data = pathGeometry;
        }

        public void StartAnimation()
        {
            if (_frames == null || _frames.Count == 0) return;

            // Очищаем только элементы анимации, но оставляем оси координат
            ClearAnimationElements();

            _lastFrameTime = DateTime.Now;
            _animationTimer.Start();
        }

        private void ClearAnimationElements()
        {
            // Удаляем только элементы анимации, но оставляем оси и текст
            var elementsToRemove = _canvas.Children.OfType<VisualHost>().ToList();
            foreach (var element in elementsToRemove)
            {
                _canvas.Children.Remove(element);
                element.Dispose();
            }

            if (_trajectoryPath != null && _canvas.Children.Contains(_trajectoryPath))
            {
                _canvas.Children.Remove(_trajectoryPath);
            }
        }

        public void StopAnimation()
        {
            _animationTimer.Stop();
        }

        public void ResetAnimation()
        {
            _animationTimer.Stop();
            _currentFrame = 0;
            DrawFrame(_frames[_currentFrame]);
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            // Проверяем, прошло ли достаточно времени для следующего кадра
            if ((DateTime.Now - _lastFrameTime).TotalSeconds < _frameStep)
                return;

            _lastFrameTime = DateTime.Now;

            if (_currentFrame >= _frames.Count - 1)
            {
                StopAnimation();
                return;
            }

            _currentFrame++;
            DrawFrame(_frames[_currentFrame]);

            FrameUpdated?.Invoke(this, new FrameUpdatedEventArgs
            {
                FrameNumber = _currentFrame,
                CurrentTime = _currentFrame * _frameStep
            });
        }

        private void DrawFrame(Point[] points)
        {
            // Не очищаем canvas полностью, чтобы сохранить оси координат
            ClearAnimationElements();

            // Рисуем траекторию (если видима)
            if (IsTrajectoryVisible && _trajectoryPath != null)
            {
                _canvas.Children.Add(_trajectoryPath);
            }

            // Рисуем текущий кадр автомобиля
            if (_visualCache.TryGetValue(_currentFrame, out var visual))
            {
                _currentVisualHost?.Dispose();
                _currentVisualHost = new VisualHost(visual);
                _canvas.Children.Add(_currentVisualHost);
            }

            // Обновляем текст кадра
            _frameTextBlock.Text = $"Кадр: {_currentFrame}";
            if (!_canvas.Children.Contains(_frameTextBlock))
            {
                Canvas.SetLeft(_frameTextBlock, 10);
                Canvas.SetTop(_frameTextBlock, 10);
                _canvas.Children.Add(_frameTextBlock);
            }
        }

        public void RedrawCurrentFrame()
        {
            if (_currentFrame >= 0 && _currentFrame < _frames.Count)
            {
                DrawFrame(_frames[_currentFrame]);
            }
        }

        public void ToggleTrajectoryVisibility()
        {
            IsTrajectoryVisible = !IsTrajectoryVisible;
            _trajectoryPath.Visibility = IsTrajectoryVisible
                ? Visibility.Visible
                : Visibility.Collapsed;

            RedrawCurrentFrame();
        }

        private void ClearAnimation()
        {
            StopAnimation();
            ClearVisuals();
            _frames = null;
            _currentFrame = 0;
        }

        private void ClearVisuals()
        {
            _canvas.Children.Clear();
            _currentVisualHost?.Dispose();
            _currentVisualHost = null;
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            ClearAnimation();
            _animationTimer.Tick -= AnimationTick;
            _animationTimer = null;
            _visualCache.Clear();

            _isDisposed = true;
        }

        public void SetCurrentFrame(int frameNumber)
        {
            if (frameNumber < 0 || frameNumber >= _frames.Count)
                return;

            _currentFrame = frameNumber;
            DrawFrame(_frames[_currentFrame]);
        }
    }

    public class FrameUpdatedEventArgs : EventArgs
    {
        public int FrameNumber { get; set; }
        public double CurrentTime { get; set; }
    }

    public class VisualHost : FrameworkElement
    {
        public bool IsAxesVisual { get; set; }
        private readonly Visual _visual;

        public VisualHost(Visual visual)
        {
            _visual = visual;
            AddVisualChild(_visual);
        }

        protected override int VisualChildrenCount => 1;
        protected override Visual GetVisualChild(int index) => _visual;

        public void Dispose()
        {
            if (_visual != null && VisualTreeHelper.GetParent(_visual) != null)
            {
                RemoveVisualChild(_visual);
            }
        }
    }
}