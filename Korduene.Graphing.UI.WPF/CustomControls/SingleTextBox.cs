using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    public class SingleTextBox : PortTextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!float.TryParse(this.Text, out float result))
            {
                this.Text = "0";
            }

            base.OnTextChanged(e);
        }
    }
}
