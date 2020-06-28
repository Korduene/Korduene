using System.ComponentModel;
using System.Diagnostics;

namespace Korduene.UI
{
    /// <summary>
    /// Korduene Document
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Korduene Document")]
    [DisplayName("KordueneDocument")]
    [DebuggerDisplay("{Name}")]
    public class KDocumentBase : DockableBase
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private string _filePath;
        private bool _isSaved;
        private string _documentTypeId;
        private string _name;

        #endregion

        #region [Public Properties]

        public override string Name
        {
            get { return IsSaved ? _name : $"{_name}*"; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                if (_isSaved != value)
                {
                    _isSaved = value;
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged();
                }
            }
        }

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

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="KDocumentBase"/> class.
        /// </summary>
        public KDocumentBase()
        {
        }

        #endregion

        #region [Public Methods]

        public virtual void Save()
        {
            IsSaved = true;
        }

        public virtual void Reload()
        {
        }

        #endregion

        public virtual void OnActivated()
        {
        }

        #region [Private Methods]

        #endregion
    }
}
