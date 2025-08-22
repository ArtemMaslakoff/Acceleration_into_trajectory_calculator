using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AIT_Calculator.Converters
{
    public class FractionWidthConverter : IValueConverter
    {
        public double Fraction { get; set; } = 1.0;
        public double Margin { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                return (width * Fraction) - Margin;
            }
            return 100; // значение по умолчанию
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
