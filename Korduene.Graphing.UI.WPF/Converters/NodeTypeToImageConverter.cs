using Microsoft.CodeAnalysis;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Korduene.Graphing.UI.WPF.Converters
{
    public class NodeTypeToImageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is INamedTypeSymbol namedTypeSymbol)
            {
                if (namedTypeSymbol.SpecialType != SpecialType.None)
                {
                    return GetResource("IntelliSenseKeyword_16x");
                }

                switch (namedTypeSymbol.TypeKind)
                {
                    case TypeKind.Unknown:
                        break;
                    case TypeKind.Array:
                        return GetResource("Array_16x");
                    case TypeKind.Class:
                        return GetResource("Class_16x");
                    case TypeKind.Delegate:
                        return GetResource("Delegate_16x");
                    case TypeKind.Dynamic:
                        break;
                    case TypeKind.Enum:
                        return GetResource("Enumerator_16x");
                    case TypeKind.Error:
                        break;
                    case TypeKind.Interface:
                        return GetResource("Interface_16x");
                    case TypeKind.Module:
                        break;
                    case TypeKind.Pointer:
                        break;
                    case TypeKind.Struct:
                        return GetResource("Structure_16x");
                    //case TypeKind.Structure:
                    //break;
                    case TypeKind.TypeParameter:
                        break;
                    case TypeKind.Submission:
                        break;
                    default:
                        break;
                }
            }

            if (value is INamespaceSymbol namespaceSymbol)
            {
                return GetResource("Namespace_16x");
            }

            if (value is IMethodSymbol methodSymbol)
            {
                return GetResource("Method_16x");
            }

            if (value is IPropertySymbol propertySymbol)
            {
                return GetResource("Property_16x");
            }

            //switch (typeInfo.Kind)
            //{
            //    case TypeKind.Other:
            //        return null;
            //    case TypeKind.Class:
            //        return GetResource("Class_16x");
            //    case TypeKind.Interface:
            //        return GetResource("Interface_16x");
            //    case TypeKind.Struct:
            //        return GetResource("Structure_16x");
            //    case TypeKind.Delegate:
            //        return GetResource("Delegate_16x");
            //    case TypeKind.Enum:
            //        return GetResource("Enumerator_16x");
            //    case TypeKind.Void:
            //        return GetResource("IntelliSenseKeyword_16x");
            //    case TypeKind.Unknown:
            //        break;
            //    case TypeKind.Null:
            //        break;
            //    case TypeKind.None:
            //        break;
            //    case TypeKind.Dynamic:
            //        break;
            //    case TypeKind.UnboundTypeArgument:
            //        break;
            //    case TypeKind.TypeParameter:
            //        break;
            //    case TypeKind.Array:
            //        return GetResource("Array_16x");
            //    case TypeKind.Pointer:
            //        break;
            //    case TypeKind.ByReference:
            //        break;
            //    case TypeKind.Intersection:
            //        break;
            //    case TypeKind.ArgList:
            //        break;
            //    case TypeKind.Tuple:
            //        break;
            //    case TypeKind.ModOpt:
            //        break;
            //    case TypeKind.ModReq:
            //        break;
            //    default:
            //        break;
            //}

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
