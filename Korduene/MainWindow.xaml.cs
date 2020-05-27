using AvalonDock.Layout.Serialization;
using Korduene.UI.WPF;
using Korduene.UI.WPF.Documents.ViewModels;
using Korduene.UI.WPF.ToolWindows.ViewModels;
using System;
using System.Windows;

namespace Korduene
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _layoutLoaded;
        private string _layoutFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "layout.xml");

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            UIServices.MainWindow = this;
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_layoutLoaded)
            {
                if (System.IO.File.Exists(_layoutFile))
                {
                    var layoutSerializer = new XmlLayoutSerializer(dockMgr);
                    layoutSerializer.LayoutSerializationCallback += LayoutSerializer_LayoutSerializationCallback;
                    layoutSerializer.Deserialize(_layoutFile);
                }

                _layoutLoaded = true;
            }
        }

        private void LayoutSerializer_LayoutSerializationCallback(object sender, LayoutSerializationCallbackEventArgs e)
        {
            var mvm = this.DataContext as MainWindowViewModel;

            if (e.Model.ContentId == null)
            {
                e.Cancel = true;
                return;
            }

            if (e.Model.ContentId.Equals(Korduene.UI.WPF.Constants.ContentIds.TOOLBOX, StringComparison.OrdinalIgnoreCase))
            {
                var vm = new ToolBoxViewModel();
                e.Content = vm;
                mvm.AddToolsInternal(vm);
            }
            else if (e.Model.ContentId.Equals(Korduene.UI.WPF.Constants.ContentIds.PROPERTIES, StringComparison.OrdinalIgnoreCase))
            {
                var vm = new PropertiesViewModel();
                e.Content = vm;
                mvm.AddToolsInternal(vm);
            }
            else if (e.Model.ContentId.Equals(Korduene.UI.WPF.Constants.ContentIds.SOLUTION_EXPLORER, StringComparison.OrdinalIgnoreCase))
            {
                var vm = new SolutionExplorerViewModel();
                e.Content = vm;
                mvm.AddToolsInternal(vm);
            }
            else if (e.Model.ContentId.Equals(Korduene.UI.WPF.Constants.ContentIds.OUTPUT, StringComparison.OrdinalIgnoreCase))
            {
                var vm = new OutputViewModel();
                e.Content = vm;
                mvm.AddToolsInternal(vm);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            var layoutSerializer = new XmlLayoutSerializer(dockMgr);
            layoutSerializer.Serialize(_layoutFile);
        }

        private void dockMgr_ActiveContentChanged(object sender, EventArgs e)
        {
            if ((sender as AvalonDock.DockingManager).ActiveContent is UI.KDocument kdoc)
            {
                Current.Instance.ActiveDocument = kdoc;
            }
        }
    }
}
