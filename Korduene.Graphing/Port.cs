using Korduene.Graphing.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    /// <summary>
    /// Port, connecting nodes to each other
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Port, connecting nodes to each other")]
    [DisplayName("Port")]
    [DebuggerDisplay("Port, {PortType}, {Text}")]
    public class Port : IPort
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private string _id;
        private INode _parentNode;
        private PortType _portType;
        private bool _isPassThrough;
        private bool _isCollection;
        private string _text;
        private string _comment;
        private object _dataType;
        private object _value;
        private GraphPoint _location;
        private GraphColor _textColor = Constants.COLOR_TEXT;
        private GraphColor _fillColor = Constants.COLOR_NODE_FILL;
        private GraphColor _borderColor = Constants.COLOR_BORDER;
        private bool _isVisible = true;
        private ObservableCollection<IPort> _connectedPorts;
        private AcceptedConnections _acceptedConnections;
        private Dictionary<string, object> _properties;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Browsable(false)]
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
        /// Gets or sets the parent node.
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public INode ParentNode
        {
            get { return _parentNode; }
            set
            {
                if (_parentNode != value)
                {
                    _parentNode = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the type of the port.
        /// </summary>
        [Browsable(false)]
        public PortType PortType
        {
            get { return _portType; }
            set
            {
                if (_portType != value)
                {
                    _portType = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pass through.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pass through; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsPassThrough
        {
            get { return _isPassThrough; }
            set
            {
                if (_isPassThrough != value)
                {
                    _isPassThrough = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this port is a collection of specified type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port is a collection of specified type; otherwise, <c>false</c>.
        /// </value>
        public bool IsCollection
        {
            get { return _isCollection; }
            set
            {
                if (_isCollection != value)
                {
                    _isCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this port is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port is connected; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsConnected { get { return _connectedPorts.Count > 0; } }

        /// <summary>
        /// The amount of accepted connections for this port.
        /// </summary>
        /// <value>
        /// The amount of accepted connections for this port.
        /// </value>
        [Browsable(false)]
        public AcceptedConnections AcceptedConnections
        {
            get { return _acceptedConnections; }
            set
            {
                if (_acceptedConnections != value)
                {
                    _acceptedConnections = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the index of this port in the parent node port collection.
        /// </summary>
        [Browsable(false)]
        public int Index
        {
            get
            {
                if (_parentNode?.Ports != null)
                {
                    return _parentNode.Ports.IndexOf(this);
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        [Browsable(false)]
        public int RowIndex
        {
            get
            {
                if (ParentNode == null || Index == 0)
                {
                    return 0;
                }

                var index = Index;

                return ParentNode.Ports.Count(x => x.PortType == _portType && x.Index < index);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this port has an editor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port has an editor; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool HasEditor
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        [Browsable(false)]
        public object DataType
        {
            get { return _dataType; }
            set
            {
                if (_dataType != value)
                {
                    _dataType = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Browsable(false)]
        public object Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        [Browsable(false)]
        public Dictionary<string, object> Properties
        {
            get { return _properties ??= new Dictionary<string, object>(); }
            set
            {
                if (_properties != value)
                {
                    _properties = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the connected ports.
        /// </summary>
        /// <value>
        /// The connected ports.
        /// </value>
        [Browsable(false)]
        //[JsonIgnore]
        public ObservableCollection<IPort> ConnectedPorts
        {
            get { return _connectedPorts; }
            set
            {
                if (_connectedPorts != value)
                {
                    _connectedPorts = value;
                    OnPropertyChanged();
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
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>
        /// The color of the border.
        /// </value>
        [Browsable(false)]
        public virtual GraphColor BorderColor
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
        /// Gets or sets the port fill color.
        /// </summary>
        /// <value>
        /// The port fill color.
        /// </value>
        [Browsable(false)]
        public GraphColor FillColor
        {
            get { return _fillColor; }
            set
            {
                if (_fillColor != value)
                {
                    _fillColor = value;
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

        #endregion

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="Port"/> class.
        /// </summary>
        public Port()
        {
            _connectedPorts = new ObservableCollection<IPort>();
            _connectedPorts.CollectionChanged += _connectedPorts_CollectionChanged;
            _location = new GraphPoint();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Port" /> class.
        /// </summary>
        /// <param name="portType">Type of the port.</param>
        /// <param name="acceptedConnections">The accepted connections.</param>
        public Port(PortType portType, AcceptedConnections acceptedConnections) : this()
        {
            this.PortType = portType;
            this.AcceptedConnections = acceptedConnections;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Port" /> class.
        /// </summary>
        /// <param name="portType">Type of the port.</param>
        /// <param name="acceptedConnections">The accepted connections.</param>
        /// <param name="text">The text.</param>
        public Port(PortType portType, AcceptedConnections acceptedConnections, string text) : this(portType, acceptedConnections)
        {
            this.Text = text;
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// Gets the port center.
        /// </summary>
        /// <returns></returns>
        public GraphPoint GetPortCenter()
        {
            return new GraphPoint(Location.X - (Constants.PORT_WIDTH_HEIGHT / 2), Location.Y - (Constants.PORT_WIDTH_HEIGHT / 2));
        }

        #endregion

        #region [Private Methods]

        private void _connectedPorts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
            }
        }

        #endregion

        /// <summary>
        /// With the property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Port WithProperty(string name, object value)
        {
            Properties[name] = value;
            return this;
        }

        /// <summary>
        /// Determines whether port has property with the specified name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        ///   <c>true</c> if port has property with the specified name; otherwise, <c>false</c>.
        /// </returns>
        public bool HasProperty(string propertyName)
        {
            return Properties.ContainsKey(propertyName);
        }

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Determines whether this port can connect the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns>
        ///   <c>true</c> if this port can connect the specified port; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanConnect(IPort port)
        {
            return port != null && port != this;
        }

        /// <summary>
        /// Determines whether this port is connected to the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns>
        ///   <c>true</c> if this port is connected to the specified port; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsConnectedTo(IPort port)
        {
            return _connectedPorts.Contains(port);
        }

        /// <summary>
        /// Connects the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public virtual ConnectionResult Connect(IPort port)
        {
            if (!CanConnect(port)) { return new ConnectionResult(false, "Cannot connect."); }

            if (IsConnectedTo(port)) { return new ConnectionResult(false, "Already connected."); }

            if (AcceptedConnections == AcceptedConnections.None) { return new ConnectionResult(false, "Connections are not accepted."); }

            if ((this.AcceptedConnections == AcceptedConnections.One && ConnectedPorts.Count > 0) || (port.AcceptedConnections == AcceptedConnections.One && port.ConnectedPorts.Count > 0))
            {
                return new ConnectionResult(false, "Can only accept one connection.");
            }

            if (this.PortType == port.PortType) { return new ConnectionResult(false, "Ports are of the same type."); }

            this.ConnectedPorts.Add(port);
            port.ConnectedPorts.Add(this);

            if (ParentNode.ParentGraph.LinkingLink != null)
            {
                ParentNode.ParentGraph.LinkingLink.EndPort = port;
                ParentNode.ParentGraph.LinkingLink.UpdateVisuals();
            }
            else
            {
                var link = new Link
                {
                    StartPort = this,
                    EndPort = port
                };

                ParentNode.ParentGraph.Members.Add(link);
                link.UpdateVisuals();
            }

            ParentNode.ParentGraph.LinkingLink = null;
            ParentNode.ParentGraph.MouseDownPort = null;
            ParentNode.ParentGraph.MouseUpPort = null;

            return new ConnectionResult(true);
        }

        /// <summary>
        /// Disconnects the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        public virtual void Disconnect(IPort port)
        {
            var link = ParentNode.ParentGraph.Members.OfType<ILink>().FirstOrDefault(x => (x.StartPort == this && x.EndPort == port) || (x.StartPort == port && x.EndPort == this));

            this.ConnectedPorts.Remove(port);
            port.ConnectedPorts.Remove(this);

            ParentNode.ParentGraph.Members.Remove(link);
        }

        /// <summary>
        /// Disconnects all.
        /// </summary>
        public virtual void Disconnect()
        {
            while (ConnectedPorts.Count > 0)
            {
                Disconnect(ConnectedPorts[0]);
            }
        }
    }
}
