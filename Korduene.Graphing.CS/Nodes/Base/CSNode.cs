using Korduene.Graphing.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Korduene.Graphing.CS.Nodes.Base
{
    [DebuggerDisplay("Node, {Name}")]
    public class CSNode : Node
    {
        private string _symbolName;
        private string _modifiers = "private";

        [JsonIgnore]
        private ISymbol _symbol;

        public NodeType NodeType { get; set; }

        public string Modifiers
        {
            get { return _modifiers; }
            set
            {
                if (_modifiers != value)
                {
                    _modifiers = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SymbolName
        {
            get { return _symbolName; }
            set
            {
                if (_symbolName != value)
                {
                    _symbolName = value;
                    _symbol = null;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Symbol));
                }
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public ISymbol Symbol
        {
            get
            {
                if (_symbol == null)
                {
                    _symbol = SymbolProvider.GetSymbol(SymbolName);
                }

                return _symbol;
            }
        }

        public override GraphColor HeaderColor
        {
            get
            {
                switch (NodeType)
                {
                    case NodeType.Variable:
                        return new GraphColor(System.Drawing.Color.Purple.A, System.Drawing.Color.Purple.R, System.Drawing.Color.Purple.G, System.Drawing.Color.Purple.B);
                    case NodeType.Method:
                        return new GraphColor(System.Drawing.Color.SeaGreen.A, System.Drawing.Color.SeaGreen.R, System.Drawing.Color.SeaGreen.G, System.Drawing.Color.SeaGreen.B);
                    default:
                        return base.HeaderColor;
                }
            }

            set
            {
                base.HeaderColor = value;
            }
        }

        /// <summary>
        /// Gets the documentation trivia syntax.
        /// </summary>
        /// <returns></returns>
        public virtual SyntaxTriviaList GetDocumentationTriviaSyntax()
        {
            var comment = "/// <summary>\r\n";
            comment += $"/// {this.Comment}\r\n";
            comment += "/// </summary>\r\n";

            var par = GetParameterPorts();

            if (par != null)
            {
                foreach (var item in par)
                {
                    comment += $"/// <param name=\"{item.Text}\">{item.Comment}</param>";
                }
            }

            return SyntaxFactory.ParseLeadingTrivia(comment);
        }

        public virtual SyntaxTrivia GetCommentSyntaxTrivia()
        {
            return SyntaxFactory.Comment($"// {Comment}");
        }

        public virtual IEnumerable<IPort> GetParameterPorts()
        {
            return null;
        }

        public virtual IEnumerable<SyntaxNode> GetSyntax()
        {
            return null;
        }

        public virtual IEnumerable<StatementSyntax> GetStatementSyntax(CSPort sourcePort)
        {
            return null;
        }

        public virtual ExpressionSyntax AccessSyntax(CSPort sourcePort)
        {
            return null;
        }
    }
}
