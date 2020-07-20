using Korduene.Graphing.CS.Utilities;
using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SymbolName = "void";
        }

        public override IEnumerable<SyntaxNode> GetSyntax()
        {
            var method = SyntaxFactory.MethodDeclaration
            (
                SyntaxFactory.PredefinedType
                (
                    SyntaxFactory.Token(SyntaxKind.VoidKeyword)
                ),
                SyntaxFactory.Identifier(Name)
            )
            .WithModifiers(SyntaxHelpers.GetModifiers(Modifiers));

            if (!string.IsNullOrWhiteSpace(Comment))
            {
                method = method.WithLeadingTrivia(GetDocumentationTriviaSyntax());
            }

            var block = SyntaxFactory.Block();

            foreach (var port in Ports.Where(x => x.PortType == Enums.PortType.Out))
            {
                foreach (var connectedPort in port.ConnectedPorts.Cast<CSPort>())
                {
                    block = block.AddStatements(connectedPort.GetStatementSyntax().ToArray());
                }
            }

            return new[] { method.WithBody(block) };
        }

        public override IEnumerable<StatementSyntax> GetStatementSyntax(CSPort sourcePort)
        {
            var args = SyntaxFactory.ArgumentList();

            //foreach (var par in (Symbol as IMethodSymbol).Parameters)
            //{
            //    if (Ports.FirstOrDefault(x => x.Text == par.Name) is CSPort port)
            //    {
            //        if (port.IsConnected)
            //        {
            //            args = args.AddArguments(SyntaxFactory.Argument(port.GetFirstConnectedPortMemberAccessExpressionSyntax()));
            //        }
            //        else
            //        {
            //            args = args.AddArguments(SyntaxFactory.Argument(port.GetValueLiteralExpression()));
            //        }
            //    }
            //}

            var invocation = SyntaxFactory.InvocationExpression(SyntaxFactory.IdentifierName(Name));

            if (args.Arguments.Any())
            {
                invocation = invocation.WithArgumentList(args);
            }

            var syntaxNode = SyntaxFactory.ExpressionStatement(invocation);

            if (!string.IsNullOrWhiteSpace(Comment))
            {
                syntaxNode = syntaxNode.WithLeadingTrivia(GetCommentSyntaxTrivia());
            }

            return new[] { syntaxNode };
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
