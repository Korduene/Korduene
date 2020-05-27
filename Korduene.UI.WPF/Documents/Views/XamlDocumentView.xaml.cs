using ICSharpCode.WpfDesign.Designer.Xaml;
using ICSharpCode.WpfDesign.XamlDom;
using Korduene.UI.WPF.CustomControls;
using Korduene.UI.WPF.Helpers;
using System;
using System.Text;
using System.Windows;
using System.Xml;

namespace Korduene.UI.WPF.Documents.Views
{
    /// <summary>
    /// Interaction logic for XamlDocumentView.xaml
    /// </summary>
    public partial class XamlDocumentView : UserControlEx
    {
        private bool _initialLoad;
        private bool _isSavingDesigner;

        public XamlDocumentView()
        {
            InitializeComponent();
            this.DataContextChanged += GraphDocumentView_DataContextChanged;
            this.Loaded += DesignerDocumentView_Loaded;
            designControl.PropertyChanged += DesignControl_PropertyChanged;
        }

        private void DesignControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(designControl.DesignContext))
            {
                (DataContext as XamlDocument).DesignContext = designControl.DesignContext;
            }
        }

        private void UndoService_UndoStackChanged(object sender, EventArgs e)
        {
            SaveDesigner();
        }

        private void GraphDocumentView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var doc = DataContext as XamlDocument;
            var menu = new Graphing.UI.WPF.GraphContextMenu();
            doc.AvalonDocument.TextChanged += AvalonXamlDocument_TextChanged;
            //doc.PropertyChanged += Doc_PropertyChanged;
        }

        private void DesignerDocumentView_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_initialLoad)
            {
                LoadDesigner();
                _initialLoad = true;
            }
        }

        private void AvalonXamlDocument_TextChanged(object sender, EventArgs e)
        {
            LoadDesigner();
        }

        //private void Doc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(CSDesignerDocument.AvalonXamlDocument))
        //    {
        //        LoadDesigner();
        //    }
        //}

        private void LoadDesigner()
        {
            if (_isSavingDesigner)
            {
                return;
            }

            try
            {
                var vm = this.DataContext as XamlDocument;

                using (var reader = XmlReader.Create(new System.IO.StringReader(vm.AvalonDocument.Text)))
                {
                    KordueneTypeFinder.RefreshAssemblies();
                    var settings = new XamlLoadSettings();

                    foreach (var item in KordueneTypeFinder.Instance.RegisteredAssemblies)
                    {
                        settings.DesignerAssemblies.Add(item);
                    }

                    settings.TypeFinder = KordueneTypeFinder.Instance;

                    designControl.LoadDesigner(reader, settings);

                    if (designControl.DesignContext != null)
                    {
                        var undoService = designControl.DesignContext.Services.GetService<ICSharpCode.WpfDesign.Designer.Services.UndoService>();
                        undoService.UndoStackChanged -= UndoService_UndoStackChanged;
                        undoService.UndoStackChanged += UndoService_UndoStackChanged;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SaveDesigner()
        {
            try
            {
                _isSavingDesigner = true;

                var vm = this.DataContext as XamlDocument;

                var sb = new StringBuilder();

                using (var xamlWriter = new ICSharpCode.WpfDesign.XamlDom.XamlXmlWriter(sb))
                {
                    designControl.SaveDesigner(xamlWriter);
                    vm.AvalonDocument.Text = XamlFormatter.Format(sb.ToString());
                }

                _isSavingDesigner = false;
            }
            catch (Exception)
            {
            }
        }
    }
}

