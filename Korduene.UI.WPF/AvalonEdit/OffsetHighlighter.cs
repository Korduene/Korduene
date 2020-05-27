using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Rendering;
using System.Windows;
using System.Windows.Media;

namespace Korduene.UI.WPF.AvalonEdit
{
    /// <summary>
    /// Highlights current selected line
    /// </summary>
    public class OffsetHighlighter : IBackgroundRenderer
    {
        private TextEditor _editor;

        /// <summary>
        /// Layer
        /// </summary>
        public KnownLayer Layer
        {
            get { return KnownLayer.Caret; }
        }

        /// <summary>
        /// Highlights current selected line
        /// </summary>
        /// <param name="editor">editor</param>
        public OffsetHighlighter(TextEditor editor)
        {
            _editor = editor;
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="textView"></param>
        /// <param name="drawingContext"></param>
        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (_editor.Document == null)
            {
                return;
            }

            textView.EnsureVisualLines();
            var currentLine = _editor.Document.GetLineByOffset(_editor.CaretOffset);
            foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, currentLine))
            {
                var rc = new System.Windows.Shapes.Rectangle();
                rc.Height = rect.Height;
                rc.Width = textView.ActualWidth;
                rc.Stroke = new SolidColorBrush(Colors.LightGray);
                rc.StrokeThickness = 1;
                var vb = new VisualBrush(rc);

                drawingContext.DrawRectangle(vb, null, new Rect(rect.Location, new Size(textView.ActualWidth, rect.Height)));
            }
        }
    }
}
