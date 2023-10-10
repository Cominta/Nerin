using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib.Symbols
{
    public enum SymbolKind
    {
        GlobalVar,
        LocalVar,
        Var,
        Type
    }

    public abstract class Symbol
    {
        public abstract SymbolKind Kind { get; }
        public string Name { get; }

        public Symbol(string name)
        {
            Name = name;
        }
    }
}
