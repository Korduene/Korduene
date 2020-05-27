//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Data;
//using System.Windows.Markup;
//using System.Windows.Media;

//namespace Korduene.Graphing.UI.WPF.Converters
//{
//    public class PortColorConverter : MarkupExtension, IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            var port = value as IPort;

//            if (port == null)
//            {
//                return new SolidColorBrush(Colors.Transparent);
//            }

//            return new SolidColorBrush(PortColors.GetPortColor(port));
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }

//        public override object ProvideValue(IServiceProvider serviceProvider)
//        {
//            return this;
//        }
//    }
//}
