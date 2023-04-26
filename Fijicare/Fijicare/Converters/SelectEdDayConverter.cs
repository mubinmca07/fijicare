using System;
using System.Globalization;
using Xamarin.Forms;

namespace Fijicare.Converters
{
    class SelectEdDayConverter : Xamarin.Forms.IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int day = (int)value;
                return day == (int)Fijicare.App.SelectedDate.DayOfWeek ? Color.Green : Color.Black;
            }
            catch (Exception)
            {
                return null;
            }
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int day = (int)value;
            return day == (int)Fijicare.App.SelectedDate.DayOfWeek ? Color.Green : Color.Black;
        }
    }
}
