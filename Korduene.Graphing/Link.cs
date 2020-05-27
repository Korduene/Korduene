using Korduene.Graphing.Enums;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    /// <summary>
    /// Link, connects two ports
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Link, connects two ports")]
    [DisplayName("Link")]
    public class Link : INotifyPropertyChanged, ILink
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]
        private Guid _guid;

        private IGraph _parentGraph;
        private object _element;
        private string _id;

        private string _name;
        private GraphPoint _location;
        private GraphPoint _lastLocation;

        private double _width;
        private double _height;
        //private string _comment;
        private bool _isVisible = true;
        private bool _isSelected;
        private IPort _inPort;
        private IPort _outPort;
        private GraphColor _color;
        private GraphColor _secondColor;
        private GraphPoint _startPoint;
        private GraphPoint _point1;
        private GraphPoint _point2;
        private GraphPoint _pont3;
        private int _zIndex;
        private int _overlayZIndex;

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
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid Guid
        {
            get { return _guid; }
            set
            {
                if (_guid != value)
                {
                    _guid = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
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
        //[JsonIgnore]
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
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
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
        /// Gets or sets the start point.
        /// </summary>
        /// <value>
        /// The start point.
        /// </value>
        public GraphPoint StartPoint
        {
            get { return _startPoint; }
            set
            {
                if (_startPoint != value)
                {
                    _startPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the middle point.
        /// </summary>
        /// <value>
        /// The middle point.
        /// </value>
        public GraphPoint Point1
        {
            get { return _point1; }
            set
            {
                if (_point1 != value)
                {
                    _point1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        public GraphPoint Point2
        {
            get { return _point2; }
            set
            {
                if (_point2 != value)
                {
                    _point2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the point3.
        /// </summary>
        /// <value>
        /// The point3.
        /// </value>
        public GraphPoint Point3
        {
            get { return _pont3; }
            set
            {
                if (_pont3 != value)
                {
                    _pont3 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
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

        ///// <summary>
        ///// Gets or sets the comment.
        ///// </summary>
        //public string Comment
        //{
        //    get { return _comment; }
        //    set
        //    {
        //        if (_comment != value)
        //        {
        //            _comment = value;
        //            OnPropertyChanged();
        //            OnPropertyChanged(nameof(IsCommentVisible));
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets a value indicating whether this node's comment visible.
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if this node's comment visible; otherwise, <c>false</c>.
        ///// </value>
        //public bool IsCommentVisible
        //{
        //    get { return !string.IsNullOrWhiteSpace(_comment); }
        //}

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public GraphColor Color
        {
            get
            {
                if (IsSelected)
                {
                    return Constants.COLOR_SELECTED;
                }

                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the second.
        /// </summary>
        /// <value>
        /// The color of the second.
        /// </value>
        public GraphColor SecondColor
        {
            get { return _secondColor; }
            set
            {
                if (_secondColor != value)
                {
                    _secondColor = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the in port.
        /// </summary>
        /// <value>
        /// The in port.
        /// </value>
        public IPort StartPort
        {
            get { return _inPort; }
            set
            {
                if (_inPort != value)
                {
                    _inPort = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the out port.
        /// </summary>
        /// <value>
        /// The out port.
        /// </value>
        public IPort EndPort
        {
            get { return _outPort; }
            set
            {
                if (_outPort != value)
                {
                    _outPort = value;
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

        /// <summary>
        /// Gets or sets Z index of the overlay.
        /// </summary>
        /// <value>
        /// The Z index of the overlay.
        /// </value>
        public int OverlayZIndex
        {
            get { return _overlayZIndex; }
            set
            {
                if (_overlayZIndex != value)
                {
                    _overlayZIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link()
        {
            _guid = Guid.NewGuid();
            _zIndex = Constants.LINK_ZINDEX;
            _overlayZIndex = Constants.LINK_OVERLAY_ZINDEX;
            Color = Constants.COLOR_LINK;
            _location = new GraphPoint();
            _lastLocation = new GraphPoint();
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (name == nameof(StartPort))
            {
                WatchStartPort();
            }
            else if (name == nameof(EndPort))
            {
                WatchEndPort();
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private double GetBend()
        {
            if (StartPort.Location.X < EndPort.Location.X)
            {
                return Math.Abs(StartPort.Location.X - EndPort.Location.X);
            }

            return 0;
        }

        private void WatchStartPort()
        {
            if (StartPort != null)
            {
                StartPort.PropertyChanged -= StartPort_PropertyChanged;
                StartPort.PropertyChanged += StartPort_PropertyChanged;
            }
        }

        private void UnWatchStartPort()
        {
            if (StartPort != null)
            {
                StartPort.PropertyChanged -= StartPort_PropertyChanged;
            }
        }

        private void StartPort_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPort.Location))
            {
                UpdateVisuals();
            }
        }

        private void WatchEndPort()
        {
            if (EndPort != null)
            {
                EndPort.PropertyChanged -= EndPort_PropertyChanged;
                EndPort.PropertyChanged += EndPort_PropertyChanged;
            }
        }

        private void UnWatchEndPort()
        {
            if (EndPort != null)
            {
                EndPort.PropertyChanged -= EndPort_PropertyChanged;
            }
        }

        private void EndPort_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPort.Location))
            {
                UpdateVisuals();
            }
        }

        /// <summary>
        /// Updates the visuals.
        /// </summary>
        public void UpdateVisuals()
        {
            if (ParentGraph == null)
            {
                return;
            }

            if (StartPort != null)
            {
                var startX = StartPort.Location.X;
                var startY = StartPort.Location.Y;
                double endX;
                double endY;
                if (EndPort == null)
                {
                    endX = ParentGraph.MousePosition.X;
                    endY = ParentGraph.MousePosition.Y;
                }
                else
                {
                    endX = EndPort.Location.X;
                    endY = EndPort.Location.Y;
                }

                double bend = 0;

                if (startX > endX)
                {
                    bend = Math.Abs(startX - endX);
                }

                double startBezX;
                double startBezY;
                double endBezX;
                double endBezY;

                if (StartPort.PortType == PortType.Out)
                {
                    startBezX = endX + bend - (int)((endX - startX) / 2);
                    startBezY = startY;
                    endBezX = startX - bend + (int)((endX - startX) / 2);
                    endBezY = endY;
                }
                else
                {
                    startBezX = startX - bend + (int)((startX - endX) / 2);
                    startBezY = startY;
                    endBezX = endX + bend - (int)((startX - endX) / 2);
                    endBezY = endY;
                }

                StartPoint = new GraphPoint(startX, startY);
                Point1 = new GraphPoint(startBezX, startBezY);
                Point2 = new GraphPoint(endBezX, endBezY);
                Point3 = new GraphPoint(endX, endY);
            }
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            UnWatchStartPort();
            UnWatchEndPort();

            if (StartPort != null && EndPort != null)
            {
                StartPort.Disconnect(EndPort);
            }
            else
            {
                ParentGraph.Members.Remove(this);
            }

            StartPort = null;
            EndPort = null;
            ParentGraph = null;
        }
    }
}
