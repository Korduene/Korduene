using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class PropertyNode : CSNode
    {
        private const string GET_PROPERTY = "GET";
        private const string SET_PROPERTY = "SET";
        private const string GET_VALUE_PROPERTY = "GET_VALUE";
        private const string SET_VALUE_PROPERTY = "SET_VALUE";

        public override string DefaultName
        {
            get { return "Property"; }
        }

        public PropertyNode()
        {
            Modifiers = "public";
        }

        public override IEnumerable<SyntaxNode> GetSyntax()
        {
            var syntaxNodes = new List<SyntaxNode>();
            var getBlock = SyntaxFactory.Block();
            var setBlock = SyntaxFactory.Block();

            var getPort = Ports.FirstOrDefault(x => x.HasProperty(GET_PROPERTY));
            var setPort = Ports.FirstOrDefault(x => x.HasProperty(SET_PROPERTY));

            var property = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(SymbolName), Name).WithModifiers(Utilities.SyntaxHelpers.GetModifiers(Modifiers));

            var accessors = SyntaxFactory.AccessorList();

            if (getPort.ConnectedPorts.Any())
            {
                foreach (var connectedPort in getPort.ConnectedPorts.Cast<CSPort>())
                {
                    getBlock = getBlock.AddStatements(connectedPort.GetStatementSyntax().ToArray());
                }

                accessors = accessors.AddAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithBody(getBlock));
            }
            else
            {
                accessors = accessors.AddAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            }

            if (setPort.ConnectedPorts.Any())
            {
                foreach (var connectedPort in setPort.ConnectedPorts.Cast<CSPort>())
                {
                    setBlock = setBlock.AddStatements(connectedPort.GetStatementSyntax().ToArray());
                }

                accessors = accessors.AddAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithBody(setBlock));
            }
            else
            {
                accessors = accessors.AddAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            }

            property = property.WithAccessorList(accessors);

            if (!string.IsNullOrWhiteSpace(this.Comment))
            {
                property = property.WithLeadingTrivia(GetDocumentationTriviaSyntax());
            }

            syntaxNodes.Add(property);

            //var collectionPort = Ports.FirstOrDefault(x => x.HasProperty(COLLECTION_PROPERTY)) as CSPort;
            //var completePort = Ports.FirstOrDefault(x => x.HasProperty(COMPLETE_PROPERTY)) as CSPort;

            //foreach (var connectedPort in getPort.ConnectedPorts.Cast<CSPort>())
            //{
            //    getBlock = getBlock.AddStatements(connectedPort.GetStatementSyntax().ToArray());
            //}

            //foreach (var connectedPort in completePort.ConnectedPorts.Cast<CSPort>())
            //{
            //    complete.AddRange(connectedPort.GetStatementSyntax().ToArray());
            //}

            //var colRef = collectionPort.GetFirstConnectedPortMemberAccessExpressionSyntax();

            //if (colRef == null)
            //{
            //    return new[] { SyntaxFactory.EmptyStatement() };
            //    //return new[] { SyntaxFactory.ForEachStatement(SyntaxFactory.ParseTypeName("var"), "item", SyntaxFactory.LiteralExpression(SyntaxKind.ExpressionStatement, SyntaxFactory.Literal("collection")), block) };
            //}

            //statements.Add(SyntaxFactory.ForEachStatement(SyntaxFactory.ParseTypeName("var"), "item", collectionPort.GetFirstConnectedPortMemberAccessExpressionSyntax(), getBlock));

            //statements.AddRange(complete);

            return syntaxNodes;
        }

        public override ExpressionSyntax AccessSyntax(CSPort sourcePort)
        {
            if (sourcePort.HasProperty(SET_VALUE_PROPERTY))
            {
                return SyntaxFactory.IdentifierName("value");
            }

            return SyntaxFactory.IdentifierName(this.Name);
        }

        public static PropertyNode Create(ITypeSymbol typeSymbol)
        {
            var node = new PropertyNode
            {
                Name = "Property",
                SymbolName = typeSymbol.ToString()
            };

            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Get (value)").WithProperty(GET_VALUE_PROPERTY, GET_VALUE_PROPERTY));
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Get").WithProperty(GET_PROPERTY, GET_PROPERTY));
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Set").WithProperty(SET_PROPERTY, SET_PROPERTY));
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Set (value)").WithProperty(SET_VALUE_PROPERTY, SET_VALUE_PROPERTY));

            return node;
        }
    }
}
