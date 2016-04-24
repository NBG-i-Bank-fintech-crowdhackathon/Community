using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace CrowdHacakthon.Converters
{
    public class TypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int v = (int)value;

            if (v==0)
            {
                return new SolidColorBrush(Colors.BlueViolet);
            }
            if (v == 1)
            {
                return new SolidColorBrush(Colors.Coral);
            }
            if (v == 2)
            {
                return new SolidColorBrush(Colors.MediumTurquoise);
            }
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
