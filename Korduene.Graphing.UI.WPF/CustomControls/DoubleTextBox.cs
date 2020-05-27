using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    public class DoubleTextBox : PortTextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!double.TryParse(this.Text, out double result))
            {
                this.Text = "0";
            }

            base.OnTextChanged(e);
        }
    }
}
