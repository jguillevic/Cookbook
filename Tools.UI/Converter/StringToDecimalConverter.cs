using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Tools.UI.Converter
{
    public class StringToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            else
                return decimal.Zero;
        }
    }
}
