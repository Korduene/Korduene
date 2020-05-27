using Korduene.Graphing.Collections;
using Korduene.Graphing.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    /// <summary>
    /// Node, representing a code block
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Node, representing a code block")]
    [DisplayName("Node")]
    [DebuggerDisplay("{Name}, {Ports.Count} ports")]
    public class Node : INode
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when port changed.
        /// </summary>
        public event EventHandler<PortChangedEventArgs> PortChanged;

        /// <summary>
        /// Occurs when node changed.
        /// </summary>
        public event EventHandler<NodeChangedEventArgs> NodeChanged;

        /// <summary>
        /// Occurs when node is selected/unselected.
        /// </summary>
        public event EventHandler SelectionChanged;

        #endregion

        #region [Private Objects]
        private IGraph _parentGraph;
        private PortCollection _ports;
        private object _element;
        private string _id;
        private string _name;
        private GraphPoint _location;
        private GraphPoint _lastLocation;
        private double _width;
        private double _height;
        private string _comment;
        private GraphColor _textColor = Constants.COLOR_TEXT;
        private GraphColor _headerColor = Constants.COLOR_NODE_HEADER_FILL;
        private GraphColor _backgroundColor = Constants.COLOR_NODE_FILL;
        private GraphColor _borderColor = Constants.COLOR_BORDER;
        private double _opacity = Constants.NODE_OPACITY;
        private bool _isVisible = true;
        private bool _isSelected;
        private int _zIndex;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public object VisualContainer
        {
            get { return _element; }
            set
            {
                if (_element != value)
                {
                    _element = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the parent graph.
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public IGraph ParentGraph
        {
            get { return _parentGraph; }
            set
            {
                if (_parentGraph != value)
                {
                    _parentGraph = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the type of the member.
        /// </summary>
        public MemberType MemberType
        {
            get { return MemberType.Node; }
        }

        /// <summary>
        /// Gets or sets the Ports.
        /// </summary>
        public PortCollection Ports
        {
            get { return _ports; }
        }

        /// <summary>
        /// Gets the connected nodes.
        /// </summary>
        /// <value>
        /// The connected nodes.
        /// </value>
        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<INode> ConnectedNodes
        {
            get
            {
                return Ports.SelectMany(x => x.ConnectedPorts).Select(x => x.ParentNode).Distinct();
            }
        }

        /// <summary>
        /// Gets the parent nodes.
        /// </summary>
        /// <value>
        /// The parent nodes.
        /// </value>
        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<INode> ParentNodes
        {
            get
            {
                return Ports.Where(x => x.PortType == PortType.In).SelectMany(x => x.ConnectedPorts).Select(x => x.ParentNode).Distinct();
            }
        }

        public bool IsConnected
        {
            get
            {
                return ConnectedNodes.Any();
            }
        }

        /// <summary>
        /// Gets the child nodes.
        /// </summary>
        /// <value>
        /// The child nodes.
        /// </value>
        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<INode> ChildNodes
        {
            get
            {
                return Ports.Where(x => x.PortType == PortType.Out).SelectMany(x => x.ConnectedPorts).Select(x => x.ParentNode).Distinct();
            }
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
        /// Gets the default name.
        /// </summary>
        /// <value>
        /// The default name.
        /// </value>
        [JsonIgnore]
        public virtual string DefaultName { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this node is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }

                if (value)
                {
                    BorderColor = Constants.COLOR_SELECTED;
                }
                else
                {
                    BorderColor = Constants.COLOR_BORDER;
                }
            }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [Browsable(false)]
        public GraphPoint Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the last location (internal use only).
        /// </summary>
        /// <value>
        /// The last location.
        /// </value>
        [Browsable(false)]
        public GraphPoint LastLocation
        {
            get { return _lastLocation; }
            set
            {
                if (_lastLocation != value)
                {
                    _lastLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [Browsable(false)]
        public double Width
        {
            get { return _width < Constants.NODE_MIN_WIDTH ? Constants.NODE_MIN_WIDTH : _width; }
            set
            {
                if (_width != value)
                {
                    if (value < Constants.NODE_MIN_WIDTH)
                    {
                        _width = Constants.NODE_MIN_WIDTH;
                    }
                    else
                    {
                        _width = value;
                    }
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [Browsable(false)]
        public double Height
        {
            get { return _height < Constants.NODE_MIN_HEIGHT ? Constants.NODE_MIN_HEIGHT : _height; }
            set
            {
                if (_height != value)
                {
                    if (value < Constants.NODE_MIN_HEIGHT)
                    {
                        _height = Constants.NODE_MIN_HEIGHT;
                    }
                    else
                    {
                        _height = value;
                    }
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsCommentVisible));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this node's comment visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this node's comment visible; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsCommentVisible
        {
            get { return !string.IsNullOrWhiteSpace(_comment); }
        }

        #region [Visual]

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>
        /// The color of the text.
        /// </value>
        [Browsable(false)]
        public GraphColor TextColor
        {
            get { return _textColor; }
            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the header.
        /// </summary>
        /// <value>
        /// The color of the header.
        /// </value>
        [Browsable(false)]
        public virtual GraphColor HeaderColor
        {
            get { return _headerColor; }
            set
            {
                if (_headerColor != value)
                {
                    _headerColor = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>
        /// The color of the border.
        /// </value>
        [Browsable(false)]
        public GraphColor BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        [Browsable(false)]
        public GraphColor BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        [Browsable(false)]
        public double Opacity
        {
            get { return _opacity; }
            set
            {
                if (_opacity != value)
                {
                    _opacity = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this port is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port is visible; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets Z index of the member.
        /// </summary>
        /// <value>
        /// The Z index of the member.
        /// </value>
        [Browsable(false)]
        public int ZIndex
        {
            get { return _zIndex; }
            set
            {
                if (_zIndex != value)
                {
                    _zIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node()
        {
            _ports = new PortCollection(this);
            _zIndex = Constants.NODE_ZINDEX;
            _location = new GraphPoint();
            _lastLocation = new GraphPoint();
            _ports.CollectionChanged += _ports_CollectionChanged;
            _ports.PortChanged += _ports_PortChanged;
            _ports.PortConnected += _ports_PortConnected;
            _ports.PortDisconnected += _ports_PortDisconnected;
        }

        #endregion

        #region [Public Methods]

        public void UpdateVisuals()
        {
        }

        public bool Overlaps(INode node)
        {
            return !(this.Location.X > node.Location.X + node.Width) &&
                   !(this.Location.X + this.Width < node.Location.X) &&
                   !(this.Location.Y > node.Location.Y + node.Height) &&
                   !(this.Location.Y + this.Height < node.Location.Y);
        }

        public bool IsConnectedTo(INode node)
        {
            foreach (var port in Ports)
            {
                var result = node.Ports.ToList().Any(x => x.IsConnectedTo(port));

                if (result)
                {
                    return result;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the top (Y).
        /// </summary>
        /// <returns></returns>
        public double GetTop()
        {
            return Location.Y;
        }

        /// <summary>
        /// Gets the bottom (Y + Height).
        /// </summary>
        /// <returns></returns>
        public double GetBottom()
        {
            return Location.Y + Height;
        }

        /// <summary>
        /// Gets the left (X).
        /// </summary>
        /// <returns></returns>
        public double GetLeft()
        {
            return Location.X;
        }

        /// <summary>
        /// Gets the right (X + Width).
        /// </summary>
        /// <returns></returns>
        public double GetRight()
        {
            return Location.X + Width;
        }

        /// <summary>
        /// Destroys this node.
        /// </summary>
        public void Destroy()
        {
            foreach (var port in Ports)
            {
                port.Disconnect();
            }

            ParentGraph?.Members.Remove(this);
        }

        #endregion

        #region [Private Methods]

        #endregion

        #region [Event Handlers]

        private void _ports_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.PortAdded));
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.PortRemoved));
            }
        }

        private void _ports_PortChanged(object sender, PortChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPort.Text))
            {
                NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.PortTextChanged));
            }
            if (e.PropertyName == nameof(IPort.Comment))
            {
                NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.PortCommentChanged));
            }
            else if (e.PropertyName == nameof(IPort.Value))
            {
                NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.PortValueChanged));
            }
            else if (e.PropertyName == nameof(IPort.ConnectedPorts))
            {
                NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.PortValueChanged));
            }
        }

        private void _ports_PortDisconnected(object sender, IPort e)
        {
            NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.PortConnected, e));
        }

        private void _ports_PortConnected(object sender, IPort e)
        {
            NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.PortDisconnected, e));
        }

        #endregion

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            if (name == nameof(IsSelected))
            {
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
            else if (name == nameof(Name))
            {
                NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.NodeNameChanged));
            }
            else if (name == nameof(Comment))
            {
                NodeChanged?.Invoke(this, new NodeChangedEventArgs(NodeChangeEventType.NodeCommentChanged));
            }
        }
    }
}