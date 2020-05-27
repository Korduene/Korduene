using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class SetterNode : CSNode
    {
        private const string VARIABLE = "VARIABLE";
        private const string VALUE = "VALUE";
        public SetterNode()
        {
        }

        public ConnectionResult ConnectVariableTo(IPort port)
        {
            return this.Ports[2].Connect(port);
        }

        public ConnectionResult ConnectValueTo(IPort port)
        {
            return this.Ports[3].Connect(port);
        }

        public override IEnumerable<StatementSyntax> GetStatementSyntax(CSPort sourcePort)
        {
            var result = new List<StatementSyntax>();

            var variablePort = Ports.FirstOrDefault(x => x.HasProperty(VARIABLE)) as CSPort;

            var valuePort = Ports.FirstOrDefault(x => x.HasProperty(VALUE)) as CSPort;

            if (variablePort.ConnectedPorts.Any())
            {
                if (valuePort.ConnectedPorts.Any())
                {
                    result.Add(SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, variablePort.GetFirstConnectedPortMemberAccessExpressionSyntax(), valuePort.GetFirstConnectedPortMemberAccessExpressionSyntax())));
                }
                else
                {
                    result.Add(SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, variablePort.GetFirstConnectedPortMemberAccessExpressionSyntax(), SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression))));
                }
            }

            return result;
        }

        public static SetterNode Create()
        {
            var node = new SetterNode() { Name = "Setter" };

            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple, "Set") { IsPassThrough = true });
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Run") { IsPassThrough = true });
            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple, "Variable").WithProperty(VARIABLE, VARIABLE));
            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple, "Value").WithProperty(VALUE, VALUE));

            return node;
        }
    }
}
