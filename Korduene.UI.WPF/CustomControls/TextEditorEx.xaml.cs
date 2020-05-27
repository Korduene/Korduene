using ICSharpCode.AvalonEdit;
using Korduene.UI.WPF.AvalonEdit;
using System.Windows.Media;

namespace Korduene.UI.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for TextEditorEx.xaml
    /// </summary>
    public partial class TextEditorEx : TextEditor
    {
        public TextEditorEx()
        {
            InitializeComponent();
            this.TextArea.SelectionBrush = new SolidColorBrush(Color.FromArgb(150, 0, 148, 245));
            this.TextArea.SelectionCornerRadius = 0;
            this.TextArea.SelectionBorder = null;
            this.ShowLineNumbers = true;

            //syntax highlighting
            this.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("C#");
            this.TextArea.TextView.BackgroundRenderers.Add(new OffsetHighlighter(this));
        }
    }
}
