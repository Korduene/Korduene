using Microsoft.CodeAnalysis;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class SymbolToDocumentationConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ISymbol symbol)
            {
                var doc = symbol.GetDocumentationCommentXml();
                doc = doc.Replace("<summary>", string.Empty).Replace("</summary>", string.Empty).Trim();

                return doc;
            }

            return string.Empty;
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
