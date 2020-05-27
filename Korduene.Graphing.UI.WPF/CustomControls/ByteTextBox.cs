using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    public class ByteTextBox : PortTextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!byte.TryParse(this.Text, out byte result))
            {
                this.Text = "0";
            }

            base.OnTextChanged(e);
        }
    }
}
