using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.UI.WPF.Converters
{
    public class AppStateToBoolConverter : MarkupExtension, IValueConverter
    {
        public AppState State { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Current.Instance.State == State;
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
