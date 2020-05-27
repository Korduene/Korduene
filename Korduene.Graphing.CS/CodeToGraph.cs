using Korduene.Graphing.CS.Nodes.Base;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS
{
    public class CodeToGraph : CSharpSyntaxWalker
    {
        private CSharpCompilation _compilation;
        private CSGraph _graph;
        private CSharpSyntaxNode _currentNode;
        private IEnumerable<TextSpan> _changes;

        public CodeToGraph(CSGraph graph)
        {
            _graph = graph;
        }

        public void UpdateGraph()
        {
            return;

            _graph.IsLoading = true;

            var tree = CSharpSyntaxTree.ParseText(_graph.Code);
            var root = tree.GetRoot();

            if (root.ContainsDiagnostics)
            {
                _graph.IsLoading = false;
                return;
            }

            if (!IsCompilable(tree))
            {
                return;
            }

            if (_graph.LastSyntaxTree == null)
            {
                _graph.LastSyntaxTree = tree;
            }
            else
            {
                //= tree.GetChanges(_graph.LastSyntaxTree);
                _changes = tree.GetChangedSpans(_graph.LastSyntaxTree);
            }

            this.Visit(root);

            _graph.LastSyntaxTree = tree;

            _graph.IsLoading = false;
        }

        public override void DefaultVisit(SyntaxNode node)
        {
            base.DefaultVisit(node);
        }

        private bool IsCompilable(SyntaxTree tree)
        {
            if (_compilation == null)
            {
                _compilation = CSharpCompilation.Create("test", null, _graph.Document.Project.MetadataReferences, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            }

            _compilation = _compilation.RemoveAllSyntaxTrees();
            _compilation = _compilation.AddSyntaxTrees(new[] { tree });

            var diag = _compilation.GetDiagnostics().Where(x => x.Severity == DiagnosticSeverity.Error).ToList();

            return diag.Count < 1;
        }

        //public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        //{
        //    var old = _graph.LastSyntaxTree.GetRoot().FindNode(node.Span) as VariableDeclarationSyntax;

        //    foreach (var variable in node.Variables)
        //    {
        //        VariableNode gNode = null;

        //        if (old != null)
        //        {
        //            var oldVar = old.FindNode(variable.Span) as VariableDeclaratorSyntax;

        //            if (oldVar != null)
        //            {
        //                _graph.Nodes.FirstOrDefault(x => x.Name == oldVar.Identifier.)
        //            }
        //        }

        //        if (_graph.Nodes.Any(x => x.Name.Equals(variable.Identifier.ValueText)))
        //        {
        //            continue;
        //        }

        //        var dataType = string.Empty;

        //        if (node.Type is PredefinedTypeSyntax predefinedTypeSyntax)
        //        {
        //            dataType = predefinedTypeSyntax.Keyword.Text;
        //        }

        //        var gNode = VariableNode.Create(variable.Identifier.ValueText, variable.Identifier.ValueText, dataType);
        //        _graph.Members.Add(gNode);
        //    }

        //    base.VisitVariableDeclaration(node);
        //}

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            var dataType = (node.Initializer.Value as LiteralExpressionSyntax).Token.ValueText;

            var gNode = VariableNode.Create(node.Identifier.ValueText, node.Identifier.ValueText, dataType);
            _graph.Members.Add(gNode);

            base.VisitVariableDeclarator(node);
        }

        //public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        //{
        //    foreach (var variable in node.Variables)
        //    {
        //        var dataType = string.Empty;

        //        if (node.Type is PredefinedTypeSyntax predefinedTypeSyntax)
        //        {
        //            dataType = predefinedTypeSyntax.Keyword.Text;
        //        }

        //        var variableNode = new VariableNode(variable.Identifier.ValueText, variable.Identifier.ValueText, dataType);

        //        if (variable.Initializer is EqualsValueClauseSyntax equalsValueClauseSyntax)
        //        {
        //            if (equalsValueClauseSyntax.Value is IdentifierNameSyntax identifierNameSyntax)
        //            {
        //                variableNode.AssignValue(_graph.GetPort(identifierNameSyntax.Identifier.ValueText, Enums.PortType.Out));
        //            }
        //            else
        //            {
        //                //TODO: different value types need to be handled here
        //                variableNode.AssignValue(equalsValueClauseSyntax.Value.ToFullString());
        //            }
        //        }

        //        _graph.Members.Add(variableNode);
        //    }

        //    base.VisitVariableDeclaration(node);
        //}

        //public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        //{
        //    _currentNode = node;

        //    var methodNode = new MethodDeclerationNode() { Id = node.Identifier.ValueText, Name = node.Identifier.ValueText };

        //    foreach (var par in node.ParameterList.Parameters)
        //    {
        //        methodNode.Ports.Add(new CSPort(PortType.In, AcceptedConnections.One)
        //        {
        //            Id = par.Identifier.ValueText,
        //            SymbolName = par.Type.ToString(),
        //            Text = par.Identifier.ValueText,
        //            //Value = par.Defaulpt.Value
        //        });

        //        methodNode.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.One)
        //        {
        //            Id = par.Identifier.ValueText,
        //            SymbolName = par.Type.ToString(),
        //            Text = par.Identifier.ValueText,
        //            //Value = par.Default.Value
        //        });
        //    }

        //    _graph.Members.Add(methodNode);

        //    base.VisitMethodDeclaration(node);
        //}

        //public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        //{
        //    if (node.Expression is InvocationExpressionSyntax invocationExpression)
        //    {
        //        if (invocationExpression.Expression is IdentifierNameSyntax identifierName)
        //        {
        //            var n = _graph.Nodes.FirstOrDefault(x => x.Id == identifierName.Identifier.ValueText);

        //            if (n is MethodDeclerationNode methodNode)
        //            {
        //                if (_currentNode is MethodDeclarationSyntax methodDeclarationSyntax)
        //                {
        //                    if (_graph.Nodes.FirstOrDefault(x => x.Id == methodDeclarationSyntax.Identifier.ValueText) is MethodDeclerationNode curNode)
        //                    {
        //                        curNode.Call(methodNode);
        //                    }
        //                }
        //            }

        //            foreach (var arg in invocationExpression.ArgumentList.Arguments)
        //            {
        //                if (arg.Expression is IdentifierNameSyntax argIdentifier)
        //                {

        //                }
        //            }

        //        }
        //    }

        //    base.VisitExpressionStatement(node);
        //}

        //public override void VisitBlock(BlockSyntax node)
        //{
        //    base.VisitBlock(node);
        //}

        //public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
        //{
        //    var setterNode = new SetterNode();
        //    _graph.Members.Add(setterNode);

        //    if (_currentNode is MethodDeclarationSyntax parentMethod)
        //    {
        //        var parentNode = _graph.Members.OfType<MethodDeclerationNode>().FirstOrDefault(x => x.Id == parentMethod.Identifier.ValueText);
        //        parentNode.Call(setterNode);
        //    }

        //    if (node.Left is IdentifierNameSyntax leftIdentifierNameSyntax)
        //    {
        //        setterNode.ConnectVariableTo(_graph.GetPort(leftIdentifierNameSyntax.Identifier.Text, Enums.PortType.Out));
        //    }

        //    if (node.Right is IdentifierNameSyntax rightIdentifierNameSyntax)
        //    {
        //        setterNode.ConnectValueTo(_graph.GetPort(rightIdentifierNameSyntax.Identifier.Text, Enums.PortType.Out));
        //    }

        //    base.VisitAssignmentExpression(node);
        //}
    }
}
