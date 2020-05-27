using Korduene.Graphing.Enums;
using Korduene.Graphing.UI.WPF;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Korduene.Graphing.CS
{
    public class CSPort : Port
    {
        private string _symbolName;

        [JsonIgnore]
        private ISymbol _symbol;

        public string SymbolName
        {
            get { return _symbolName; }
            set
            {
                if (_symbolName != value)
                {
                    _symbolName = value;
                    _symbol = null;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Symbol));
                }
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public ISymbol Symbol
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SymbolName))
                {
                    return _symbol;
                }

                return _symbol ??= SymbolProvider.GetSymbol(SymbolName);
            }
        }

        public override GraphColor BorderColor
        {
            get
            {
                return PortColors.GetPortColor(this);
            }

            set
            {
                base.BorderColor = value;
            }
        }

        public CSPort()
        {
        }

        public CSPort(PortType portType, AcceptedConnections acceptedConnections) : base(portType, acceptedConnections)
        {
        }

        public CSPort(PortType portType, AcceptedConnections acceptedConnections, string text) : base(portType, acceptedConnections, text)
        {
        }

        public CSPort(PortType portType, AcceptedConnections acceptedConnections, string text, string dataTypeSymbolName) : base(portType, acceptedConnections, text)
        {
            this.SymbolName = dataTypeSymbolName;
        }

        public virtual IEnumerable<StatementSyntax> GetConnectedPortStatementsSyntax()
        {
            return null;
        }

        public virtual IEnumerable<StatementSyntax> GetFirstConnectedPortStatementsSyntax()
        {
            return null;
        }

        //public LiteralExpressionSyntax GetValueLiteralExpression()
        //{
        //    var type = Symbol as INamedTypeSymbol;

        //    var value = this.Value;

        //    if (ConnectedPorts.Any())
        //    {
        //        var connectedPort = ConnectedPorts.First() as CSPort;
        //        //return connectedPort.GetValueLiteralExpression();
        //    }

        //    //TODO: Check to see if port is connected if connected get the value from the other port

        //    if (this.Value == null)
        //    {
        //        return SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);
        //    }
        //    else if (type.SpecialType != SpecialType.None)
        //    {
        //        switch (type.SpecialType)
        //        {
        //            case SpecialType.None:
        //                break;
        //            case SpecialType.System_Object:
        //                break;
        //            case SpecialType.System_Enum:
        //                break;
        //            case SpecialType.System_MulticastDelegate:
        //                break;
        //            case SpecialType.System_Delegate:
        //                break;
        //            case SpecialType.System_ValueType:
        //                break;
        //            case SpecialType.System_Void:
        //                break;
        //            case SpecialType.System_Boolean:
        //                if (value is bool b && b)
        //                {
        //                    return SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression);
        //                }
        //                else
        //                {
        //                    return SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression);
        //                }
        //            case SpecialType.System_Char:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToChar()));
        //            case SpecialType.System_SByte:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToSByte()));
        //            case SpecialType.System_Byte:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToByte()));
        //            case SpecialType.System_Int16:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToInt16()));
        //            case SpecialType.System_UInt16:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToUInt16()));
        //            case SpecialType.System_Int32:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToInt32()));
        //            case SpecialType.System_UInt32:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToUInt32()));
        //            case SpecialType.System_Int64:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToInt64()));
        //            case SpecialType.System_UInt64:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToUInt64()));
        //            case SpecialType.System_Decimal:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToDecimal()));
        //            case SpecialType.System_Single:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToSingle()));
        //            case SpecialType.System_Double:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToDouble()));
        //            case SpecialType.System_String:
        //                return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value?.ToString()));
        //            case SpecialType.System_IntPtr:
        //                break;
        //            case SpecialType.System_UIntPtr:
        //                break;
        //            case SpecialType.System_Array:
        //                break;
        //            case SpecialType.System_Collections_IEnumerable:
        //                break;
        //            case SpecialType.System_Collections_Generic_IEnumerable_T:
        //                break;
        //            case SpecialType.System_Collections_Generic_IList_T:
        //                break;
        //            case SpecialType.System_Collections_Generic_ICollection_T:
        //                break;
        //            case SpecialType.System_Collections_IEnumerator:
        //                break;
        //            case SpecialType.System_Collections_Generic_IEnumerator_T:
        //                break;
        //            case SpecialType.System_Collections_Generic_IReadOnlyList_T:
        //                break;
        //            case SpecialType.System_Collections_Generic_IReadOnlyCollection_T:
        //                break;
        //            case SpecialType.System_Nullable_T:
        //                break;
        //            case SpecialType.System_DateTime:
        //                break;
        //            case SpecialType.System_Runtime_CompilerServices_IsVolatile:
        //                break;
        //            case SpecialType.System_IDisposable:
        //                break;
        //            case SpecialType.System_TypedReference:
        //                break;
        //            case SpecialType.System_ArgIterator:
        //                break;
        //            case SpecialType.System_RuntimeArgumentHandle:
        //                break;
        //            case SpecialType.System_RuntimeFieldHandle:
        //                break;
        //            case SpecialType.System_RuntimeMethodHandle:
        //                break;
        //            case SpecialType.System_RuntimeTypeHandle:
        //                break;
        //            case SpecialType.System_IAsyncResult:
        //                break;
        //            case SpecialType.System_AsyncCallback:
        //                break;
        //            case SpecialType.System_Runtime_CompilerServices_RuntimeFeature:
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    return SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);
        //}

        public virtual ExpressionSyntax GetFirstConnectedPortMemberAccessExpressionSyntax()
        {
            return (this.ConnectedPorts.FirstOrDefault() as CSPort)?.AccessSyntax();
        }

        public virtual IEnumerable<StatementSyntax> GetStatementSyntax()
        {
            return (ParentNode as Nodes.Base.CSNode)?.GetStatementSyntax(this);
        }

        public ExpressionSyntax AccessSyntax()
        {
            return (ParentNode as Nodes.Base.CSNode)?.AccessSyntax(this);
        }
    }
}
