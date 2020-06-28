using Korduene.UI.WPF.CustomControls;
using System.Windows;
using System.Windows.Input;

namespace Korduene.UI.WPF.ToolWindows.Views
{
    /// <summary>
    /// Interaction logic for ToolBoxView.xaml
    /// </summary>
    public partial class ToolBoxView : UserControlEx
    {
        private ICSharpCode.WpfDesign.Designer.Services.CreateComponentTool _tool;

        public ToolBoxView()
        {
            InitializeComponent();
            tree.PreviewMouseLeftButtonDown += Tree_PreviewMouseLeftButtonDown;
            tree.PreviewMouseMove += Tree_PreviewMouseMove;
            tree.SelectedItemChanged += Tree_SelectedItemChanged;
        }

        private void Tree_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrepareToolBoxItem();
        }

        private void Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PrepareToolBoxItem();
        }

        private void Tree_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (_tool != null)
                {
                    DragDrop.DoDragDrop(tree, _tool, DragDropEffects.Copy);
                }
            }
        }

        private void PrepareToolBoxItem()
        {
            if (tree.SelectedItem is KToolboxItem item)
            {
                _tool = new ICSharpCode.WpfDesign.Designer.Services.CreateComponentTool(item.Type);

                if (Current.Instance.ActiveDocument is XamlDocument designerDocument && designerDocument.DesignContext is ICSharpCode.WpfDesign.DesignContext context)
                {
                    context.Services.Tool.CurrentTool = _tool;
                }
            }
        }
    }
}
