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
    /// New Project ViewModel
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("New Project ViewModel")]
    [DisplayName("NewProjectViewModel")]
    [DebuggerDisplay("{this}")]
    public class NewProjectViewModel : ViewModelBase
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private string _directory;
        private ProjectTemplate _selectedTemplate;

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

        public ProjectTemplate SelectedTemplate
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
                return new NewProjectData() { Template = SelectedTemplate, Directory = Directory };
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="NewProjectViewModel"/> class.
        /// </summary>
        public NewProjectViewModel(string directory)
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

            if (System.IO.Directory.Exists(Directory))
            {
                var dirs = new DirectoryInfo(Directory).GetDirectories().Select(x => x.Name);
                var count = 0;
                var name = SelectedTemplate.DefaultName;

                while (true)
                {
                    count++;
                    name = SelectedTemplate.DefaultName + count.ToString();

                    if (!dirs.Contains(name))
                    {
                        break;
                    }
                }

                SelectedTemplate.ChosenName = name;
            }
            else
            {
                SelectedTemplate.ChosenName = SelectedTemplate.DefaultName;
            }
        }

        #endregion
    }
}
