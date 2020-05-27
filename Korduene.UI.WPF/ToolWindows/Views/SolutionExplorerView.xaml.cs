using Korduene.UI.WPF.CustomControls;
using Korduene.UI.WPF.ToolWindows.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Korduene.UI.WPF.ToolWindows.Views
{
    /// <summary>
    /// Interaction logic for SolutionExplorerView.xaml
    /// </summary>
    public partial class SolutionExplorerView : UserControlEx
    {
        private bool _noExpansion;

        public SolutionExplorerView()
        {
            InitializeComponent();
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;

            if (!item.IsSelected)
            {
                return;
            }

            if (item.DataContext is WorkspaceItem wsi && wsi.Type == WorkspaceItemType.File)
            {
                _noExpansion = true;
                (DataContext as SolutionExplorerViewModel).ItemDoubleClickCommand.Execute(item.DataContext);
            }
        }

        private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
        {
            var item = sender as TreeViewItem;

            if (item.DataContext is WorkspaceItem wsi && wsi.Type == WorkspaceItemType.Solution)
            {
                item.IsExpanded = true;
            }
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            var item = sender as TreeViewItem;

            if (_noExpansion)
            {
                item.IsExpanded = false;
                _noExpansion = false;
            }
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem item && item.DataContext is WorkspaceItem wsi)
            {
                wsi.IsSelected = true;
            }
        }
    }
}
