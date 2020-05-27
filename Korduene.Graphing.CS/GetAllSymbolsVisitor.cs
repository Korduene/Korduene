using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Korduene.Graphing.CS
{
    public class GetAllSymbolsVisitor : SymbolVisitor
    {
        public List<INamedTypeSymbol> NamedTypes { get; set; } = new List<INamedTypeSymbol>();

        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            Parallel.ForEach(symbol.GetMembers(), s => s.Accept(this));
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.TypeKind != TypeKind.Module && symbol.DeclaredAccessibility == Accessibility.Public)
            {
                if (!NamedTypes.Contains(symbol))
                {
                    NamedTypes.Add(symbol);
                }
            }
        }
    }
}
