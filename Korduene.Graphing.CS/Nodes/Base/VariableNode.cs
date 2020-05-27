using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class VariableNode : CSNode
    {
        public override string DefaultName
        {
            get
            {
                if (SymbolName?.Contains(".") == true)
                {
                    return SymbolName.Split('.').Last();
                }

                return SymbolName;
            }
        }

        public VariableNode()
        {
            this.NodeType = Enums.NodeType.Variable;
            //Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Value"));
        }

        public void AssignDataType(string symbolName)
        {
            foreach (var port in Ports.Cast<CSPort>())
            {
                port.SymbolName = symbolName;
            }
        }

        public ConnectionResult AssignValue(IPort port)
        {
            return Ports.First().Connect(port);
        }

        public void AssignValue(string value)
        {
            foreach (var port in Ports)
            {
                port.Value = value;
            }
        }

        //public ConnectionResult ConnectToValue(IPort port)
        //{
        //    return Ports.First().Connect(port);
        //}

        public override IEnumerable<SyntaxNode> GetSyntax()
        {
            var declerator = SyntaxFactory.VariableDeclarator(this.Name).WithInitializer(GetInitializer());

            //declerator = declerator.WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(Ports.First().Value.ToString()))));

            var field = SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(SymbolName))
                .WithVariables(SyntaxFactory.SingletonSeparatedList(declerator)))
                .WithModifiers(Utilities.SyntaxHelpers.GetModifiers(Modifiers)).NormalizeWhitespace();

            if (!string.IsNullOrWhiteSpace(this.Comment))
            {
                field = field.WithLeadingTrivia(SyntaxFactory.Comment($"//{this.Comment}"));
            }

            return new[] { field };
        }

        public override ExpressionSyntax AccessSyntax(CSPort sourcePort)
        {
            return SyntaxFactory.IdentifierName(this.Name);
        }

        public EqualsValueClauseSyntax GetInitializer()
        {
            var port = Ports.First() as CSPort;

            return SyntaxFactory.EqualsValueClause(GetValueLiteralExpression());
        }

        public LiteralExpressionSyntax GetValueLiteralExpression()
        {
            var type = Symbol as INamedTypeSymbol;

            var value = Ports.First().Value;

            if (Ports.First().ConnectedPorts.Any())
            {
                var connectedPort = Ports.First().ConnectedPorts.First() as CSPort;
                //return connectedPort.GetValueLiteralExpression();
            }

            //TODO: Check to see if port is connected if connected get the value from the other port

            if (value == null)
            {
                return SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);
            }
            else if (type.SpecialType != SpecialType.None)
            {
                switch (type.SpecialType)
                {
                    case SpecialType.None:
                        break;
                    case SpecialType.System_Object:
                        break;
                    case SpecialType.System_Enum:
                        break;
                    case SpecialType.System_MulticastDelegate:
                        break;
                    case SpecialType.System_Delegate:
                        break;
                    case SpecialType.System_ValueType:
                        break;
                    case SpecialType.System_Void:
                        break;
                    case SpecialType.System_Boolean:
                        if (value is bool b && b)
                        {
                            return SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression);
                        }
                        else
                        {
                            return SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression);
                        }
                    case SpecialType.System_Char:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToChar()));
                    case SpecialType.System_SByte:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToSByte()));
                    case SpecialType.System_Byte:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToByte()));
                    case SpecialType.System_Int16:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToInt16()));
                    case SpecialType.System_UInt16:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToUInt16()));
                    case SpecialType.System_Int32:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToInt32()));
                    case SpecialType.System_UInt32:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToUInt32()));
                    case SpecialType.System_Int64:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToInt64()));
                    case SpecialType.System_UInt64:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToUInt64()));
                    case SpecialType.System_Decimal:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToDecimal()));
                    case SpecialType.System_Single:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToSingle()));
                    case SpecialType.System_Double:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value.ToDouble()));
                    case SpecialType.System_String:
                        return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value?.ToString()));
                    case SpecialType.System_IntPtr:
                        break;
                    case SpecialType.System_UIntPtr:
                        break;
                    case SpecialType.System_Array:
                        break;
                    case SpecialType.System_Collections_IEnumerable:
                        break;
                    case SpecialType.System_Collections_Generic_IEnumerable_T:
                        break;
                    case SpecialType.System_Collections_Generic_IList_T:
                        break;
                    case SpecialType.System_Collections_Generic_ICollection_T:
                        break;
                    case SpecialType.System_Collections_IEnumerator:
                        break;
                    case SpecialType.System_Collections_Generic_IEnumerator_T:
                        break;
                    case SpecialType.System_Collections_Generic_IReadOnlyList_T:
                        break;
                    case SpecialType.System_Collections_Generic_IReadOnlyCollection_T:
                        break;
                    case SpecialType.System_Nullable_T:
                        break;
                    case SpecialType.System_DateTime:
                        break;
                    case SpecialType.System_Runtime_CompilerServices_IsVolatile:
                        break;
                    case SpecialType.System_IDisposable:
                        break;
                    case SpecialType.System_TypedReference:
                        break;
                    case SpecialType.System_ArgIterator:
                        break;
                    case SpecialType.System_RuntimeArgumentHandle:
                        break;
                    case SpecialType.System_RuntimeFieldHandle:
                        break;
                    case SpecialType.System_RuntimeMethodHandle:
                        break;
                    case SpecialType.System_RuntimeTypeHandle:
                        break;
                    case SpecialType.System_IAsyncResult:
                        break;
                    case SpecialType.System_AsyncCallback:
                        break;
                    case SpecialType.System_Runtime_CompilerServices_RuntimeFeature:
                        break;
                    default:
                        break;
                }
            }

            return SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);
        }

        public static VariableNode Create(string id, string name, string symbolName)
        {
            var node = new VariableNode()
            {
                Id = id,
                Name = name,
                SymbolName = symbolName
            };

            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Value") { SymbolName = symbolName });
            node.AssignDataType(symbolName);

            return node;
        }
    }
}
