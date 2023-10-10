using Nerin;
using Nerin.Analyzers;
using Nerin.Analyzers.Binder;
using Nerin.Analyzers.Binder.Items;
using Nerin.Analyzers.Items;
using NerinLib.Analyzers;
using NerinLib.Analyzers.Binder;
using NerinLib.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NerinLib
{
    public class Compilation
    {
        public SyntaxTree Tree { get; }
        public Compilation Previous { get; }
        private BoundGlobalScope globalScope;

        public Compilation(SyntaxTree tree) 
            : this(null, tree)
        {
        }

        private Compilation(Compilation previous, SyntaxTree tree)
        {
            Tree = tree;
            Previous = previous;
        }

        public BoundGlobalScope GlobalScope 
        { 
            get 
            { 
                if (globalScope == null)
                {
                    globalScope = Binder.BindGlobal(Previous?.GlobalScope, new CompilationUnit(Tree));
                }

                return globalScope;
            } 
        }

        public Compilation ContinueWith(SyntaxTree tree)
        {
            return new Compilation(this, tree);
        }

        public EvaluationResult EvaluateResult(Dictionary<VariableSymbol, object> variables)
        {
            BoundExpr expr = GlobalScope.Expression;

            Evaulator evaulator = new Evaulator(expr, variables);
            return new EvaluationResult(evaulator.Evaluate());
        }
    }

    // In future will contain errors and some information
    public class EvaluationResult
    {
        public object Value { get; }

        public EvaluationResult(object value) 
        {
            Value = value;
        }
    }

    public class CompilationUnit
    {
        public SyntaxTree Tree { get; }
        //public SyntaxToken TokenEnd { get; }

        public CompilationUnit(SyntaxTree tree/*, SyntaxToken End*/)
        {
            Tree = tree; 
            //TokenEnd = End;
        }
    }
}
