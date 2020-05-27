using Korduene.Templates;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.UI.WPF.Converters
{
    public class ProjectTemplateToImageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var template = value as ProjectTemplate;

            if (template == null)
            {
                return null;
            }

            var file = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(template.File), template.Icon);

            if (System.IO.File.Exists(file))
            {
                return new Image();
            }

            return GetResource(template.Icon) ?? GetResource($"{template.Icon}_16x");
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
