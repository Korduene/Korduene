using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class MethodDeclerationNode : CSNode
    {
        public override string DefaultName
        {
            get { return "NewMethod"; }
        }

        public MethodDeclerationNode()
        {
            this.SymbolName = "void";
            this.NodeType = Enums.NodeType.Method;
            //this.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple) { IsPassThrough = true, CallParent = true });
            //this.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple) { IsPassThrough = true });
        }

        public override IEnumerable<SyntaxNode> GetSyntax()
        {
            var method = SyntaxFactory.MethodDeclaration(
            SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
            SyntaxFactory.Identifier(this.Name))
            .WithModifiers(Utilities.SyntaxHelpers.GetModifiers(Modifiers));

            if (!string.IsNullOrWhiteSpace(this.Comment))
            {
                method = method.WithLeadingTrivia(GetDocumentationTriviaSyntax());
            }

            var block = SyntaxFactory.Block();

            foreach (var port in Ports.Where(x => x.PortType == PortType.Out))
            {
                foreach (var connectedPort in port.ConnectedPorts)
                {
                    //TODO: PORTS NEED TO DO THEIR OWN STATEMENT SYNTAXES

                    if (connectedPort.ParentNode != null)
                    {
                        block = block.AddStatements((connectedPort as CSPort).GetStatementSyntax().ToArray());
                    }
                }
            }

            method = method.WithBody(block);

            return new[] { method };
        }

        public override IEnumerable<StatementSyntax> GetStatementSyntax(CSPort sourcePort)
        {
            var list = new List<StatementSyntax>();

            var argumentList = SyntaxFactory.ArgumentList();

            //foreach (var par in Symbol.Parameters)
            //{
            //    if (this.Ports.FirstOrDefault(x => x.Text == par.Name) is CSPort port)
            //    {
            //        if (port.ConnectedPorts.Any())
            //        {
            //            argumentList = argumentList.AddArguments(SyntaxFactory.Argument((port.ConnectedPorts.First() as CSPort).GetMemberAccessExpressionSyntax()));
            //        }
            //        else
            //        {
            //            argumentList = argumentList.AddArguments(SyntaxFactory.Argument(port.GetValueLiteralExpression()));
            //        }
            //    }
            //}

            InvocationExpressionSyntax invocation = null;

            if (argumentList.Arguments.Any())
            {
                invocation = SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName(this.Name), SyntaxFactory.IdentifierName(Symbol.Name))).WithArgumentList(argumentList);
            }
            else
            {
                invocation = SyntaxFactory.InvocationExpression(SyntaxFactory.IdentifierName(this.Name));
            }

            var syntaxNode = SyntaxFactory.ExpressionStatement(invocation);

            if (!string.IsNullOrWhiteSpace(this.Comment))
            {
                syntaxNode = syntaxNode.WithLeadingTrivia(SyntaxFactory.Comment($"// {this.Comment}"));
            }

            list.Add(syntaxNode);

            return list;
        }

        public void Call(INode node)
        {
            //TODO: IMPROVE
            this.Ports[1].Connect(node.Ports.First(x => x.PortType == PortType.In));
        }

        public static MethodDeclerationNode Create()
        {
            var node = new MethodDeclerationNode();
            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple) { IsPassThrough = true });
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple) { IsPassThrough = true });

            return node;
        }
    }
}
