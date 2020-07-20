using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class ExternalMethodNode : CSNode
    {
        public override string DefaultName
        {
            get { return "ExternalMethod"; }
        }

        public ExternalMethodNode()
        {
            this.NodeType = Enums.NodeType.Method;
            //this.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple, "Run") { IsPassThrough = true, CallParent = true });
            //this.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Run") { IsPassThrough = true });
        }

        public ExternalMethodNode(string name) : this()
        {
            this.Name = name;
        }

        public override IEnumerable<SyntaxNode> GetSyntax()
        {
            var list = new List<SyntaxNode>();

            return list;
        }

        public override IEnumerable<StatementSyntax> GetStatementSyntax(CSPort sourcePort)
        {
            var list = new List<StatementSyntax>();

            var argumentList = SyntaxFactory.ArgumentList();

            foreach (var par in (Symbol as IMethodSymbol).Parameters)
            {
                if (this.Ports.FirstOrDefault(x => x.Text == par.Name) is CSPort port)
                {
                    if (port.ConnectedPorts.Any())
                    {
                        argumentList = argumentList.AddArguments(SyntaxFactory.Argument((port.ConnectedPorts.First() as CSPort).AccessSyntax()));
                    }
                    else
                    {
                        //TODO: get values properly, instead of passing everything as strings
                        argumentList = argumentList.AddArguments(SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.DefaultLiteralExpression, SyntaxFactory.Literal(port.Value?.ToString()))));
                    }
                }
            }

            var invocation = SyntaxFactory.InvocationExpression(
                            SyntaxFactory.MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                SyntaxFactory.IdentifierName(Symbol.ContainingType.ToString()), SyntaxFactory.IdentifierName(Symbol.Name))).WithArgumentList(argumentList);

            var syntaxNode = SyntaxFactory.ExpressionStatement(invocation);

            if (!string.IsNullOrWhiteSpace(this.Comment))
            {
                syntaxNode = syntaxNode.WithLeadingTrivia(SyntaxFactory.Comment($"// {this.Comment}"));
            }

            list.Add(syntaxNode);

            return list;
        }

        public override ExpressionSyntax AccessSyntax(CSPort sourcePort)
        {
            var argumentList = SyntaxFactory.ArgumentList();

            foreach (var par in (Symbol as IMethodSymbol).Parameters)
            {
                if (this.Ports.FirstOrDefault(x => x.Text == par.Name) is CSPort port)
                {
                    if (port.ConnectedPorts.Any())
                    {
                        argumentList = argumentList.AddArguments(SyntaxFactory.Argument((port.ConnectedPorts.First() as CSPort).AccessSyntax()));
                    }
                    else
                    {
                        argumentList = argumentList.AddArguments(SyntaxFactory.Argument(port.AccessSyntax()));
                    }
                }
            }

            var identifiers = this.Name.Split('.');

            var first = string.Join('.', identifiers.SkipLast(1));
            var last = identifiers.Last();

            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName(first), SyntaxFactory.IdentifierName(last))).WithArgumentList(argumentList);
        }

        public void Call(INode node)
        {
            //TODO: IMPROVE
            this.Ports[1].Connect(node.Ports.First(x => x.PortType == PortType.In));
        }

        public static ExternalMethodNode Create(string name, string symbolName, IEnumerable<IParameterSymbol> parameters, ITypeSymbol returnType)
        {
            var node = new ExternalMethodNode(name) { SymbolName = symbolName };

            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple, "Run") { IsPassThrough = true });
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Run") { IsPassThrough = true });

            if (parameters != null)
            {
                foreach (var par in parameters)
                {
                    node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.One, par.Name, par.Type.ToDisplayString()));
                }
            }

            if (returnType != null)
            {
                var isCol = returnType.Kind == SymbolKind.ArrayType || (returnType.SpecialType != SpecialType.System_String && returnType.AllInterfaces.Any(x => x.Name == "IEnumerable"));
                node.Ports.Insert(1, new CSPort(PortType.Out, AcceptedConnections.Multiple, "Return", returnType.ToDisplayString()) { IsCollection = isCol });
            }

            return node;
        }
    }
}
