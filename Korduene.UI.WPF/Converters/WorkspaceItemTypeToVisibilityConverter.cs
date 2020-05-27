using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.UI.WPF.Converters
{
    public class WorkspaceItemTypeToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public bool All { get; set; }

        public bool Solution { get; set; }

        public bool Project { get; set; }

        public bool Folder { get; set; }

        public bool File { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is WorkspaceItemType workspaceItemType))
            {
                return Visibility.Collapsed;
            }

            if (All)
            {
                return Visibility.Visible;
            }
            else if (Solution && workspaceItemType == WorkspaceItemType.Solution)
            {
                return Visibility.Visible;
            }
            else if (Project && workspaceItemType == WorkspaceItemType.Project)
            {
                return Visibility.Visible;
            }
            else if (Folder && workspaceItemType == WorkspaceItemType.Folder)
            {
                return Visibility.Visible;
            }
            else if (File && workspaceItemType == WorkspaceItemType.File)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
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
