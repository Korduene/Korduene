using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene.UI
{
    /// <summary>
    /// ToolboxItemCategory
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("ToolboxItemCategory")]
    [DisplayName("ToolboxItemCategory")]
    [DebuggerDisplay("ToolboxItemCategory")]
    public class ToolboxItemCategory : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private string _name;
        private bool _isVisible;
        private string _documentTypeId;
        private bool _isExpanded;
        private ObservableCollection<IKToolboxItem> _items;

        #endregion

        #region [Public Properties]

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
        /// Gets or sets the document type identifier.
        /// </summary>
        /// <value>
        /// The document type identifier.
        /// </value>
        public string DocumentTypeId
        {
            get { return _documentTypeId; }
            set
            {
                if (_documentTypeId != value)
                {
                    _documentTypeId = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<IKToolboxItem> Items
        {
            get { return _items ??= new ObservableCollection<IKToolboxItem>(); }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolboxItemCategory"/> class.
        /// </summary>
        public ToolboxItemCategory()
        {
        }

        public ToolboxItemCategory(string name, string documentTypeId) : this()
        {
            this.Name = name;
            this.DocumentTypeId = documentTypeId;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
