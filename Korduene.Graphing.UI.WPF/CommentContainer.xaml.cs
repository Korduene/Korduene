using System.Windows;
using System.Windows.Input;

namespace Korduene.Graphing.UI.WPF
{
    /// <summary>
    /// Interaction logic for NodeContainer.xaml
    /// </summary>
    public partial class CommentContainer : MemberContainer
    {
        public CommentContainer()
        {
            InitializeComponent();
        }

        private void tbComment_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                tbComment.Visibility = Visibility.Collapsed;
                txtComment.Visibility = Visibility.Visible;
            }
        }

        private void txtComment_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                txtComment.Visibility = Visibility.Collapsed;
                tbComment.Visibility = Visibility.Visible;
            }
        }
    }
}
