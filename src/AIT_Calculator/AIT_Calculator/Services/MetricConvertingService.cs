using System;

namespace AIT_Calculator.Services
{
    /// <summary>
    /// Сервис для преобразования единиц измерения между различными системами
    /// </summary>
    public static class MetricConvertingService
    {
        #region Ускорение (мм/с² ↔ м/с²)

        /// <summary>
        /// Преобразует миллиметры в секунду в квадрате в метры в секунду в квадрате
        /// </summary>
        /// <param name="mmPerSecSquared">Значение в мм/с²</param>
        /// <returns>Значение в м/с²</returns>
        public static double ConvertMmPerSecSquaredToMetersPerSecSquared(double mmPerSecSquared)
        {
            return mmPerSecSquared / 1000.0; // вот тут должно быть 1000
        }

        /// <summary>
        /// Преобразует метры в секунду в квадрате в миллиметры в секунду в квадрате
        /// </summary>
        /// <param name="mPerSecSquared">Значение в м/с²</param>
        /// <returns>Значение в мм/с²</returns>
        public static double ConvertMetersPerSecSquaredToMmPerSecSquared(double mPerSecSquared)
        {
            return mPerSecSquared * 1000.0; // вот тут должно быть 1000
        }

        /// <summary>
        /// Преобразует массив значений из мм/с² в м/с²
        /// </summary>
        /// <param name="values">Массив значений в мм/с²</param>
        /// <returns>Новый массив значений в м/с²</returns>
        public static double[] ConvertMmPerSecSquaredArrayToMetersPerSecSquared(double[] values)
        {
            if (values == null) return null;

            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = ConvertMmPerSecSquaredToMetersPerSecSquared(values[i]);
            }
            return result;
        }

        /// <summary>
        /// Преобразует массив значений из м/с² в мм/с²
        /// </summary>
        /// <param name="values">Массив значений в м/с²</param>
        /// <returns>Новый массив значений в мм/с²</returns>
        public static double[] ConvertMetersPerSecSquaredArrayToMmPerSecSquared(double[] values)
        {
            if (values == null) return null;

            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = ConvertMetersPerSecSquaredToMmPerSecSquared(values[i]);
            }
            return result;
        }

        #endregion

        #region Скорость (км/ч ↔ м/с)

        /// <summary>
        /// Преобразует километры в час в метры в секунду
        /// </summary>
        /// <param name="kmPerHour">Значение в км/ч</param>
        /// <returns>Значение в м/с</returns>
        public static double ConvertKmPerHourToMetersPerSecond(double kmPerHour)
        {
            return kmPerHour * 1000.0 / 3600.0;
        }

        /// <summary>
        /// Преобразует метры в секунду в километры в час
        /// </summary>
        /// <param name="mPerSecond">Значение в м/с</param>
        /// <returns>Значение в км/ч</returns>
        public static double ConvertMetersPerSecondToKmPerHour(double mPerSecond)
        {
            return mPerSecond * 3600.0 / 1000.0;
        }

        /// <summary>
        /// Преобразует массив значений из км/ч в м/с
        /// </summary>
        /// <param name="values">Массив значений в км/ч</param>
        /// <returns>Новый массив значений в м/с</returns>
        public static double[] ConvertKmPerHourArrayToMetersPerSecond(double[] values)
        {
            if (values == null) return null;

            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = ConvertKmPerHourToMetersPerSecond(values[i]);
            }
            return result;
        }

        /// <summary>
        /// Преобразует массив значений из м/с в км/ч
        /// </summary>
        /// <param name="values">Массив значений в м/с</param>
        /// <returns>Новый массив значений в км/ч</returns>
        public static double[] ConvertMetersPerSecondArrayToKmPerHour(double[] values)
        {
            if (values == null) return null;

            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = ConvertMetersPerSecondToKmPerHour(values[i]);
            }
            return result;
        }

        #endregion

        #region Углы (радианы ↔ градусы)

        /// <summary>
        /// Преобразует радианы в градусы
        /// </summary>
        /// <param name="radians">Угол в радианах</param>
        /// <returns>Угол в градусах</returns>
        public static double ConvertRadiansToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        /// <summary>
        /// Преобразует градусы в радианы
        /// </summary>
        /// <param name="degrees">Угол в градусах</param>
        /// <returns>Угол в радианах</returns>
        public static double ConvertDegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        /// <summary>
        /// Преобразует массив значений из радиан в градусы
        /// </summary>
        /// <param name="values">Массив углов в радианах</param>
        /// <returns>Массив углов в градусах</returns>
        public static double[] ConvertRadiansArrayToDegrees(double[] values)
        {
            if (values == null) return null;

            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = ConvertRadiansToDegrees(values[i]);
            }
            return result;
        }

        /// <summary>
        /// Преобразует массив значений из градусов в радианы
        /// </summary>
        /// <param name="values">Массив углов в градусах</param>
        /// <returns>Массив углов в радианах</returns>
        public static double[] ConvertDegreesArrayToRadians(double[] values)
        {
            if (values == null) return null;

            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = ConvertDegreesToRadians(values[i]);
            }
            return result;
        }

        #endregion
    }
}