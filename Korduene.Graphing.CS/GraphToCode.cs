using Korduene.Graphing.CS.Nodes.Base;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS
{
    public class GraphToCode
    {
        private CSGraph _graph;

        public GraphToCode(CSGraph graph)
        {
            _graph = graph;
        }

        public string ToCode()
        {
            var node = ToSyntaxTree().GetRoot();

            return node.NormalizeWhitespace().ToFullString();
        }

        public SyntaxTree ToSyntaxTree()
        {
            var usings = new List<UsingDirectiveSyntax>();

            foreach (var item in _graph.Usings)
            {
                usings.Add(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(item)));
            }

            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(_graph.Namespace));

            var classDeclaration = SyntaxFactory.ClassDeclaration(_graph.Name).WithModifiers(Utilities.SyntaxHelpers.GetModifiers(_graph.Modifiers));

            var syntaxNodes = new List<SyntaxNode>();

            foreach (var gNode in _graph.Members.OfType<CSNode>())
            {
                var syntax = gNode.GetSyntax();

                if (syntax != null)
                {
                    syntaxNodes.AddRange(syntax);
                }
            }

            var unit = SyntaxFactory.CompilationUnit()
                        .WithUsings(SyntaxFactory.List(usings))
                        .WithMembers(new SyntaxList<MemberDeclarationSyntax>(new MemberDeclarationSyntax[] { ns.WithMembers(new SyntaxList<MemberDeclarationSyntax>(classDeclaration.WithMembers(SyntaxFactory.List(syntaxNodes)))) }));

            return SyntaxFactory.SyntaxTree(unit);
        }
    }
}
