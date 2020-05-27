using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF
{
    public class DisplayPartContent
    {
        public string Text { get; set; }

        public SolidColorBrush Foreground { get; set; }

        public DisplayPartContent()
        {
        }

        public DisplayPartContent(string text, SolidColorBrush foreground)
        {
            this.Text = text;
            this.Foreground = foreground;
        }
    }
}
