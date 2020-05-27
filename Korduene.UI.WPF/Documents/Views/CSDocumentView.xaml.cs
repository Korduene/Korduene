using Korduene.Graphing.CS;
using Korduene.UI.WPF.CustomControls;
using System.Windows;

namespace Korduene.UI.WPF.Documents.Views
{
    /// <summary>
    /// Interaction logic for CSDocumentView.xaml
    /// </summary>
    public partial class CSDocumentView : UserControlEx
    {
        public CSDocumentView()
        {
            InitializeComponent();
            this.DataContextChanged += CSDocumentView_DataContextChanged;
        }

        private void CSDocumentView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var doc = DataContext as CSDocument;
            //graphControl.Graph = doc.Graph;
            graphControl.DataContext = doc.Graph;
            var menu = new Graphing.UI.WPF.GraphContextMenu();
            menu.DataContext = new CSGraphContextMenuProvider(doc.Graph, menu, doc.Document);
            graphControl.PopupMenu = menu; // { StaysOpen = true };
        }
    }
}
