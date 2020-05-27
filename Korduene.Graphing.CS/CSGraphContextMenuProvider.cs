using Korduene.Graphing.CS.Nodes.Base;
using Korduene.Graphing.Enums;
using Korduene.UI;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Korduene.Graphing.CS
{
    public class CSGraphContextMenuProvider : GraphContextMenuProvider
    {
        public IGraph Graph { get; }
        public Document Document { get; }

        private List<INamedTypeSymbol> _types = null;
        private IEnumerable<string> _ignoredNamespaces;

        private ICommand _createVariableCommand;
        private ICommand _createPropertyCommand;
        private ICommand _createInstanceCommand;

        public ICommand CreateVariableCommand
        {
            get { return _createVariableCommand ??= new KCommand<GraphMenuItem>(ExecuteCreateVariableCommand); }
        }

        public ICommand CreatePropertyCommand
        {
            get { return _createPropertyCommand ??= new KCommand<GraphMenuItem>(ExecuteCreatePropertyCommand); }
        }

        public ICommand CreateInstanceCommand
        {
            get { return _createInstanceCommand ??= new KCommand<GraphMenuItem>(ExecuteCreateInstanceCommand); }
        }

        public CSGraphContextMenuProvider(IGraph graph, IGraphContextMenuControl contextMenuControl, Document document) : base(graph, contextMenuControl)
        {
            this.Graph = graph;
            this.Document = document;
            Load();
        }

        public void Load()
        {
            var cache = SymbolProvider.GetProjectSymbolCache(Document.Project.Name);

            _types = cache.GetSymbols().Cast<INamedTypeSymbol>().ToList();

            var ignoredNsTxt = Path.Combine(Path.GetDirectoryName(Document.Project.FilePath), "IgnoredNamespaces.txt");

            if (File.Exists(ignoredNsTxt))
            {
                _ignoredNamespaces = File.ReadLines(ignoredNsTxt);

                _types = _types.Where(x => x?.ContainingNamespace != null && !_ignoredNamespaces.Contains(x.ContainingNamespace.ToString())).GroupBy(x => x.ToString()).Select(x => x.First()).OrderBy(x => x.ToString()).ToList();
            }

            var namespaces = _types.Where(x => x != null).Select(x => x.ContainingNamespace.ToString()).Distinct().ToList();
            namespaces.RemoveAll(x => string.IsNullOrWhiteSpace(x));
            namespaces = namespaces.OrderBy(x => x).ToList();

            var organizedNamespaces = Korduene.Utilities.NamespaceGen.FromStrings(namespaces);

            var items = new GraphMenuItemCollection((GraphMenuItem)null);

            AddNodes(items, organizedNamespaces);

            this.Items = items;

            AddSpecialItems();

            this.FlatItems.CacheSearchableContent();
        }

        public override void AddNode(GraphMenuItem nodeTreeViewItem)
        {
            if (nodeTreeViewItem.TypeInfo is INamespaceSymbol)
            {
                return;
            }

            if (nodeTreeViewItem.TypeInfo is NodeType nodeType)
            {
                CreateSpecialNode(nodeType);
                return;
            }

            if (nodeTreeViewItem.TypeInfo is INamedTypeSymbol namedTypeSymbol)
            {
                if (namedTypeSymbol.SpecialType == SpecialType.System_Void)
                {
                    AddNode(MethodDeclerationNode.Create());
                }
                else
                {
                    AddNode(VariableNode.Create(namedTypeSymbol.ToDisplayString(), null, namedTypeSymbol.ToString()));
                }
            }
            else if (nodeTreeViewItem.TypeInfo is IMethodSymbol methodSymbol)
            {
                AddNode(ExternalMethodNode.Create($"{methodSymbol.ContainingType.Name}.{methodSymbol.Name}", methodSymbol.ToString(), methodSymbol.Parameters, methodSymbol.ReturnType));
            }
            //else if (nodeTreeViewItem.TypeInfo is Void)
            //{

            //}

            //base.AddNode(nodeTreeViewItem);
        }

        private void AddNodes(ObservableCollection<GraphMenuItem> nodeTreeViewItems, IEnumerable<Korduene.Utilities.NamespaceGen> namespaces)
        {
            foreach (var ns in namespaces)
            {
                var typeInfo = _types.Find(x => x.ContainingNamespace.ToString() == ns.FullName || x.ContainingNamespace.ToString().StartsWith(ns.FullName + "."))?.ContainingNamespace;

                var node = new GraphMenuItem(ns.NameOnLevel) { TypeInfo = typeInfo };

                AddNodes(node.Items, ns.Subnamespaces);

                foreach (var item in _types.Where(x => x.ContainingNamespace.ToString().Equals(ns.FullName)).OrderBy(x => x.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)))
                {
                    var typeNode = CreateMenuItem(item);
                    node.Items.Add(typeNode);
                    this.FlatItems.Add(typeNode);
                    if (item.TypeKind == TypeKind.Class)
                    {
                        var members = item.GetMembers();
                        foreach (var m in members)
                        {
                            if (m is IMethodSymbol methodSymbol && methodSymbol.MethodKind == MethodKind.Ordinary && methodSymbol.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Public && methodSymbol.IsStatic)
                            {
                                var method = new GraphMenuItem(m.Name, m.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat.RemoveMemberOptions(SymbolDisplayMemberOptions.IncludeContainingType).RemoveMemberOptions(SymbolDisplayMemberOptions.IncludeType))) { TypeInfo = m };
                                //this.FlatItems.Add(method);
                                typeNode.Items.Add(method);
                            }
                            else if (m is IPropertySymbol propertySymbol && propertySymbol.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Public && propertySymbol.IsStatic)
                            {
                                var property = new GraphMenuItem(m.Name, m.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat.RemoveMemberOptions(SymbolDisplayMemberOptions.IncludeContainingType).RemoveMemberOptions(SymbolDisplayMemberOptions.IncludeType))) { TypeInfo = m };
                                //this.FlatItems.Add(method);
                                typeNode.Items.Add(property);
                            }
                        }
                    }
                }

                nodeTreeViewItems.Add(node);
            }
        }

        private GraphMenuItem CreateMenuItem(INamedTypeSymbol typeDefinition)
        {
            return new GraphMenuItem(typeDefinition.Name, typeDefinition.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)) { TypeInfo = typeDefinition };
        }

        private void CreateSpecialNode(NodeType nodeType)
        {
            if (nodeType == NodeType.ForEach)
            {
                AddNode(ForEachNode.Create());
            }
            else if (nodeType == NodeType.Method)
            {
                AddNode(MethodDeclerationNode.Create());
            }
            else if (nodeType == NodeType.Return)
            {
                AddNode(ReturnNode.Create());
            }
            else if (nodeType == NodeType.Setter)
            {
                AddNode(SetterNode.Create());
            }
            else if (nodeType == NodeType.Coalesce)
            {
                AddNode(CoalescingOperatorNode.Create());
            }
        }

        private void AddNode(INode node)
        {
            node.Location = new GraphPoint(Graph.LastMouseDownPosition.X, Graph.LastMouseDownPosition.Y);
            Graph.Members.Add(node);
        }

        private void AddSpecialItems()
        {
            this.Items.Add(new GraphMenuItem("ForEach", "ForEach") { TypeInfo = NodeType.ForEach });
            this.Items.Add(new GraphMenuItem("Method", "Method") { TypeInfo = NodeType.Method });
            this.Items.Add(new GraphMenuItem("Return", "Return") { TypeInfo = NodeType.Return });
            this.Items.Add(new GraphMenuItem("Setter", "Setter") { TypeInfo = NodeType.Setter });
            this.Items.Add(new GraphMenuItem("Coalesce", "Coalesce") { TypeInfo = NodeType.Coalesce });
        }

        public void ExecuteCreatePropertyCommand(GraphMenuItem obj)
        {
            if (obj.TypeInfo is INamedTypeSymbol namedTypeSymbol)
            {
                AddNode(PropertyNode.Create(namedTypeSymbol));
            }
        }

        public void ExecuteCreateVariableCommand(GraphMenuItem obj)
        {
            if (obj.TypeInfo is INamedTypeSymbol namedTypeSymbol)
            {
                AddNode(VariableNode.Create(namedTypeSymbol.ToDisplayString(), null, namedTypeSymbol.ToString()));
            }
        }

        public void ExecuteCreateInstanceCommand(GraphMenuItem obj)
        {
            if (obj.TypeInfo is INamedTypeSymbol namedTypeSymbol)
            {
                AddNode(CreateInstanceNode.Create(namedTypeSymbol.ToString()));
            }
        }
    }
}
