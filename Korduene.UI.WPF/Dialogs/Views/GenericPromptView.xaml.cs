using System.Windows;

namespace Korduene.UI.WPF.Dialogs.Views
{
    /// <summary>
    /// Interaction logic for GenericPromptView.xaml
    /// </summary>
    public partial class GenericPromptView : Window
    {
        public GenericPromptView()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
