using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Korduene.Graphing.CS
{
    public class Grapher
    {
        private IGraph _graph;

        public static Grapher Instance { get; }

        static Grapher()
        {
            Instance = new Grapher();
        }

        public void UpdateGraph(IGraph graph, string code)
        {
            _graph = graph;
            _graph.Members.Clear(); //this is temporary

            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();

            var compilation = CSharpCompilation.Create("c", new[] { tree });
            var model = compilation.GetSemanticModel(tree, true);

            var method = root.DescendantNodes().OfType<MethodDeclarationSyntax>().FirstOrDefault();

            var cfg = Microsoft.CodeAnalysis.FlowAnalysis.ControlFlowGraph.Create(method, model);
        }
    }
}
