using Microsoft.CodeAnalysis.Text;
using System;

namespace Korduene
{
    public class KordueneSourceTextContainer : SourceTextContainer
    {
        public override event EventHandler<TextChangeEventArgs> TextChanged;

        private SourceText _sourceText;

        public override SourceText CurrentText { get { return _sourceText; } }

        public KordueneSourceTextContainer(string file)
        {
            _sourceText = SourceText.From(System.IO.File.ReadAllText(file));
        }
    }
}
