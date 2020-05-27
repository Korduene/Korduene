using Korduene.Graphing.CS;
using System.Windows;
using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.DataTemplateSelectors
{
    public class PortEditorDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var port = item as CSPort;

            if (port == null)
            {
                return (container as FrameworkElement).FindResource("emptyTemplate") as DataTemplate;
            }

            var type = GetDataType(port);

            switch (type)
            {
                case "string":
                    return (container as FrameworkElement).FindResource("stringEditor") as DataTemplate;
                case "int":
                    return (container as FrameworkElement).FindResource("intEditor") as DataTemplate;
                case "long":
                    return (container as FrameworkElement).FindResource("longEditor") as DataTemplate;
                case "double":
                    return (container as FrameworkElement).FindResource("doubleEditor") as DataTemplate;
                case "float":
                    return (container as FrameworkElement).FindResource("floatEditor") as DataTemplate;
                case "decimal":
                    return (container as FrameworkElement).FindResource("decimalEditor") as DataTemplate;
                case "byte":
                    return (container as FrameworkElement).FindResource("byteEditor") as DataTemplate;
                case "char":
                    return (container as FrameworkElement).FindResource("charEditor") as DataTemplate;
                case "bool":
                    return (container as FrameworkElement).FindResource("boolEditor") as DataTemplate;
                default:
                    break;
            }

            return (container as FrameworkElement).FindResource("emptyTemplate") as DataTemplate;
            //return base.SelectTemplate(item, container);
        }

        private string GetDataType(CSPort port)
        {
            return port.Symbol?.Name.ToLower();
        }
    }
}
