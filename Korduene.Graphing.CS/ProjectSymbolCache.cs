using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Korduene
{
    [DebuggerDisplay("{ProjectName}, {AssemblySymbols.Count}")]
    public class ProjectSymbolCache
    {
        private IEnumerable<ISymbol> _cache;

        public string ProjectName { get; set; }

        public List<AssemblySymbolCache> AssemblySymbols { get; set; }

        public ProjectSymbolCache()
        {
            AssemblySymbols = new List<AssemblySymbolCache>();
        }

        public ProjectSymbolCache(string projectName) : this()
        {
            this.ProjectName = projectName;
        }

        public IEnumerable<ISymbol> GetSymbols()
        {
            if (_cache == null)
            {
                return _cache = AssemblySymbols.SelectMany(x => x.Symbols).ToList().Distinct();
            }

            return _cache;
        }
    }
}
