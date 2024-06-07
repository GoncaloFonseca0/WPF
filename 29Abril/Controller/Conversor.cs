using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace _29Abril.Controller
{
    internal class Conversor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            if (int.TryParse(value.ToString(), out int dado))
            {
                BitmapImage bitmap = new BitmapImage(new Uri("/dados/"+dado.ToString() + ".png", UriKind.Relative));
                return bitmap;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
