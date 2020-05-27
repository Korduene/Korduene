using System.Windows.Controls;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF
{
    public static class TreeViewItemExtensions
    {
        public static int GetDepth(this TreeViewItem item)
        {
            System.Windows.FrameworkElement elem = item;
            var parent = VisualTreeHelper.GetParent(item);
            var count = 0;
            while (parent != null && !(parent is TreeView))
            {
                var tvi = parent as TreeViewItem;
                if (parent is TreeViewItem)
                    count++;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return count;
        }

        private static TreeViewItem GetParent(TreeViewItem item)
        {
            var parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TreeViewItem;
        }
    }
}
