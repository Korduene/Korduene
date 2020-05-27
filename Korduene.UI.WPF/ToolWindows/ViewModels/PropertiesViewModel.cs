using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Korduene.UI.WPF.ToolWindows.ViewModels
{
    /// <summary>
    /// Properties ViewModel
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Properties ViewModel")]
    [DisplayName("PropertiesViewModel")]
    [DebuggerDisplay("{Name}")]
    public class PropertiesViewModel : KToolWindow
    {
        #region [Events]

        #endregion

        #region [Private Objects]
        private object _selectedObject;
        private object[] _selectedObjects;
        private KordueneWorkspace _workspace;
        private ICommand _itemDoubleClickCommand;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets the selected object.
        /// </summary>
        /// <value>
        /// The selected object.
        /// </value>
        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (_selectedObject != value)
                {
                    _selectedObject = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected objects.
        /// </summary>
        /// <value>
        /// The selected objects.
        /// </value>
        public object[] SelectedObjects
        {
            get { return _selectedObjects; }
            set
            {
                if (_selectedObjects != value)
                {
                    _selectedObjects = value;
                    OnPropertyChanged();
                }
            }
        }

        public KordueneWorkspace Workspace
        {
            get { return _workspace; }
            set
            {
                if (_workspace != value)
                {
                    _workspace = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Commands]

        public ICommand ItemDoubleClickCommand
        {
            get { return _itemDoubleClickCommand ??= new KCommand<object>(ExecuteItemDoubleClickCommand); }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesViewModel"/> class.
        /// </summary>
        public PropertiesViewModel()
        {
            this.ContentId = Constants.ContentIds.PROPERTIES;
            this.Name = "Properties";
            Current.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        #endregion

        #region [Command Handlers]

        public void ExecuteItemDoubleClickCommand(object parameter)
        {
            if (parameter is Document document)
            {
                Current.Instance.OpenDocument(document);
            }
        }

        #endregion

        #region [Public Methods]

        public override void OnShown()
        {
            SelectedObjects = Current.Instance.SelectedItems;
            base.OnShown();
        }

        #endregion

        #region [Private Methods]

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Current.SelectedItems))
            {
                SelectedObjects = Current.Instance.SelectedItems;
            }
        }

        #endregion
    }
}
