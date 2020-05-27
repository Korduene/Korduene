using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Korduene.Graphing.CS
{
    public static class SpecialTypeToSyntaxKind
    {
        public static SyntaxKind ConvertToLiteralExpression(SpecialType specialType, object value)
        {
            if (value == null)
            {
                return SyntaxKind.NullLiteralExpression;
            }

            switch (specialType)
            {
                case SpecialType.None:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Object:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Enum:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_MulticastDelegate:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Delegate:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_ValueType:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Void:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Boolean:
                    if (value is bool b && b)
                    {
                        return SyntaxKind.TrueLiteralExpression;
                    }
                    else
                    {
                        return SyntaxKind.FalseLiteralExpression;
                    }
                case SpecialType.System_Char:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_SByte:
                case SpecialType.System_Byte:
                case SpecialType.System_Int16:
                case SpecialType.System_UInt16:
                case SpecialType.System_Int32:
                case SpecialType.System_UInt32:
                case SpecialType.System_Int64:
                case SpecialType.System_UInt64:
                case SpecialType.System_Decimal:
                case SpecialType.System_Single:
                case SpecialType.System_Double:
                    return SyntaxKind.NumericLiteralExpression;
                case SpecialType.System_String:
                    return SyntaxKind.StringLiteralExpression;
                case SpecialType.System_IntPtr:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_UIntPtr:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Array:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Collections_IEnumerable:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Collections_Generic_IEnumerable_T:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Collections_Generic_IList_T:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Collections_Generic_ICollection_T:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Collections_IEnumerator:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Collections_Generic_IEnumerator_T:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Collections_Generic_IReadOnlyList_T:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Collections_Generic_IReadOnlyCollection_T:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Nullable_T:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_DateTime:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Runtime_CompilerServices_IsVolatile:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_IDisposable:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_TypedReference:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_ArgIterator:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_RuntimeArgumentHandle:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_RuntimeFieldHandle:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_RuntimeMethodHandle:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_RuntimeTypeHandle:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_IAsyncResult:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_AsyncCallback:
                    return SyntaxKind.NullLiteralExpression;
                case SpecialType.System_Runtime_CompilerServices_RuntimeFeature:
                    return SyntaxKind.NullLiteralExpression;
                default:
                    return SyntaxKind.NullLiteralExpression;
            }
        }
    }
}
