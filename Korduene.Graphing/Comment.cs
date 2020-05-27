using Korduene.Graphing.Enums;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    /// <summary>
    /// Comment
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Comment")]
    [DisplayName("Comment")]
    [DebuggerDisplay("Comment: {Text}")]
    public class Comment : IComment
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private string _id;
        private IGraph _parentGraph;
        private object _element;
        private string _name;
        private GraphPoint _location;
        private GraphPoint _lastLocation;
        private double _width;
        private double _height;
        private bool _isSelected;
        private int _zIndex;
        private string _text;

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
            get { return MemberType.Comment; }
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
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
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

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
            LastLocation = new GraphPoint();
        }

        #endregion

        #region [Public Methods]

        public void UpdateVisuals()
        {
        }

        #endregion

        #region [Private Methods]

        #endregion

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
