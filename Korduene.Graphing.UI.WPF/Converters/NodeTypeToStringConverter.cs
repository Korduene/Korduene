using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class NodeTypeToStringConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ISymbol symbol)
            {
                return GetDisplayParts(symbol, null);
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

        private IEnumerable<DisplayPartContent> GetDisplayParts(ISymbol symbol, ISymbol parent)
        {
            var result = new List<DisplayPartContent>();

            if (parent == null && symbol is INamedTypeSymbol namedTypeSymbol)
            {
                result.Add(new DisplayPartContent($"{namedTypeSymbol.TypeKind.ToString().ToLower()} ", Brushes.Blue));
            }
            else if (parent == null && symbol is INamespaceSymbol namespaceSymbol)
            {
                result.Add(new DisplayPartContent($"{namespaceSymbol.Kind.ToString().ToLower()} ", Brushes.Blue));
            }
            else if (symbol is IMethodSymbol methodSymbol)
            {
                if (methodSymbol.ReturnType.SpecialType != SpecialType.None)
                {
                    result.Add(new DisplayPartContent($"{methodSymbol.ReturnType.ToString()} ", Brushes.Blue));
                }
                else
                {
                    result.AddRange(GetDisplayParts(methodSymbol.ReturnType, methodSymbol));
                    result.Add(new DisplayPartContent(" ", Brushes.Transparent));
                }
            }
            else if (symbol is IPropertySymbol propertySymbol)
            {
                if (propertySymbol.Type.SpecialType != SpecialType.None)
                {
                    result.Add(new DisplayPartContent($"{propertySymbol.Type.ToString()} ", Brushes.Blue));
                }
                else
                {
                    result.AddRange(GetDisplayParts(propertySymbol.Type, propertySymbol));
                    result.Add(new DisplayPartContent(" ", Brushes.Transparent));
                }
            }
            var parts = symbol.ToDisplayParts(SymbolDisplayFormat.CSharpErrorMessageFormat.AddParameterOptions(SymbolDisplayParameterOptions.IncludeName));

            foreach (var item in parts)
            {
                result.Add(GetPartContent(item));
            }

            return result;
        }

        private DisplayPartContent GetPartContent(SymbolDisplayPart item)
        {
            if (item.Kind == SymbolDisplayPartKind.NamespaceName || item.Kind == SymbolDisplayPartKind.Punctuation || item.Kind == SymbolDisplayPartKind.PropertyName)
            {
                return new DisplayPartContent(item.ToString(), Brushes.Black);
            }
            else if (item.Kind == SymbolDisplayPartKind.ClassName || item.Kind == SymbolDisplayPartKind.InterfaceName || item.Kind == SymbolDisplayPartKind.EnumName || item.Kind == SymbolDisplayPartKind.DelegateName || item.Kind == SymbolDisplayPartKind.TypeParameterName)
            {
                return new DisplayPartContent(item.ToString(), new SolidColorBrush(Color.FromRgb(43, 145, 175)));
            }
            else if (item.Kind == SymbolDisplayPartKind.MethodName || item.Kind == SymbolDisplayPartKind.ExtensionMethodName)
            {
                return new DisplayPartContent(item.ToString(), new SolidColorBrush(Color.FromRgb(116, 83, 31)));
            }
            else if (item.Kind == SymbolDisplayPartKind.Keyword)
            {
                return new DisplayPartContent(item.ToString(), Brushes.Blue);
            }
            else if (item.Kind == SymbolDisplayPartKind.ParameterName)
            {
                return new DisplayPartContent(item.ToString(), Brushes.DarkSlateGray);
            }
            else if (item.Kind == SymbolDisplayPartKind.Space)
            {
                return new DisplayPartContent(item.ToString(), Brushes.Transparent);
            }

            return new DisplayPartContent();
        }
    }
}
