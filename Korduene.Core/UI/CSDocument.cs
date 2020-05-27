using Korduene.Graphing;
using Korduene.Graphing.CS;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Korduene.UI
{
    /// <summary>
    /// Korduene Document
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("CS Document")]
    [DisplayName("CSDocument")]
    [DebuggerDisplay("{Name}")]
    public class CSDocument : KDocument
    {
        #region [Events]

        #endregion

        #region [Private Objects]
        private bool _codeUpdating;
        private ICSharpCode.AvalonEdit.Document.TextDocument _avalonDocument;
        private Microsoft.CodeAnalysis.Document _document;
        private bool _isGraphActive;
        private bool _isCodeActive;
        private CSGraph _graph;

        #endregion

        #region [Public Properties]

        public string GraphFilePath
        {
            get { return $"{FilePath}.{Constants.GRAPH_EXTENSION}"; }
        }

        public ICSharpCode.AvalonEdit.Document.TextDocument AvalonDocument
        {
            get { return _avalonDocument; }
            set
            {
                if (_avalonDocument != value)
                {
                    _avalonDocument = value;
                    OnPropertyChanged();
                }
            }
        }

        public Microsoft.CodeAnalysis.Document Document
        {
            get { return _document; }
            set
            {
                if (_document != value)
                {
                    _document = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsGraphActive
        {
            get { return _isGraphActive; }
            set
            {
                if (_isGraphActive != value)
                {
                    _isGraphActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCodeActive
        {
            get { return _isCodeActive; }
            set
            {
                if (_isCodeActive != value)
                {
                    _isCodeActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public CSGraph Graph
        {
            get
            {
                return _graph;
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="KDocumentBase"/> class.
        /// </summary>
        public CSDocument(Microsoft.CodeAnalysis.Document document)
        {
            this.IsGraphActive = true;
            this.Document = document;
            this.Name = document.Name;
            this.FilePath = document.FilePath;

            this.AvalonDocument = new ICSharpCode.AvalonEdit.Document.TextDocument() { FileName = this.Name, Text = File.ReadAllText(document.FilePath) };
            this.AvalonDocument.TextChanged += AvalonDocument_TextChanged;

            if (File.Exists(this.GraphFilePath))
            {
                _graph = GraphSerializer.Deserialize<CSGraph>(File.ReadAllText(this.GraphFilePath));
            }
            else
            {
                _graph = new CSGraph();
            }

            this.Graph.Name = Path.GetFileNameWithoutExtension(document.Name);
            this.Graph.FilePath = this.GraphFilePath;
            this.Graph.Document = document;

            _graph.GraphChanged += _graph_GraphChanged;
            _graph.NodeChanged += _graph_NodeChanged;
            _graph.NodeSelectionChanged += _graph_NodeSelectionChanged;
            _graph.CodeChanged += _graph_CodeChanged;

            OnNodeSelectionChanged();
        }

        private void AvalonDocument_TextChanged(object sender, EventArgs e)
        {
            this.IsSaved = false;

            if (!_codeUpdating)
            {
                Graph.Code = AvalonDocument.Text;
            }
        }

        #endregion

        #region [Public Methods]

        public override void Save()
        {
            File.WriteAllText(this.FilePath, this.AvalonDocument.Text);
            File.WriteAllText(this.GraphFilePath, this.Graph.SerializeToJson());

            base.Save();
        }

        public override void OnActivated()
        {
            OnNodeSelectionChanged();
            base.OnActivated();
        }

        public virtual void OnNodeSelectionChanged()
        {
            if (Graph == null)
            {
                return;
            }

            var nodes = _graph.Nodes.Where(x => x.IsSelected).ToArray();

            if (nodes.Length > 0)
            {
                Current.Instance.SelectedItems = nodes;
            }
            else
            {
                Current.Instance.SelectedItems = new[] { _graph };
            }
        }

        #endregion

        #region [Private Methods]

        private void _graph_GraphChanged(object sender, PropertyChangedEventArgs e)
        {
            this.IsSaved = false;
        }

        private void _graph_NodeChanged(object sender, NodeChangedEventArgs e)
        {
            this.IsSaved = false;
        }

        private void _graph_NodeSelectionChanged(object sender, INode e)
        {
            OnNodeSelectionChanged();
        }

        private void _graph_CodeChanged(object sender, EventArgs e)
        {
            _codeUpdating = true;
            AvalonDocument.Text = Graph.Code;
            _codeUpdating = false;
        }

        #endregion
    }
}