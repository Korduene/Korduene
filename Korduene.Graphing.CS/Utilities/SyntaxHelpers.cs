using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;

namespace Korduene.Graphing.CS.Utilities
{
    public static class SyntaxHelpers
    {
        public static SyntaxTokenList GetModifiers(string modifiers)
        {
            var mod = new List<SyntaxToken>();

            foreach (var item in modifiers.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (Enum.TryParse<SyntaxKind>($"{item}keyword", true, out SyntaxKind syntaxKind))
                {
                    mod.Add(SyntaxFactory.Token(syntaxKind));
                }
            }

            if (mod.Count == 0)
            {
                mod.Add(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
            }

            return SyntaxFactory.TokenList(mod);
        }
    }
}
