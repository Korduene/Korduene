using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class GraphColorToSolidBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GraphColor graphColor)
            {
                return new SolidColorBrush(Color.FromArgb(graphColor.A, graphColor.R, graphColor.G, graphColor.B));
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush && brush.Color != null)
            {
                return new GraphColor(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
            }

            return new GraphColor();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
