using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Korduene.UI
{
    /// <summary>
    /// Korduene Document
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("XamlDocument")]
    [DisplayName("XamlDocument")]
    [DebuggerDisplay("XamlDocument")]
    public class XamlDocument : KDocument
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private ICSharpCode.AvalonEdit.Document.TextDocument _avalonDocument;
        private Microsoft.CodeAnalysis.Document _document;
        private bool _isGraphActive;
        private bool _isCodeActive;
        private bool _isDesignerActive;
        private object _designContext;
        private bool _designerLoadFailed;

        #endregion

        #region [Public Properties]

        public ICSharpCode.AvalonEdit.Document.TextDocument AvalonDocument
        {
            get { return _avalonDocument; }
            set
            {
                if (_avalonDocument != value)
                {
                    _avalonDocument = value;
                    OnPropertyChanged();
                }
            }
        }

        public Microsoft.CodeAnalysis.Document Document
        {
            get { return _document; }
            set
            {
                if (_document != value)
                {
                    _document = value;
                    OnPropertyChanged();
                }
            }
        }

        public object DesignContext
        {
            get { return _designContext; }
            set
            {
                if (_designContext != value)
                {
                    _designContext = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDesignerActive
        {
            get { return _isDesignerActive; }
            set
            {
                if (_isDesignerActive != value)
                {
                    _isDesignerActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsGraphActive
        {
            get { return _isGraphActive; }
            set
            {
                if (_isGraphActive != value)
                {
                    _isGraphActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCodeActive
        {
            get { return _isCodeActive; }
            set
            {
                if (_isCodeActive != value)
                {
                    _isCodeActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool DesignerLoadFailed
        {
            get { return _designerLoadFailed; }
            set
            {
                if (_designerLoadFailed != value)
                {
                    _designerLoadFailed = value;
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
        public XamlDocument(Microsoft.CodeAnalysis.Document document)
        {
            this.ContentId = Constants.WPF_DESIGNER;
            this.IsDesignerActive = true;
            this.Document = document;
            this.Name = document.Name;
            this.FilePath = document.FilePath;

            this.AvalonDocument = new ICSharpCode.AvalonEdit.Document.TextDocument() { FileName = this.Name, Text = File.ReadAllText(FilePath) };
            this.AvalonDocument.TextChanged += AvalonDocument_TextChanged;
        }

        private void AvalonDocument_TextChanged(object sender, EventArgs e)
        {
            this.IsSaved = false;
        }

        #endregion

        #region [Public Methods]

        public override void Save()
        {
            File.WriteAllText(this.FilePath, this.AvalonDocument.Text);
            this.IsSaved = true;
        }

        public override void Reload()
        {
            OnPropertyChanged(nameof(AvalonDocument));
            base.Reload();
        }

        #endregion

        #region [Private Methods]

        #endregion
    }
}