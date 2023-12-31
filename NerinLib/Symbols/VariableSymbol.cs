﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib.Symbols
{
    public class VariableSymbol : Symbol
    {
        public override SymbolKind Kind => SymbolKind.Var;
        public Type Type { get; }
        public bool isReadOnly { get; }

        public VariableSymbol(string name, bool readOnly, Type type)
                : base(name)
        {
            Type = type;
            isReadOnly = readOnly;
        }
    }
}
