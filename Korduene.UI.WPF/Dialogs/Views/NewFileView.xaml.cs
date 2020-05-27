using System.Windows;

namespace Korduene.UI.WPF.Dialogs.Views
{
    /// <summary>
    /// Interaction logic for NewFileView.xaml
    /// </summary>
    public partial class NewFileView : Window
    {
        public NewFileView()
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
