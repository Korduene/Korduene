using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    public class Int32TextBox : PortTextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!int.TryParse(this.Text, out int result))
            {
                this.Text = "0";
            }

            base.OnTextChanged(e);
        }
    }
}
