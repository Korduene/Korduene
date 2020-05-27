using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class IsGreatorThanZeroToBoolConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value?.ToString(), out int i))
            {
                return i > 0;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
