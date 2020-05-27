using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS.Nodes.Base
{
    public class ForEachNode : CSNode
    {
        private const string BODY_PROPERTY = "BODY";
        private const string COLLECTION_PROPERTY = "COLLECTION";
        private const string CURRENT_PROPERTY = "CURRENT";
        private const string COMPLETE_PROPERTY = "COMPLETE";
        private string _itemName = "item";

        public override string DefaultName
        {
            get { return "ForEach"; }
        }

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (_itemName != value)
                {
                    _itemName = value;

                    if (Ports.FirstOrDefault(x => x.HasProperty(CURRENT_PROPERTY)) is CSPort port)
                    {
                        port.Text = value;
                    }

                    OnPropertyChanged();
                }
            }
        }

        public ForEachNode()
        {
        }

        public override IEnumerable<StatementSyntax> GetStatementSyntax(CSPort sourcePort)
        {
            var statements = new List<StatementSyntax>();
            var block = SyntaxFactory.Block();
            var complete = new List<StatementSyntax>();

            var bodyPort = Ports.FirstOrDefault(x => x.HasProperty(BODY_PROPERTY));
            var collectionPort = Ports.FirstOrDefault(x => x.HasProperty(COLLECTION_PROPERTY)) as CSPort;
            var completePort = Ports.FirstOrDefault(x => x.HasProperty(COMPLETE_PROPERTY)) as CSPort;

            foreach (var connectedPort in bodyPort.ConnectedPorts.Cast<CSPort>())
            {
                block = block.AddStatements(connectedPort.GetStatementSyntax().ToArray());
            }

            foreach (var connectedPort in completePort.ConnectedPorts.Cast<CSPort>())
            {
                complete.AddRange(connectedPort.GetStatementSyntax().ToArray());
            }

            var colRef = collectionPort.GetFirstConnectedPortMemberAccessExpressionSyntax();

            if (colRef == null)
            {
                return new[] { SyntaxFactory.EmptyStatement() };
                //return new[] { SyntaxFactory.ForEachStatement(SyntaxFactory.ParseTypeName("var"), "item", SyntaxFactory.LiteralExpression(SyntaxKind.ExpressionStatement, SyntaxFactory.Literal("collection")), block) };
            }

            statements.Add(SyntaxFactory.ForEachStatement(SyntaxFactory.ParseTypeName("var"), "item", collectionPort.GetFirstConnectedPortMemberAccessExpressionSyntax(), block));

            statements.AddRange(complete);

            return statements;
        }

        public static ForEachNode Create()
        {
            var node = new ForEachNode
            {
                Name = "ForEach"
            };

            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple, "Run") { IsPassThrough = true });
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Body").WithProperty(BODY_PROPERTY, BODY_PROPERTY));
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "item").WithProperty(CURRENT_PROPERTY, CURRENT_PROPERTY));
            node.Ports.Add(new CSPort(PortType.Out, AcceptedConnections.Multiple, "Complete") { IsPassThrough = true }.WithProperty(COMPLETE_PROPERTY, COMPLETE_PROPERTY));
            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.One, "Collection").WithProperty(COLLECTION_PROPERTY, COLLECTION_PROPERTY));
            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple, "Break"));
            node.Ports.Add(new CSPort(PortType.In, AcceptedConnections.Multiple, "Continue"));

            return node;
        }
    }
}
