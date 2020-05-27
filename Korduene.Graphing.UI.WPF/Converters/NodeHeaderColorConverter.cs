using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class NodeHeaderColorConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GraphColor graphColor)
            {
                var color = Color.FromArgb(graphColor.A, graphColor.R, graphColor.G, graphColor.B);

                var linearGradientBrush = new LinearGradientBrush(color, Colors.Transparent, new Point(0, 0.5), new Point(1, 0.5));

                var gradientStop1 = new GradientStop
                {
                    Color = color,
                    Offset = 0
                };

                var gradientStop2 = new GradientStop
                {
                    Color = Colors.Transparent,
                    Offset = 1
                };

                linearGradientBrush.GradientStops.Add(gradientStop1);
                linearGradientBrush.GradientStops.Add(gradientStop2);

                return linearGradientBrush;
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return new GraphColor(color.A, color.R, color.G, color.B);
            }

            return new GraphColor();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
