using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Korduene.Graphing.CS
{
    public class CSGraph : Graph
    {
        public event EventHandler CodeChanged;

        [JsonIgnore]
        [Browsable(false)]
        public SyntaxTree LastSyntaxTree { get; set; }

        [JsonIgnore]
        private string _code;
        private string _namespace;
        private string _modifiers = "public";
        private ObservableCollection<string> _usings;

        [JsonIgnore]
        private readonly GraphToCode _graphToCode;

        [JsonIgnore]
        private readonly CodeToGraph _codeToGraph;

        [Browsable(false)]
        [JsonIgnore]
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    UpdateGraphFromCode();
                }
            }
        }

        public string Namespace
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_namespace))
                {
                    if (Document != null)
                    {
                        return Document.Project.Name;
                    }
                    else
                    {
                        return "Unknown";
                    }
                }

                return _namespace;
            }
            set
            {
                if (_namespace != value)
                {
                    _namespace = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public ObservableCollection<string> Usings
        {
            get { return _usings ??= new ObservableCollection<string>(); }
            set
            {
                if (_usings != value)
                {
                    _usings = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public Microsoft.CodeAnalysis.Document Document { get; set; }

        public CSGraph() : base()
        {
            _graphToCode = new GraphToCode(this);
            _codeToGraph = new CodeToGraph(this);
            this.NodeChanged += CSGraph_NodeChanged;
            this.Usings.CollectionChanged += Usings_CollectionChanged;
        }

        private void Usings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (!this.IsLoading)
            {
                UpdateCodeFromGraph();
            }
        }

        private void CSGraph_NodeChanged(object sender, NodeChangedEventArgs e)
        {
            UpdateCodeFromGraph();
        }

        private void UpdateCodeFromGraph()
        {
            LastSyntaxTree = _graphToCode.ToSyntaxTree();
            _code = LastSyntaxTree.GetRoot().NormalizeWhitespace().ToFullString();
            CodeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateGraphFromCode()
        {
            _codeToGraph.UpdateGraph();
        }
    }
}
