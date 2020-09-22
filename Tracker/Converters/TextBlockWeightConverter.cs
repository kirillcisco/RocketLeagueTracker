using System;
using System.Windows;
using System.Windows.Data;

namespace Tracker
{
    class TextBlockWeightConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {

                FontWeight weight = FontWeights.Normal;

                if ((double?)value >= 10)
                    weight = FontWeights.Bold;
                return weight;
            }
            else
            {

                FontWeight weight = FontWeights.Normal;

                if ((double?)value >= 10)
                    weight = FontWeights.Bold;
                return weight;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
