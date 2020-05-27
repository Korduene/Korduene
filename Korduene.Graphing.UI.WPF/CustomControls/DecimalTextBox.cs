using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    public class DecimalTextBox : PortTextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!decimal.TryParse(this.Text, out decimal result))
            {
                this.Text = "0";
            }

            base.OnTextChanged(e);
        }
    }
}
