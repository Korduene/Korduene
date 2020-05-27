using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    /// <summary>
    /// Node TreeView Item
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Node TreeView Item")]
    [DisplayName("NodeTreeViewItem")]
    [DebuggerDisplay("{Name}, {Items.Count} item(s)")]
    public class GraphMenuItem : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private string _name;
        private string _displayName;
        private bool _isVisible = true;
        private Type _nodeType;
        private GraphMenuItem _parent;
        private GraphMenuItemCollection _items;
        private object _typeInfo;

        #endregion

        #region [Public Properties]

        public GraphMenuItem Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != value)
                {
                    _parent = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
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
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;

                    if (value)
                    {
                        MakeParentVisible();
                    }

                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the node.
        /// </summary>
        /// <value>
        /// The type of the node.
        /// </value>
        public Type NodeType
        {
            get { return _nodeType; }
            set
            {
                if (_nodeType != value)
                {
                    _nodeType = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type information.
        /// </summary>
        /// <value>
        /// The type information.
        /// </value>
        public object TypeInfo
        {
            get { return _typeInfo; }
            set
            {
                if (_typeInfo != value)
                {
                    _typeInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public GraphMenuItemCollection Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchableContent { get; set; }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphMenuItem"/> class.
        /// </summary>
        public GraphMenuItem()
        {
            _items = new GraphMenuItemCollection(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphMenuItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public GraphMenuItem(string name) : this()
        {
            this.Name = name;
            this.DisplayName = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphMenuItem" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        public GraphMenuItem(string name, string displayName) : this()
        {
            this.Name = name;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphMenuItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="nodeType">Type of the node.</param>
        public GraphMenuItem(string name, Type nodeType) : this(name)
        {
            this.NodeType = nodeType;
        }

        #endregion

        #region [Public Methods]

        public void MakeParentVisible()
        {
            this.IsVisible = true;

            var currentParent = this.Parent;
            while (currentParent != null)
            {
                currentParent.IsVisible = true;
                currentParent = currentParent.Parent;
            }
        }

        public void MakeItemsVisible()
        {
            this.IsVisible = true;

            foreach (var item in this.Items)
            {
                MakeItemsVisible(item);
            }
        }

        #endregion

        #region [Private Methods]

        private void MakeItemsVisible(GraphMenuItem item)
        {
            item.IsVisible = true;

            foreach (var sub in item.Items)
            {
                sub.IsVisible = true;
            }
        }

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
