using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class BezierPointsConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var link = values[0] as ILink;
            var bezier = values[1] as System.Windows.Media.BezierSegment;

            bezier.Point1 = new System.Windows.Point(link.StartPort.Location.X, link.StartPort.Location.Y);
            bezier.Point3 = new System.Windows.Point(link.EndPort.Location.X, link.EndPort.Location.Y);

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
