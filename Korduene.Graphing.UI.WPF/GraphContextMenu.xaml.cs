using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace Korduene.Graphing.UI.WPF
{
    /// <summary>
    /// Interaction logic for GraphContextMenu.xaml
    /// </summary>
    public partial class GraphContextMenu : Popup, IGraphContextMenuControl
    {
        private DispatcherTimer _timer;

        public GraphContextMenuProvider GraphContextMenuProvider
        {
            get
            {
                return DataContext as GraphContextMenuProvider;
            }
        }

        public GraphContextMenu()
        {
            InitializeComponent();
            _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Normal, OnTimerTick, this.Dispatcher) { IsEnabled = false };
        }

        public void Invoke(Action action)
        {
            this.Dispatcher.Invoke(action);
        }

        protected override void OnOpened(EventArgs e)
        {
            txtSearch.Focus();
            base.OnOpened(e);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _timer.Stop();
            GraphContextMenuProvider.Search(txtSearch.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _timer.Stop();
            _timer.Start();
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem tvi && tvi.IsSelected)
            {
                var item = (e.Source as TreeViewItem).Header as GraphMenuItem;

                if (item == null)
                {
                    return;
                }

                if (item.TypeInfo is Microsoft.CodeAnalysis.INamespaceSymbol)
                {
                    return;
                }

                IsOpen = false;
                GraphContextMenuProvider.AddNode(item);
            }
        }

        private void CreateInstance_Click(object sender, RoutedEventArgs e)
        {
            this.IsOpen = false;
            (GraphContextMenuProvider as CS.CSGraphContextMenuProvider).ExecuteCreateInstanceCommand((sender as MenuItem).DataContext as GraphMenuItem);
        }

        private void CreateVariable_Click(object sender, RoutedEventArgs e)
        {
            this.IsOpen = false;
            (GraphContextMenuProvider as CS.CSGraphContextMenuProvider).ExecuteCreateVariableCommand((sender as MenuItem).DataContext as GraphMenuItem);
        }

        private void CreateProperty_Click(object sender, RoutedEventArgs e)
        {
            this.IsOpen = false;
            (GraphContextMenuProvider as CS.CSGraphContextMenuProvider).ExecuteCreatePropertyCommand((sender as MenuItem).DataContext as GraphMenuItem);
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem item)
            {
                item.IsSelected = true;
            }
        }
    }
}
