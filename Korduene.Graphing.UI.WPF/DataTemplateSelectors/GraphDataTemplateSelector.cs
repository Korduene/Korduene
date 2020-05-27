using System.Windows;
using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.DataTemplateSelectors
{
    public class GraphDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is INode)
            {
                return (container as FrameworkElement).FindResource("NodeTemplate") as DataTemplate;
            }
            else if (item is IPort port)
            {
                if (port.PortType == Enums.PortType.In)
                {
                    //if (port.IsPassThrough)
                    //{
                    //    return (container as FrameworkElement).FindResource("PassInPortTemplate") as DataTemplate;
                    //}

                    return (container as FrameworkElement).FindResource("InPortTemplate") as DataTemplate;
                }
                else if (port.PortType == Enums.PortType.Out)
                {
                    //if (port.IsPassThrough)
                    //{
                    //    return (container as FrameworkElement).FindResource("PassOutPortTemplate") as DataTemplate;
                    //}

                    return (container as FrameworkElement).FindResource("OutPortTemplate") as DataTemplate;
                }
            }
            else if (item is ILink)
            {
                return (container as FrameworkElement).FindResource("LinkTemplate") as DataTemplate;
            }
            else if (item is IComment)
            {
                return (container as FrameworkElement).FindResource("CommentTemplate") as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
