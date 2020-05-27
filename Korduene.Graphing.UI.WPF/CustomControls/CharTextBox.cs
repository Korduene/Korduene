using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    public class CharTextBox : PortTextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!char.TryParse(this.Text, out char result))
            {
                this.Text = "0";
            }

            base.OnTextChanged(e);
        }
    }
}
