using Korduene.Graphing.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class PortTypeToColumnConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 0 && values[0] is PortType portType)
            {
                var element = values[1] as UIElement;

                if (portType == PortType.In)
                {
                    Grid.SetColumn(element, 0);
                    return 0;
                }
                else if (portType == PortType.Out)
                {
                    Grid.SetColumn(element, 1);
                    return 1;
                }
            }

            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (value is PortType portType)
        //    {
        //        if (portType == PortType.IN)
        //        {
        //            return 0;
        //        }
        //        else if (portType == PortType.OUT)
        //        {
        //            return 1;
        //        }
        //    }

        //    return 0;
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
