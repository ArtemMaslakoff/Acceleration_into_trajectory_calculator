using AIT_Calculator.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows.Controls.Primitives;

namespace AIT_Calculator.Models
{
    /// <summary>
    /// Модель данных автомобиля для расчетов
    /// </summary>
    public class CarDataModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// L
        /// </summary>
        private string _lText =  "0";
        [JsonIgnore]
        public string LText
        {
            get => _lText;
            set
            {
                if (value == _lText) return;

                if (string.IsNullOrEmpty(value) || value == "-")
                {
                    value = "0";
                }

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= 0)
                {
                    _lText = value;
                    L = result;
                    ClearErrors(nameof(LText));
                }
                else
                {
                    SetError(nameof(LText), "Введите неотрицательное число");
                }
                OnPropertyChanged();
            }
        }
        private double _l;
        public double L
        {
            get => _l;
            set
            {
                if (value < 0) value = 0;
                if (Math.Abs(_l - value) < double.Epsilon) return;

                _l = value;
                _lText = value.ToString(CultureInfo.InvariantCulture);
                RefreshParameters();
                OnPropertyChanged();
                OnPropertyChanged(nameof(LText));
            }
        }
        ///
        
        /// <summary>
        /// A
        /// </summary>
        private string _aText = "0";
        [JsonIgnore]
        public string AText
        {
            get => _aText;
            set
            {
                if (value == _aText) return;

                if (string.IsNullOrEmpty(value) || value == "-")
                {
                    value = "0";
                }

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= 0)
                {
                    _aText = value;
                    A = result;
                    ClearErrors(nameof(AText));
                }
                else
                {
                    SetError(nameof(AText), "Введите неотрицательное число");
                }
                OnPropertyChanged();
            }
        }
        private double _a;
        public double A
        {
            get => _a;
            set
            {
                if (value < 0) value = 0;
                if (Math.Abs(_a - value) < double.Epsilon) return;

                _a = value;
                _aText = value.ToString(CultureInfo.InvariantCulture);
                RefreshParameters();
                OnPropertyChanged();
                OnPropertyChanged(nameof(AText));
            }
        }
        ///

        /// <summary>
        /// B
        /// </summary>
        private string _bText = "0";
        [JsonIgnore]
        public string BText
        {
            get => _bText;
            set
            {
                if (value == _bText) return;

                if (string.IsNullOrEmpty(value) || value == "-")
                {
                    value = "0";
                }

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= 0)
                {
                    _bText = value;
                    B = result;
                    ClearErrors(nameof(BText));
                }
                else
                {
                    SetError(nameof(BText), "Введите неотрицательное число");
                }
                OnPropertyChanged();
            }
        }
        private double _b;
        public double B
        {
            get => _b;
            set
            {
                if (value < 0) value = 0;
                if (Math.Abs(_b - value) < double.Epsilon) return;

                _b = value;
                _bText = value.ToString(CultureInfo.InvariantCulture);
                RefreshParameters();
                OnPropertyChanged();
                OnPropertyChanged(nameof(BText));
            }
        }
        ///

        /// <summary>
        /// H
        /// </summary>
        private string _hText = "0";
        [JsonIgnore]
        public string HText
        {
            get => _hText;
            set
            {
                if (value == _hText) return;

                if (string.IsNullOrEmpty(value) || value == "-")
                {
                    value = "0";
                }

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= 0)
                {
                    _hText = value;
                    H = result;
                    ClearErrors(nameof(HText));
                }
                else
                {
                    SetError(nameof(HText), "Введите неотрицательное число");
                }
                OnPropertyChanged();
            }
        }
        private double _h;
        public double H
        {
            get => _h;
            set
            {
                if (value < 0) value = 0;
                if (Math.Abs(_h - value) < double.Epsilon) return;

                _h = value;
                _hText = value.ToString(CultureInfo.InvariantCulture);
                RefreshParameters();
                OnPropertyChanged();
                OnPropertyChanged(nameof(HText));
            }
        }
        ///

        /// <summary>
        /// Y
        /// </summary>
        private string _yText = "0";
        [JsonIgnore]
        public string YText
        {
            get => _yText;
            set
            {
                if (value == _yText) return;

                if (string.IsNullOrEmpty(value) || value == "-")
                {
                    value = "0";
                }

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= 0)
                {
                    _yText = value;
                    Y = result;
                    ClearErrors(nameof(YText));
                }
                else
                {
                    SetError(nameof(YText), "Введите неотрицательное число");
                }
                OnPropertyChanged();
            }
        }
        private double _y;
        public double Y
        {
            get => _y;
            set
            {
                if (value < 0) value = 0;
                if (Math.Abs(_y - value) < double.Epsilon) return;

                _y = value;
                _yText = value.ToString(CultureInfo.InvariantCulture);
                RefreshParameters();
                OnPropertyChanged();
                OnPropertyChanged(nameof(YText));
            }
        }
        ///

        /// <summary>
        /// V
        /// </summary>
        private string _vText = "0";
        [JsonIgnore]
        public string VText
        {
            get => _vText;
            set
            {
                if (value == _vText) return;

                if (string.IsNullOrEmpty(value) || value == "-")
                {
                    value = "0";
                }

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= 0)
                {
                    _vText = value;
                    V = result;
                    ClearErrors(nameof(VText));
                }
                else
                {
                    SetError(nameof(VText), "Введите неотрицательное число");
                }
                OnPropertyChanged();
            }
        }
        private double _v;
        public double V
        {
            get => _v;
            set
            {
                if (value < 0) value = 0;
                if (Math.Abs(_v - value) < double.Epsilon) return;

                _v = value;
                _vText = value.ToString(CultureInfo.InvariantCulture);
                RefreshParameters();
                OnPropertyChanged();
                OnPropertyChanged(nameof(VText));
            }
        }

        private double _alfaStart;
        private double _bettaStart;

        private double _vx1Start;
        private double _vy1Start;
        private double _vx2Start;
        private double _vy2Start;

        private double _x1Start;
        private double _y1Start;
        private double _x2Start;
        private double _y2Start;

        private string _stepText = "0.01";
        [JsonIgnore]
        public string StepText
        {
            get => _stepText;
            set
            {
                if (value == _stepText) return;

                // Пустая строка или минус обрабатываются в TextChanged
                if (string.IsNullOrEmpty(value) || value == "-")
                {
                    value = "0";
                }

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= 0)
                {
                    _stepText = value;
                    Step = result;
                    ClearErrors(nameof(StepText));
                }
                else
                {
                    SetError(nameof(StepText), "Введите неотрицательное число");
                }
                OnPropertyChanged();
            }
        }

        private double _step = 0.01;
        public double Step
        {
            get => _step;
            set
            {
                // Разрешаем нулевое значение
                if (value < 0) value = 0;

                if (Math.Abs(_step - value) < double.Epsilon) return;

                _step = value;
                _stepText = value.ToString(CultureInfo.InvariantCulture);
                UpdateData();
                OnPropertyChanged();
                OnPropertyChanged(nameof(StepText));
            }
        }

        public double AlfaStart
        {
            get => _alfaStart;
            set
            {
                if (_alfaStart == value) return;
                _alfaStart = value;
                OnPropertyChanged();
            }
        }
        public double BettaStart
        {
            get => _bettaStart;
            set
            {
                if (_bettaStart == value) return;
                _bettaStart = value;
                OnPropertyChanged();
            }
        }
        
        public double VX1Start
        {
            get => _vx1Start;
            set
            {
                if (_vx1Start == value) return;
                _vx1Start = value;
                OnPropertyChanged();
            }
        }
        public double VY1Start
        {
            get => _vy1Start;
            set
            {
                if (_vy1Start == value) return;
                _vy1Start = value;
                OnPropertyChanged();
            }
        }
        public double VX2Start
        {
            get => _vx2Start;
            set
            {
                if (_vx2Start == value) return;
                _vx2Start = value;
                OnPropertyChanged();
            }
        }
        public double VY2Start
        {
            get => _vy2Start;
            set
            {
                if (_vy2Start == value) return;
                _vy2Start = value;
                OnPropertyChanged();
            }
        }

        public double X1Start
        {
            get => _x1Start;
            set
            {
                if (_x1Start == value) return;
                _x1Start = value;
                OnPropertyChanged();
            }
        }
        public double Y1Start
        {
            get => _y1Start;
            set
            {
                if (_y1Start == value) return;
                _y1Start = value;
                OnPropertyChanged();
            }
        }
        public double X2Start
        {
            get => _x2Start;
            set
            {
                if (_x2Start == value) return;
                _x2Start = value;
                OnPropertyChanged();
            }
        }
        public double Y2Start
        {
            get => _y2Start;
            set
            {
                if (_y2Start == value) return;
                _y2Start = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _ax1;
        public ObservableCollection<double> Ax1
        {
            get => _ax1;
            set
            {
                _ax1 = value;
                if (value == null) _ax1 = [];
                UpdatePoint1Data();
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<double> _ay1; 
        public ObservableCollection<double> Ay1
        {
            get => _ay1;
            set
            {
                _ay1 = value;
                if (value == null) _ay1 = [];
                UpdatePoint1Data();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _ax2;
        public ObservableCollection<double> Ax2
        {
            get => _ax2;
            set
            {
                _ax2 = value;
                if (value == null) _ax2 = [];
                UpdatePoint2Data();
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<double> _ay2; 
        public ObservableCollection<double> Ay2
        {
            get => _ay2;
            set
            {
                _ay2 = value;
                if (value == null) _ay2 = [];
                UpdatePoint2Data();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _vx1;
        public ObservableCollection<double> Vx1
        {
            get => _vx1;
            set
            {
                _vx1 = value;
                if (value == null) _vx1 = [];
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<double> _vy1; 
        public ObservableCollection<double> Vy1
        {
            get => _vy1;
            set
            {
                _vy1 = value;
                if (value == null) _vy1 = [];
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _vx2;
        public ObservableCollection<double> Vx2
        {
            get => _vx2;
            set
            {
                _vx2 = value;
                if (value == null) _vx2 = [];
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _vy2; 
        public ObservableCollection<double> Vy2
        {
            get => _vy2;
            set
            {
                _vy2 = value;
                if (value == null) _vy2 = [];
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _x1;
        public ObservableCollection<double> X1
        {
            get => _x1;
            set
            {
                _x1 = value;
                if (value == null) _x1 = [];
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<double> _y1;
        public ObservableCollection<double> Y1
        {
            get => _y1;
            set
            {
                _y1 = value;
                if (value == null) _y1 = [];
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _x2;
        public ObservableCollection<double> X2
        {
            get => _x2;
            set
            {
                _x2 = value;
                if (value == null) _x2 = [];
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _y2;
        public ObservableCollection<double> Y2
        {
            get => _y2;
            set
            {
                _y2 = value;
                if (value == null) _y2 = [];
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _angle;
        public ObservableCollection<double> Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                if (value == null) _angle = [];
                OnPropertyChanged();
            }
        }

        // Точка 3
        private ObservableCollection<double> _x3;
        public ObservableCollection<double> X3
        {
            get => _x3;
            set
            {
                _x3 = value;
                if (value == null) _x3 = [];
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<double> _y3;
        public ObservableCollection<double> Y3
        {
            get => _y3;
            set
            {
                _y3 = value;
                if (value == null) _y3 = [];
                OnPropertyChanged();
            }
        }
        //

        // Точка 4
        private ObservableCollection<double> _x4;
        public ObservableCollection<double> X4
        {
            get => _x4;
            set
            {
                _x4 = value;
                if (value == null) _x4 = [];
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<double> _y4;
        public ObservableCollection<double> Y4
        {
            get => _y4;
            set
            {
                _y4 = value;
                if (value == null) _y4 = [];
                OnPropertyChanged();
            }
        }
        //

        // Точка 5
        private ObservableCollection<double> _x5;
        public ObservableCollection<double> X5
        {
            get => _x5;
            set
            {
                _x5 = value;
                if (value == null) _x5 = [];
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<double> _y5;
        public ObservableCollection<double> Y5
        {
            get => _y5;
            set
            {
                _y5 = value;
                if (value == null) _y5 = [];
                OnPropertyChanged();
            }
        }
        //

        // Точка 6
        private ObservableCollection<double> _x6;
        public ObservableCollection<double> X6
        {
            get => _x6;
            set
            {
                _x6 = value;
                if (value == null) _x6 = [];
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<double> _y6;
        public ObservableCollection<double> Y6
        {
            get => _y6;
            set
            {
                _y6 = value;
                if (value == null) _y6 = [];
                OnPropertyChanged();
            }
        }
        //

        private ObservableCollection<TableTwoParameterData> _inputAcceleration1Data = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> InputAcceleration1Data
        {
            get => _inputAcceleration1Data;
            set
            {
                _inputAcceleration1Data = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TableTwoParameterData> _inputAcceleration2Data = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> InputAcceleration2Data
        {
            get => _inputAcceleration2Data;
            set
            {
                _inputAcceleration2Data = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<TableTwoParameterData> _point1VelocityData = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> Point1VelocityData
        {
            get => _point1VelocityData;
            set
            {
                _point1VelocityData = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<TableTwoParameterData> _point2VelocityData = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> Point2VelocityData
        {
            get => _point2VelocityData;
            set
            {
                _point2VelocityData = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TableTwoParameterData> _point1PositionData = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> Point1PositionData
        {
            get => _point1PositionData;
            set
            {
                _point1PositionData = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TableTwoParameterData> _point2PositionData = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> Point2PositionData
        {
            get => _point2PositionData;
            set
            {
                _point2PositionData = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TableTwoParameterData> _point3PositionData = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> Point3PositionData
        {
            get => _point3PositionData;
            set
            {
                _point3PositionData = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TableTwoParameterData> _point4PositionData = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> Point4PositionData
        {
            get => _point4PositionData;
            set
            {
                _point4PositionData = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TableTwoParameterData> _point5PositionData = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> Point5PositionData
        {
            get => _point5PositionData;
            set
            {
                _point5PositionData = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TableTwoParameterData> _point6PositionData = new ObservableCollection<TableTwoParameterData>();
        public ObservableCollection<TableTwoParameterData> Point6PositionData
        {
            get => _point6PositionData;
            set
            {
                _point6PositionData = value ?? new ObservableCollection<TableTwoParameterData>();
                OnPropertyChanged();
            }
        }

        public CarDataModel()
        {
            L = 0;
            A = 0;
            B = 0;
            H = 0;
            Y = 0;
            V = 0;

            Step = 0.01;

            _ax1 = [];
            _ay1 = [];
            _ax2 = [];
            _ay2 = [];

            _inputAcceleration1Data = [];
            _inputAcceleration2Data = [];

            _vx1 = [];
            _vy1 = [];
            _vx2 = [];
            _vy2 = [];

            _point1VelocityData = [];
            _point2VelocityData = [];

            _x1 = [];
            _y1 = [];
            _x2 = [];
            _y2 = [];

            _point1PositionData = [];
            _point2PositionData = [];

            _angle = [];

            _x3 = [];
            _y3 = [];

            _x4 = [];
            _y4 = [];

            _x5 = [];
            _y5 = [];

            _x6 = [];
            _y6 = [];

            RefreshParameters();
        }

        private void RefreshParameters()
        {
            RefreshAlfaStart();
            RefreshBettaStart();

            RefreshVX1Start();
            RefreshVY1Start();
            RefreshVX2Start();
            RefreshVY2Start();

            RefreshX1Start();
            RefreshY1Start();
            RefreshX2Start();
            RefreshY2Start();

            RefreshPoint1VelocityData();
            RefreshPoint2VelocityData();

            RefreshPoint1PositionData();
            RefreshPoint2PositionData();

            RefreshAngleData();

            RefreshPoint3PositionData();
            RefreshPoint4PositionData();
            RefreshPoint5PositionData();
            RefreshPoint6PositionData();

            UpdateData();
        }

        private void RefreshAlfaStart()
        {
            AlfaStart = Math.Atan( ( H / 2 ) / ( L + B ) ) * ( 180.0 / Math.PI );
        }
        private void RefreshBettaStart()
        {
            BettaStart = Math.Atan( ( H / 2 ) / A ) * ( 180.0 / Math.PI );
        }

        private void RefreshVX1Start()
        {
            VX1Start = MetricConvertingService.ConvertKmPerHourToMetersPerSecond(V) * Math.Cos(Y / 180 * Math.PI);
        }

        private void RefreshVY1Start()
        {
            VY1Start = MetricConvertingService.ConvertKmPerHourToMetersPerSecond(V) * Math.Sin(-1 * Y / 180 * Math.PI);
        }

        private void RefreshVX2Start()
        {
            VX2Start = MetricConvertingService.ConvertKmPerHourToMetersPerSecond(V) * Math.Cos(Y / 180 * Math.PI);
        }

        private void RefreshVY2Start()
        {
            VY2Start = MetricConvertingService.ConvertKmPerHourToMetersPerSecond(V) * Math.Sin(-1 * Y / 180 * Math.PI);
        }

        private void RefreshX1Start()
        {
            X1Start = 0;
        }
        private void RefreshY1Start()
        {
            Y1Start = ( L + B ) * Math.Sin( Y / 180 * Math.PI ) + H / 2 * Math.Cos( Y / 180 * Math.PI );
        }
        private void RefreshX2Start()
        {
            X2Start = L * Math.Cos( Y / 180 * Math.PI );
        }
        private void RefreshY2Start()
        {
            Y2Start = B * Math.Sin( Y / 180 * Math.PI ) + H / 2 * Math.Cos( Y / 180 * Math.PI );
        }

        private void RefreshPoint1VelocityData()
        {
            if (Ax1 != null && Ay1 != null)
            {
                // Конвертируем ускорения из мм/с² в м/с² перед интегрированием
                var ax1Meters = Ax1.Select(a => MetricConvertingService.ConvertMmPerSecSquaredToMetersPerSecSquared(a)).ToArray();
                var ay1Meters = Ay1.Select(a => MetricConvertingService.ConvertMmPerSecSquaredToMetersPerSecSquared(a)).ToArray();

                Vx1 = new ObservableCollection<double>(MatriceIntegrationService.Integrate(ax1Meters, Step, VX1Start));
                Vy1 = new ObservableCollection<double>(MatriceIntegrationService.Integrate(ay1Meters, Step, VY1Start));
            }
        }

        private void RefreshPoint2VelocityData()
        {
            if (Ax2 != null && Ay2 != null)
            {
                // Конвертируем ускорения из мм/с² в м/с² перед интегрированием
                var ax2Meters = Ax2.Select(a => MetricConvertingService.ConvertMmPerSecSquaredToMetersPerSecSquared(a)).ToArray();
                var ay2Meters = Ay2.Select(a => MetricConvertingService.ConvertMmPerSecSquaredToMetersPerSecSquared(a)).ToArray();

                Vx2 = new ObservableCollection<double>(MatriceIntegrationService.Integrate(ax2Meters, Step, VX2Start));
                Vy2 = new ObservableCollection<double>(MatriceIntegrationService.Integrate(ay2Meters, Step, VY2Start));
            }
        }

        private void RefreshPoint1PositionData()
        {
            if (Vx1 != null && Vy1 != null)
            {
                X1 = new ObservableCollection<double>(MatriceIntegrationService.Integrate([.. Vx1], Step, X1Start));
                Y1 = new ObservableCollection<double>(MatriceIntegrationService.Integrate([.. Vy1], Step, Y1Start));
            }
        }

        private void RefreshPoint2PositionData()
        {
            if (Vx2 != null && Vy2 != null)
            {
                X2 = new ObservableCollection<double>(MatriceIntegrationService.Integrate([.. Vx2], Step, X2Start));
                Y2 = new ObservableCollection<double>(MatriceIntegrationService.Integrate([.. Vy2], Step, Y2Start));
            }
        }

        private void RefreshAngleData()
        {
            if (Y1 == null || Y2 == null || Y1.Count != Y2.Count || L == 0)
            {
                Angle.Clear();
                return;
            }

            Angle.Clear();

            for (int i = 0; i < Y1.Count; i++)
            {
                double delta = (Y2[i] - Y1[i]) / L;
                delta = Math.Clamp(delta, -1, 1);
                Angle.Add(MetricConvertingService.ConvertRadiansToDegrees(Math.Asin(delta)));
            }
        }

        private void RefreshPoint3PositionData()
        {
            // Очищаем предыдущие данные
            X3.Clear();
            Y3.Clear();

            // Проверяем, что все необходимые коллекции инициализированы и имеют данные
            if (X1 == null || Y1 == null || Angle == null ||
                X1.Count == 0 || X1.Count != Y1.Count || X1.Count != Angle.Count)
            {
                return;
            }

            // Предварительно вычисляем константу, чтобы не считать её в цикле
            double distance = Math.Sqrt(Math.Pow(L + B, 2) + Math.Pow(H / 2, 2));
            double alfaStartRadians = MetricConvertingService.ConvertDegreesToRadians(AlfaStart); // Переводим угол в радианы

            for (int i = 0; i < X1.Count; i++)
            {
                // Вычисляем угол поворота (текущий угол + начальный угол в радианах)
                double currentAngle = MetricConvertingService.ConvertDegreesToRadians(Angle[i]) + alfaStartRadians;

                // Рассчитываем и добавляем координаты точки 3
                X3.Add(X1[i] + distance * Math.Cos(currentAngle));
                Y3.Add(Y1[i] + distance * Math.Sin(currentAngle));
            }
        }
        private void RefreshPoint4PositionData()
        {
            // Очищаем предыдущие данные
            X4.Clear();
            Y4.Clear();

            // Проверяем, что все необходимые коллекции инициализированы и имеют данные
            if (X1 == null || Y1 == null || Angle == null ||
                X1.Count == 0 || X1.Count != Y1.Count || X1.Count != Angle.Count)
            {
                return;
            }

            // Предварительно вычисляем константу, чтобы не считать её в цикле
            double distance = Math.Sqrt(Math.Pow(L + B, 2) + Math.Pow(H / 2, 2));
            double alfaStartRadians = MetricConvertingService.ConvertDegreesToRadians(AlfaStart); // Переводим угол в радианы

            for (int i = 0; i < X1.Count; i++)
            {
                // Вычисляем угол поворота (текущий угол + начальный угол в радианах)
                double currentAngle = MetricConvertingService.ConvertDegreesToRadians(Angle[i]) - alfaStartRadians;

                // Рассчитываем и добавляем координаты точки 3
                X4.Add(X1[i] + distance * Math.Cos(currentAngle));
                Y4.Add(Y1[i] + distance * Math.Sin(currentAngle));
            }
        }
        private void RefreshPoint5PositionData()
        {
            // Очищаем предыдущие данные
            X5.Clear();
            Y5.Clear();

            // Проверяем, что все необходимые коллекции инициализированы и имеют данные
            if (X1 == null || Y1 == null || Angle == null ||
                X1.Count == 0 || X1.Count != Y1.Count || X1.Count != Angle.Count)
            {
                return;
            }

            // Предварительно вычисляем константу расстояния от точки 1 до точки 5
            double distance = Math.Sqrt(Math.Pow(A, 2) + Math.Pow(H / 2, 2));

            // Начальный угол (BettaStart) в радианах
            double bettaStartRadians = MetricConvertingService.ConvertDegreesToRadians(BettaStart);

            for (int i = 0; i < X1.Count; i++)
            {
                // Текущий угол поворота автомобиля (Angle[i] уже в радианах)
                double carAngle = MetricConvertingService.ConvertDegreesToRadians(Angle[i]);

                // Угол между осью автомобиля и линией к точке 5
                double pointAngle = Math.PI + bettaStartRadians; // 180° + BettaStart

                // Общий угол для расчета позиции точки 5
                double totalAngle = carAngle + pointAngle;

                // Рассчитываем и добавляем координаты точки 5
                X5.Add(X1[i] + distance * Math.Cos(totalAngle));
                Y5.Add(Y1[i] + distance * Math.Sin(totalAngle));
            }
        }
        private void RefreshPoint6PositionData()
        {
            // Очищаем предыдущие данные
            X6.Clear();
            Y6.Clear();

            // Проверяем, что все необходимые коллекции инициализированы и имеют данные
            if (X1 == null || Y1 == null || Angle == null ||
                X1.Count == 0 || X1.Count != Y1.Count || X1.Count != Angle.Count)
            {
                return;
            }

            // Предварительно вычисляем константу расстояния от точки 1 до точки 5
            double distance = Math.Sqrt(Math.Pow(A, 2) + Math.Pow(H / 2, 2));

            // Начальный угол (BettaStart) в радианах
            double bettaStartRadians = MetricConvertingService.ConvertDegreesToRadians(BettaStart);

            for (int i = 0; i < X1.Count; i++)
            {
                // Текущий угол поворота автомобиля (Angle[i] уже в радианах)
                double carAngle = MetricConvertingService.ConvertDegreesToRadians(Angle[i]);

                // Угол между осью автомобиля и линией к точке 5
                double pointAngle = Math.PI - bettaStartRadians; // 180° + BettaStart

                // Общий угол для расчета позиции точки 5
                double totalAngle = carAngle + pointAngle;

                // Рассчитываем и добавляем координаты точки 5
                X6.Add(X1[i] + distance * Math.Cos(totalAngle));
                Y6.Add(Y1[i] + distance * Math.Sin(totalAngle));
            }
        }

        public void UpdateAccelerationData(double[] ax1Data, double[] ay1Data, double[] ax2Data, double[] ay2Data)
        {
            Ax1.Clear();
            Ay1.Clear();
            foreach (var value in ax1Data)
            {
                Ax1.Add(value);
            }
            foreach (var value in ay1Data)
            {
                Ay1.Add(value);
            }

            Ax2.Clear();
            Ay2.Clear();
            foreach (var value in ax2Data)
            {
                Ax2.Add(value);
            }
            foreach (var value in ay2Data)
            {
                Ay2.Add(value);
            }

            RefreshParameters();
        }
        public void UpdateData()
        {
            UpdatePoint1Data();
            UpdatePoint2Data();

            UpdatePoint3Data();
            UpdatePoint4Data();
            UpdatePoint5Data();
            UpdatePoint6Data();
        }
        public void UpdatePoint1Data()
        {
            UpdateInputAcceleration1Data();
            RefreshPoint1VelocityData();
            UpdatePoint1VelocityData();
            RefreshPoint1PositionData();
            UpdatePoint1PositionData();
        }
        public void UpdatePoint2Data()
        {
            UpdateInputAcceleration2Data();
            RefreshPoint2VelocityData();
            UpdatePoint2VelocityData();
            RefreshPoint2PositionData();
            UpdatePoint2PositionData();
        }
        public void UpdateInputAcceleration1Data()
        {
            InputAcceleration1Data.Clear();

            if (Ax1 == null || Ay1 == null)
                return;

            for (int i = 0; i < Math.Min(Ax1.Count, Ay1.Count); i++)
            {
                InputAcceleration1Data.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = Ax1[i],
                    Parameter2 = Ay1[i]
                });
            }
        }
        public void UpdateInputAcceleration2Data()
        {
            InputAcceleration2Data.Clear();

            if (Ax2 == null || Ay2 == null)
                return;

            for (int i = 0; i < Math.Min(Ax2.Count, Ay2.Count); i++)
            {
                InputAcceleration2Data.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = Ax2[i],
                    Parameter2 = Ay2[i]
                });
            }
        }
        public void UpdatePoint1VelocityData()
        {
            Point1VelocityData.Clear();

            if (Vx1 == null || Vy1 == null)
                return;

            for (int i = 0; i < Math.Min(Vx1.Count, Vy1.Count); i++)
            {
                Point1VelocityData.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = Vx1[i],
                    Parameter2 = Vy1[i]
                });
            }
        }
        public void UpdatePoint2VelocityData()
        {
            Point2VelocityData.Clear();

            if (Vx2 == null || Vy2 == null)
                return;

            for (int i = 0; i < Math.Min(Vx2.Count, Vy2.Count); i++)
            {
                Point2VelocityData.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = Vx2[i],
                    Parameter2 = Vy2[i]
                });
            }
        }

        public void UpdatePoint1PositionData()
        {
            Point1PositionData.Clear();

            if (X1 == null || Y1 == null)
                return;

            for (int i = 0; i < Math.Min(X1.Count, Y1.Count); i++)
            {
                Point1PositionData.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = X1[i],
                    Parameter2 = Y1[i]
                });
            }
        }
        public void UpdatePoint2PositionData()
        {
            Point2PositionData.Clear();

            if (X2 == null || Y2 == null)
                return;

            for (int i = 0; i < Math.Min(X2.Count, Y2.Count); i++)
            {
                Point2PositionData.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = X2[i],
                    Parameter2 = Y2[i]
                });
            }
        }
        public void UpdatePoint3Data()
        {
            Point3PositionData.Clear();

            if (X3 == null || Y3 == null)
                return;

            for (int i = 0; i < Math.Min(X3.Count, Y3.Count); i++)
            {
                Point3PositionData.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = X3[i],
                    Parameter2 = Y3[i]
                });
            }
        }
        public void UpdatePoint4Data()
        {
            Point4PositionData.Clear();

            if (X4 == null || Y4 == null)
                return;

            for (int i = 0; i < Math.Min(X4.Count, Y4.Count); i++)
            {
                Point4PositionData.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = X4[i],
                    Parameter2 = Y4[i]
                });
            }
        }
        public void UpdatePoint5Data()
        {
            Point5PositionData.Clear();

            if (X5 == null || Y5 == null)
                return;

            for (int i = 0; i < Math.Min(X5.Count, Y5.Count); i++)
            {
                Point5PositionData.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = X5[i],
                    Parameter2 = Y5[i]
                });
            }
        }
        public void UpdatePoint6Data()
        {
            Point6PositionData.Clear();
            
            if (X6 == null || Y6 == null)
                return;

            for (int i = 0; i < Math.Min(X6.Count, Y6.Count); i++)
            {
                Point6PositionData.Add(new TableTwoParameterData
                {
                    Time = Step * i,
                    Parameter1 = X6[i],
                    Parameter2 = Y6[i]
                });
            }
        }

        // Добавляем поддержку INotifyDataErrorInfo
        private readonly Dictionary<string, List<string>> _errors = new();

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return null;
        }

        private void SetError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void DoOnPropertyChanged()
        {
            OnPropertyChanged();
        }
    }

    public class TableResultData
    {
        public double Time { get; set; }
        public double Parameter1 { get; set; }
        public double Parameter2 { get; set; }
        public double Parameter3 { get; set; }
        public double Parameter4 { get; set; }
        public double Parameter5 { get; set; }
        public double Parameter6 { get; set; }
        public double Parameter7 { get; set; }
        public double Parameter8 { get; set; }
        public double Parameter9 { get; set; }
        public double Parameter10 { get; set; }
        public double Parameter11 { get; set; }
        public double Parameter12 { get; set; }
        public double Parameter13 { get; set; }
    }
    public class TableOneParameterData
    {
        public double Time { get; set; }
        public double Parameter1 { get; set; }
    }
    public class TableTwoParameterData
    {
        public double Time { get; set; }
        public double Parameter1 { get; set; }
        public double Parameter2 { get; set; }
    }
    public class TableThreeParameterData
    {
        public double Time { get; set; }
        public double Parameter1 { get; set; }
        public double Parameter2 { get; set; }
        public double Parameter3 { get; set; }
    }
    public class TableFourParameterData
    {
        public double Time { get; set; }
        public double Parameter1 { get; set; }
        public double Parameter2 { get; set; }
        public double Parameter3 { get; set; }
        public double Parameter4 { get; set; }
    }
}
