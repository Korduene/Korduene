using System.Windows;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for ComboBox.xaml
    /// </summary>
    public partial class ComboBox : System.Windows.Controls.ComboBox
    {
        public ComboBox()
        {
            InitializeComponent();
            this.Loaded += ComboBox_Loaded;
        }

        void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.FlowDirection = System.Windows.FlowDirection.LeftToRight;
        }
    }
}
