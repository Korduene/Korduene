using Korduene.Templates;
using Korduene.UI.Dialogs.Data;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Korduene.UI.WPF.Dialogs.ViewModels
{
    /// <summary>
    /// New File ViewModel
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("New File ViewModel")]
    [DisplayName("NewFileViewModel")]
    [DebuggerDisplay("{this}")]
    public class NewFileViewModel : ViewModelBase
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private string _directory;
        private FileTemplate _selectedTemplate;

        #endregion

        #region [Public Properties]

        public Current Current { get { return Current.Instance; } }

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

        public FileTemplate SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                if (_selectedTemplate != value)
                {
                    _selectedTemplate = value;
                    OnPropertyChanged();
                    SuggestAName();
                }
            }
        }

        public override object Data
        {
            get
            {
                return new NewFileData() { Template = SelectedTemplate, Directory = Directory };
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="NewFileViewModel" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public NewFileViewModel(string directory)
        {
            Directory = directory;
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        private void SuggestAName()
        {
            if (SelectedTemplate == null)
            {
                return;
            }

            var files = new DirectoryInfo(Directory).GetFiles().Select(x => x.Name);
            var count = 0;
            var name = SelectedTemplate.DefaultName;

            while (true)
            {
                count++;

                name = $"{SelectedTemplate.DefaultName}{count}{Path.GetExtension(SelectedTemplate.MainFile)}";

                if (!files.Contains(name))
                {
                    break;
                }
            }

            SelectedTemplate.ChosenName = Path.GetFileNameWithoutExtension(name);
        }

        #endregion
    }
}
