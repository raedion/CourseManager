using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CourseManager.Converter
{
    class Bool2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true) {
                    return new SolidColorBrush(System.Windows.Media.Colors.Blue);

                }
                else
                {
                    return new SolidColorBrush(System.Windows.Media.Colors.Red);
                }
            }
            return new SolidColorBrush(System.Windows.Media.Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool)value);
        }
    }
}
