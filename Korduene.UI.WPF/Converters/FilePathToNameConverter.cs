using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.UI.WPF.Converters
{
    public class FilePathToNameConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && !string.IsNullOrWhiteSpace(str))
            {
                return System.IO.Path.GetFileNameWithoutExtension(str);
            }

            return string.Empty;
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
