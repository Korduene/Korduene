using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    public class Int64TextBox : PortTextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!long.TryParse(this.Text, out long result))
            {
                this.Text = "0";
            }

            base.OnTextChanged(e);
        }
    }
}
