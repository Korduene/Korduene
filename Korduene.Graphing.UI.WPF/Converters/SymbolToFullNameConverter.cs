using Microsoft.CodeAnalysis;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class SymbolToFullNameConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var parts = methodSymbol.ToDisplayParts();

            if (value is INamedTypeSymbol namedTypeSymbol)
            {
                return namedTypeSymbol.ToString();
            }

            if (value is INamespaceSymbol namespaceSymbol)
            {
                return namespaceSymbol.ToString();
            }

            if (value is IMethodSymbol methodSymbol)
            {
                return methodSymbol.ToDisplayString();
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
