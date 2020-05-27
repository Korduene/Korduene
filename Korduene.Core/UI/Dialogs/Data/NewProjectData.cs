using Korduene.Templates;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene.UI.Dialogs.Data
{
    /// <summary>
    /// NewProjectData
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("NewProjectData")]
    [DisplayName("NewProjectData")]
    [DebuggerDisplay("NewProjectData")]
    public class NewProjectData : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private ProjectTemplate _template;
        private string _directory;

        #endregion

        #region [Public Properties]

        public ProjectTemplate Template
        {
            get { return _template; }
            set
            {
                if (_template != value)
                {
                    _template = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Directory
        {
            get { return _directory; }
            set
            {
                if (_directory != value)
                {
                    _directory = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="NewProjectData"/> class.
        /// </summary>
        public NewProjectData()
        {
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
