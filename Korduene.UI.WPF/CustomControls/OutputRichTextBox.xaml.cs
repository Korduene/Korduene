using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Korduene.UI.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for OutputRichTextBox.xaml
    /// </summary>
    public partial class OutputRichTextBox : RichTextBox
    {
        private SolidColorBrush _currentBrush = Brushes.Black;

        public OutputRichTextBox()
        {
            InitializeComponent();
            Current.Instance.Output += Instance_Output;
            Current.Instance.OutputClear += Instance_OutputClear;
            Current.Instance.OutputResetColor += Instance_OutputResetColor;
            Current.Instance.OutputSetColor += Instance_OutputSetColor;
        }

        private void Instance_OutputSetColor(object sender, ConsoleColor e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                switch (e)
                {
                    case ConsoleColor.Black:
                        _currentBrush = Brushes.Black;
                        break;
                    case ConsoleColor.DarkBlue:
                        _currentBrush = Brushes.DarkBlue;
                        break;
                    case ConsoleColor.DarkGreen:
                        _currentBrush = Brushes.DarkGreen;
                        break;
                    case ConsoleColor.DarkCyan:
                        _currentBrush = Brushes.DarkCyan;
                        break;
                    case ConsoleColor.DarkRed:
                        _currentBrush = Brushes.DarkRed;
                        break;
                    case ConsoleColor.DarkMagenta:
                        _currentBrush = Brushes.DarkMagenta;
                        break;
                    case ConsoleColor.DarkYellow:
                        _currentBrush = Brushes.Yellow;
                        break;
                    case ConsoleColor.Gray:
                        _currentBrush = Brushes.Gray;
                        break;
                    case ConsoleColor.DarkGray:
                        _currentBrush = Brushes.DarkGray;
                        break;
                    case ConsoleColor.Blue:
                        _currentBrush = Brushes.Blue;
                        break;
                    case ConsoleColor.Green:
                        _currentBrush = Brushes.Green;
                        break;
                    case ConsoleColor.Cyan:
                        _currentBrush = Brushes.Blue;
                        break;
                    case ConsoleColor.Red:
                        _currentBrush = Brushes.Red;
                        break;
                    case ConsoleColor.Magenta:
                        _currentBrush = Brushes.Magenta;
                        break;
                    case ConsoleColor.Yellow:
                        _currentBrush = Brushes.Yellow;
                        break;
                    case ConsoleColor.White:
                        _currentBrush = Brushes.White;
                        break;
                    default:
                        _currentBrush = Brushes.Black;
                        break;
                }
            }));
        }

        private void Instance_OutputResetColor(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _currentBrush = Brushes.Black;
            }));
        }

        private void Instance_OutputClear(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Document.Blocks.Clear();
            }));
        }

        private void Instance_Output(object sender, string e)
        {
            if (string.IsNullOrWhiteSpace(e))
            {
                return;
            }

            Dispatcher.BeginInvoke(new Action(() =>
            {
                var range = new TextRange(this.Document.ContentEnd, this.Document.ContentEnd)
                {
                    Text = e.TrimStart()
                };

                range.ApplyPropertyValue(TextElement.ForegroundProperty, _currentBrush);
                this.ScrollToEnd();
            }));
        }
    }
}
