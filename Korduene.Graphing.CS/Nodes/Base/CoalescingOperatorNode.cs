using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class CoalescingOperatorNode : CSNode
    {
        private const string VALUE_1 = "VALUE_1";
        private const string VALUE_2 = "VALUE_2";
        private const string GET = "GET";

        public override ExpressionSyntax AccessSyntax(CSPort sourcePort)
        {
            if (!sourcePort.HasProperty(GET))
            {
                return null;
            }

            var val1Port = Ports.FirstOrDefault(x => x.HasProperty(VALUE_1)) as CSPort;
            var val2Port = Ports.FirstOrDefault(x => x.HasProperty(VALUE_2)) as CSPort;

            var pAccess1 = val1Port.GetFirstConnectedPortMemberAccessExpressionSyntax();
            var pAccess2 = val2Port.GetFirstConnectedPortMemberAccessExpressionSyntax();

            if (pAccess1 == null || pAccess2 == null)
            {
                return null;
            }

            return SyntaxFactory.AssignmentExpression(SyntaxKind.CoalesceAssignmentExpression, val1Port.GetFirstConnectedPortMemberAccessExpressionSyntax(), val2Port.GetFirstConnectedPortMemberAccessExpressionSyntax());
        }

        public static CoalescingOperatorNode Create()
        {
            var node = new CoalescingOperatorNode() { Name = "??" };
            node.Ports.Add(new CSPort(Enums.PortType.In, Enums.AcceptedConnections.One, "Value 1").WithProperty(VALUE_1, VALUE_1));
            node.Ports.Add(new CSPort(Enums.PortType.In, Enums.AcceptedConnections.One, "Value 2").WithProperty(VALUE_2, VALUE_2));
            node.Ports.Add(new CSPort(Enums.PortType.Out, Enums.AcceptedConnections.Multiple, "Get").WithProperty(GET, GET));

            return node;
        }
    }
}
