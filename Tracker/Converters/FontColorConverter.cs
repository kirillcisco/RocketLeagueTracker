using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Tracker
{
    class FontColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = new SolidColorBrush();

            if ((double?)value >= 10)
                brush.Color = Colors.White;
            else
                brush.Color = Colors.Gray;

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
