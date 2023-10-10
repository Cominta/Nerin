using Nerin.Analyzers.Binder.Items;
using NerinLib.Symbols;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib.Analyzers.Binder
{
    public class BoundScope
    {
        private Dictionary<string, VariableSymbol> Variables = new Dictionary<string, VariableSymbol>();
        public BoundScope Parent { get; }

        public BoundScope(BoundScope parent)
        {
            Parent = parent;
        }

        public bool TryDeclare(VariableSymbol value)
        {
            if (Variables.ContainsKey(value.Name))
            {
                return false;
            }

            Variables.Add(value.Name, value);
            return true;
        }

        public bool TryLookup(string name, out VariableSymbol value)
        {
            if (Variables.TryGetValue(name, out value))
            {
                return true;
            }

            if (Parent == null)
            {
                return false;
            }

            return Parent.TryLookup(name, out value);
        }

        public ImmutableArray<VariableSymbol> GetVariables()
        {
            return Variables.Values.ToImmutableArray<VariableSymbol>();
        }
    }

    public class BoundGlobalScope
    {
        public BoundGlobalScope Previous { get; }
        public ImmutableArray<VariableSymbol> Variables;
        public BoundExpr Expression;

        public BoundGlobalScope(BoundGlobalScope previous, ImmutableArray<VariableSymbol> vars, BoundExpr expr)
        {
            Previous = previous;
            Variables = vars;
            Expression = expr;
        }
    }
}
