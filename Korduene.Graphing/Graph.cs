using Korduene.Common;
using Korduene.Graphing.Collections;
using Korduene.Graphing.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    /// <summary>
    /// Graph, a container that contains all the graph elements like nodes, ports etc...
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Graph, a container that contains all the graph elements like nodes, ports etc...")]
    [DisplayName("Graph")]
    public partial class Graph : IGraph
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when graph changed, ignores events like mouse move etc...
        /// </summary>
        public event PropertyChangedEventHandler GraphChanged;

        /// <summary>
        /// Occurs when node changed.
        /// </summary>
        public event EventHandler<NodeChangedEventArgs> NodeChanged;

        /// <summary>
        /// Occurs when a node is selected/unselected.
        /// </summary>
        public event EventHandler<INode> NodeSelectionChanged;

        #endregion

        #region [Private Objects]
        private bool _isLoading = true;
        private string _name;
        private string _filePath;
        private UndoEngine _undoEngine;
        private GraphMemberCollection _members;
        private GraphState _state;
        private GraphPoint _mousePosition;
        private GraphPoint _lastMouseDownPosition;
        private GraphPoint _offset = new GraphPoint();
        private GraphPoint _startupOffset = new GraphPoint();
        private GraphPoint _viewCenter = new GraphPoint();
        private double _zoom;
        private ILink _linkingLink;

        private IPort _mouseDownPort;
        private IPort _mouseUpPort;

        #endregion

        #region [Public Properties]

        [Browsable(false)]
        [JsonIgnore]
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [JsonIgnore]
        [Browsable(false)]
        public GraphState State
        {
            get { return _state; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        [Browsable(false)]
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public IUndoEngine UndoEngine
        {
            get { return _undoEngine; }
        }

        /// <summary>
        /// Gets the members.
        /// </summary>
        //[JsonIgnore]
        [Browsable(false)]
        public GraphMemberCollection Members
        {
            get { return _members; }
        }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public IEnumerable<INode> Nodes
        {
            get { return _members.OfType<INode>(); }
            //get { return _members.Where(x => x.MemberType == Enums.MemberType.Node).Cast<INode>(); }
        }

        /// <summary>
        /// Gets the selected nodes.
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public IEnumerable<INode> SelectedNodes
        {
            get { return _members.Where(x => x.MemberType == Enums.MemberType.Node && x.IsSelected).Cast<INode>(); }
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public IEnumerable<IComment> Comments
        {
            get { return _members.Where(x => x.MemberType == Enums.MemberType.Comment).Cast<IComment>(); }
        }

        /// <summary>
        /// Gets or sets the mouse position.
        /// </summary>
        /// <value>
        /// The mouse position.
        /// </value>
        [JsonIgnore]
        [Browsable(false)]
        public GraphPoint MousePosition
        {
            get { return _mousePosition == default ? (_mousePosition = new GraphPoint()) : _mousePosition; }
            set
            {
                if (_mousePosition != value)
                {
                    _mousePosition = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public GraphPoint LastMouseDownPosition
        {
            get { return _lastMouseDownPosition; }
            set
            {
                if (_lastMouseDownPosition != value)
                {
                    _lastMouseDownPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        [Browsable(false)]
        public GraphPoint Offset
        {
            get { return _offset; }
            set
            {
                if (_offset != value)
                {
                    _offset = value;
                    OnPropertyChanged();
                }
            }
        }

        [Browsable(false)]
        public GraphPoint StartupOffset
        {
            get { return _startupOffset; }
            set
            {
                if (_startupOffset != value)
                {
                    _startupOffset = value;
                    OnPropertyChanged();
                }
            }
        }

        [Browsable(false)]
        public GraphPoint ViewCenter
        {
            get { return _viewCenter; }
            set
            {
                if (_viewCenter != value)
                {
                    _viewCenter = value;
                    OnPropertyChanged();
                }
            }
        }

        [Browsable(false)]
        public double Zoom
        {
            get { return _zoom; }
            set
            {
                if (_zoom != value)
                {
                    _zoom = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the mouse down port.
        /// </summary>
        /// <value>
        /// The mouse down port.
        /// </value>
        [JsonIgnore]
        [Browsable(false)]
        public IPort MouseDownPort
        {
            get { return _mouseDownPort; }
            set
            {
                if (_mouseDownPort != value)
                {
                    _mouseDownPort = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the mouse up port.
        /// </summary>
        /// <value>
        /// The mouse up port.
        /// </value>
        [JsonIgnore]
        [Browsable(false)]
        public IPort MouseUpPort
        {
            get { return _mouseUpPort; }
            set
            {
                if (_mouseUpPort != value)
                {
                    _mouseUpPort = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the linking link.
        /// </summary>
        /// <value>
        /// The linking link.
        /// </value>
        [JsonIgnore]
        [Browsable(false)]
        public ILink LinkingLink
        {
            get { return _linkingLink; }
            set
            {
                if (_linkingLink != value)
                {
                    _linkingLink = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph"/> class.
        /// </summary>
        public Graph()
        {
            _undoEngine = new UndoEngine();
            _members = new GraphMemberCollection(this);
            _members.CollectionChanged += _members_CollectionChanged;
        }

        #endregion

        #region [Public Methods]

        public ConnectionResult CreateConnection()
        {
            if (MouseDownPort == null || MouseUpPort == null)
            {
                return new ConnectionResult(false, "Both ports, need to be none null.");
            }

            if (!MouseDownPort.CanConnect(MouseUpPort))
            {
                return new ConnectionResult(false, "Port refused to connect.");
            }

            var result = MouseDownPort.Connect(MouseUpPort);

            if (!result.Success)
            {
                DestroyCurrentLink();
            }

            return result;
        }

        public void DestroyCurrentLink()
        {
            LinkingLink.Destroy();
            LinkingLink = null;
        }

        public IPort GetPort(string id, PortType portType)
        {
            return Members.OfType<INode>().SelectMany(x => x.Ports.Where(y => y.PortType == portType)).FirstOrDefault(x => x.Id == id);
        }

        public void BringToView()
        {
            BringToView(Nodes);
        }

        public void BringToView(IEnumerable<INode> nodes)
        {
            var x = nodes.Min(n => n.Location.X - (n.Width / 4));
            var y = nodes.Min(n => n.Location.Y - (n.Height * 4));

            Offset = new GraphPoint(x, y);
            Zoom = 0.9;
        }

        public void ArrangeNodes()
        {
            //go node by node, port by port, stack connected nodes based on port index

            //GraphControl.CenterGraph();
            Nodes.ToList().ForEach(n => n.Location = ViewCenter);

            var varNodes = Nodes.Where(n => !n.IsConnected);

            var left = varNodes.Any() ? ViewCenter.X - varNodes.Max(x => x.Width * 2) : ViewCenter.X;
            var top = varNodes.Any() ? ViewCenter.Y - varNodes.Sum(x => x.Height) : ViewCenter.Y;

            foreach (var variableNode in varNodes)
            {
                top += variableNode.Height + 30;
                variableNode.Location = new GraphPoint(left, top);
            }

            left = varNodes.Any() ? varNodes.Max(n => n.GetRight()) + 300 : ViewCenter.X + 300;

            var nodes = Nodes.Where(n => !varNodes.Contains(n));
            var totalNodesHeight = nodes.Sum(n => n.Height);
            var totalNodes = nodes.Count();
            totalNodesHeight += totalNodes * 30;

            foreach (var node in nodes)
            {
                var nodeYIndex = nodes.ToList().IndexOf(node) + 1;
                var nodeY = (node.Location.Y - totalNodesHeight / 2) + (double)nodeYIndex * (totalNodesHeight / totalNodes);
                node.Location = new GraphPoint(left + node.Width, nodeY);

                var totalParentsHeight = node.ParentNodes.Sum(x => x.Height);
                var totalParentNodes = node.ParentNodes.Count();
                totalParentsHeight += totalParentNodes * 30;

                foreach (var parent in node.ParentNodes)
                {
                    var yIndex = node.ParentNodes.ToList().IndexOf(parent) + 1;

                    var y = (node.Location.Y - totalParentsHeight / 2) + (double)yIndex * (totalParentsHeight / totalParentNodes);

                    parent.Location = new GraphPoint(node.Location.X - node.Width - parent.Width, y);
                }
            }

            BringToView(Nodes);
        }

        public string SerializeToJson()
        {
            return GraphSerializer.Seraialize(this);
        }

        #endregion

        #region [Private Methods]

        private void _members_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset || e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
                {
                    e.OldItems.OfType<INode>().ToList().ForEach(x =>
                    {
                        x.NodeChanged -= OnNodeChanged;
                        x.SelectionChanged -= OnNodeSelectionChanged;
                        OnNodeChanged(x, new NodeChangedEventArgs(NodeChangeEventType.NodeRemoved));
                    });
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    e.NewItems.OfType<INode>().ToList().ForEach(x =>
                    {
                        x.NodeChanged -= OnNodeChanged;
                        x.NodeChanged += OnNodeChanged;

                        x.SelectionChanged -= OnNodeSelectionChanged;
                        x.SelectionChanged += OnNodeSelectionChanged;

                        OnNodeChanged(x, new NodeChangedEventArgs(NodeChangeEventType.NodeAdded));
                    });
                }
            }
        }

        #endregion

        private void OnNodeChanged(object sender, NodeChangedEventArgs nodeChangedEventArgs)
        {
            if (IsLoading)
            {
                return;
            }

            NodeChanged?.Invoke(sender, nodeChangedEventArgs);
            OnGraphChanged(nameof(Members));
        }

        private void OnNodeSelectionChanged(object sender, EventArgs args)
        {
            NodeSelectionChanged?.Invoke(sender, sender as INode);
        }

        private void OnGraphChanged(string propertyName)
        {
            var _ignoredProperties = new[] { nameof(MousePosition), nameof(FilePath), nameof(LastMouseDownPosition), nameof(MouseDownPort), nameof(LinkingLink), nameof(MouseUpPort) };

            if (_ignoredProperties.Contains(propertyName))
            {
                return;
            }

            GraphChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            OnGraphChanged(name);
        }
    }
}
