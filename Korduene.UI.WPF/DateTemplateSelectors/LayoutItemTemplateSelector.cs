using Korduene.UI.WPF.Documents.Views;
using Korduene.UI.WPF.ToolWindows.ViewModels;
using Korduene.UI.WPF.ToolWindows.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Korduene.UI.WPF.DateTemplateSelectors
{
    public class LayoutItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is SolutionExplorerViewModel solutionExplorer)
            {
                return GetDataTemplate<SolutionExplorerView>(solutionExplorer);
            }
            else if (item is PropertiesViewModel properties)
            {
                return GetDataTemplate<PropertiesView>(properties);
            }
            else if (item is ToolBoxViewModel toolBox)
            {
                return GetDataTemplate<ToolBoxView>(toolBox);
            }
            else if (item is OutputViewModel output)
            {
                return GetDataTemplate<OutputView>(output);
            }
            else if (item is KDocumentBase document)
            {
                //return GetDataTemplate<GraphDocumentView>(document);
                var ext = System.IO.Path.GetExtension(document.FilePath);

                if (ext.Equals(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    return GetDataTemplate<CSDocumentView>(document);
                }
                else if (ext.Equals(".xaml", StringComparison.OrdinalIgnoreCase))
                {
                    return GetDataTemplate<XamlDocumentView>(document);
                }
            }

            return base.SelectTemplate(item, container);
        }

        private DataTemplate GetDataTemplate<T>(object dataContext = null)
        {
            var fe = new FrameworkElementFactory(typeof(T));

            if (dataContext != null)
            {
                fe.SetValue(FrameworkElement.DataContextProperty, dataContext);
            }

            return new DataTemplate()
            {
                VisualTree = fe
            };
        }
    }
}
