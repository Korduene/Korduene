using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene.UI
{
    /// <summary>
    /// ViewModel Base
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("ViewModel Base")]
    [DisplayName("ViewModelBase")]
    [DebuggerDisplay("{this}")]
    public class DockableBase : IDockable
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private string _name;
        private bool _isActive;
        private bool _isSelected;
        private string _contentId;

        #endregion

        #region [Public Properties]

        public virtual string Name
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

        public virtual bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public string ContentId
        {
            get { return _contentId; }
            set
            {
                if (_contentId != value)
                {
                    _contentId = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="DockableBase"/> class.
        /// </summary>
        public DockableBase()
        {
        }

        #endregion

        #region [Command Handlers]

        #endregion

        #region [Public Methods]

        public virtual void OnLoaded() { }

        public virtual void OnShown() { }

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
