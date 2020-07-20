using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class CreateInstanceNode : CSNode
    {
        private const string RETURN = "RETURN";
        private const string RETURN_ITEM = "RETURN_ITEM";

        public CreateInstanceNode()
        {
            this.Name = "Instance";
            this.NodeType = NodeType.Return;
        }
        //#error create instance should specify costructors as a sub menu
        public override IEnumerable<StatementSyntax> GetStatementSyntax(CSPort sourcePort)
        {
            return new[] { SyntaxFactory.ExpressionStatement(SyntaxFactory.ObjectCreationExpression(SyntaxFactory.ParseTypeName(SymbolName)).WithArgumentList(SyntaxFactory.ArgumentList())) };
        }

        public override ExpressionSyntax AccessSyntax(CSPort sourcePort)
        {
            return SyntaxFactory.ObjectCreationExpression(SyntaxFactory.ParseTypeName(SymbolName)).WithArgumentList(SyntaxFactory.ArgumentList());
        }

        public static CreateInstanceNode Create(string symbolName)
        {
            var node = new CreateInstanceNode() { SymbolName = symbolName };

            node.Name = $"{node.Symbol.Name} Instance";

            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.One, "Create"));
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.One, "Get").WithProperty(RETURN_ITEM, RETURN_ITEM));

            return node;
        }
    }
}
