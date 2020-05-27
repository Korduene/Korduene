using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Korduene.Graphing.CS
{
    public static class SymbolProvider
    {
        private static List<ISymbol> _allCache;

        public static List<ProjectSymbolCache> ProjectSymbols { get; set; }

        static SymbolProvider()
        {
            ProjectSymbols = new List<ProjectSymbolCache>();
        }

        public static ProjectSymbolCache GetProjectSymbolCache(string projectName)
        {
            return ProjectSymbols.Find(x => x.ProjectName == projectName);
        }

        public static ISymbol GetSymbol(string name)
        {
            if (_allCache == null)
            {
                _allCache = new List<ISymbol>();

                foreach (var sym in ProjectSymbols.SelectMany(x => x.GetSymbols()))
                {
                    _allCache.Add(sym);

                    if (sym is ITypeSymbol ts)
                    {
                        var members = ts.GetMembers();
                        foreach (var m in members)
                        {
                            if (m is IMethodSymbol methodSymbol && methodSymbol.MethodKind == MethodKind.Ordinary && methodSymbol.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Public && methodSymbol.IsStatic)
                            {
                                _allCache.Add(m);
                            }
                            else if (m is IPropertySymbol propertySymbol && propertySymbol.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Public && propertySymbol.IsStatic)
                            {
                                _allCache.Add(propertySymbol);
                            }
                        }
                    }

                    //_cache = AssemblySymbols.SelectMany(x => x.Symbols).ToList().Distinct();
                }
            }

            return _allCache.Find(x => x?.ToString() == name);
        }
    }
}
