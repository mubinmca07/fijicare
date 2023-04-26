using System;
using System.Globalization;

namespace Fijicare.Converters
{

    class ProducrConverter : Xamarin.Forms.IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string val = (string)value;
            if (val == "00")
                return "Motor";
            return "Health";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string val = (string)value;
            if (val == "00")
                return "Motor";
            return "Health";
        }
    }
}
