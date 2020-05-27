using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.UI.WPF.Converters
{
    public class FileTypeToImageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as WorkspaceItem;

            if (item == null)
            {
                return null;
            }

            if (item.Type == WorkspaceItemType.Folder)
            {
                return GetResource("Folder_16x");
            }
            else if (item.Type == WorkspaceItemType.Solution || item.Type == WorkspaceItemType.Project || item.Type == WorkspaceItemType.File)
            {
                var extension = System.IO.Path.GetExtension(item.Path).ToLower();

                switch (extension)
                {
                    case ".sln":
                        return GetResource("Application_16x");
                    case ".csproj":
                        return GetResource("CSApplication_16x");
                    case ".xaml":
                        return GetResource("WPFPage_16x");
                    case ".cs":
                        return GetResource("CS_16x");
                    default:
                        return GetResource("TextFile_16x");
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        private object GetResource(string name)
        {
            foreach (var md in System.Windows.Application.Current.Resources.MergedDictionaries)
            {
                foreach (var key in md.Keys)
                {
                    if (key.ToString().Equals(name))
                    {
                        return md[key];
                    }
                }
            }

            return null;
        }
    }
}
