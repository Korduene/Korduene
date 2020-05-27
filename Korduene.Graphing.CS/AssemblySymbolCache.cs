using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Korduene
{
    [DebuggerDisplay("{AssemblySymbol.Name}, {Symbols.Count}")]
    public class AssemblySymbolCache
    {
        public IAssemblySymbol AssemblySymbol { get; set; }

        public List<ISymbol> Symbols { get; set; }

        public AssemblySymbolCache()
        {
            Symbols = new List<ISymbol>();
        }

        public AssemblySymbolCache(IAssemblySymbol assemblySymbol) : this()
        {
            this.AssemblySymbol = assemblySymbol;
        }

        public AssemblySymbolCache(IAssemblySymbol assemblySymbol, IEnumerable<ISymbol> symbols) : this(assemblySymbol)
        {
            Symbols = symbols.ToList();
        }
    }
}
