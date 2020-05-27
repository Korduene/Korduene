using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class ReturnNode : CSNode
    {
        private const string RETURN = "RETURN";
        private const string RETURN_ITEM = "RETURN_ITEM";

        public ReturnNode()
        {
            this.Name = "Return";
            this.NodeType = NodeType.Return;
        }

        public override IEnumerable<StatementSyntax> GetStatementSyntax(CSPort sourcePort)
        {
            var returnPort = Ports.FirstOrDefault(x => x.HasProperty(RETURN_ITEM)) as CSPort;

            if (returnPort.ConnectedPorts.Any())
            {
                var result = SyntaxFactory.ReturnStatement(returnPort.GetFirstConnectedPortMemberAccessExpressionSyntax());

                return new[] { result };
            }
            else
            {
                return new[] { SyntaxFactory.ReturnStatement() };
            }
        }

        public static ReturnNode Create()
        {
            var node = new ReturnNode();

            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.One, "Return"));
            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.One, "Value").WithProperty(RETURN_ITEM, RETURN_ITEM));

            return node;
        }
    }
}
