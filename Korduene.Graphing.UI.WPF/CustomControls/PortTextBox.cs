using Korduene.Graphing.UI.WPF.Converters;
using System.Windows.Data;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    public class PortTextBox : Graphing.UI.WPF.CustomControls.TextBox
    {
        public PortTextBox()
        {
            this.Background = Brushes.Transparent;
            this.Height = 14;
            this.FontSize = 9;
            this.MinWidth = 30;
            this.MaxWidth = 100;
            this.Foreground = Brushes.White;
            this.CaretBrush = Brushes.White;
            this.SetBinding(PortTextBox.BorderBrushProperty, new Binding(nameof(Port.BorderColor)) { Converter = new GraphColorToSolidBrushConverter() });
        }
    }
}
