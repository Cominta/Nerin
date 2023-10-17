using Nerin;
using Nerin.Analyzers;
using Nerin.Analyzers.Binder;
using Nerin.Analyzers.Binder.Items;
using Nerin.Analyzers.Items;
using NerinLib.Analyzers;
using NerinLib.Analyzers.Statements;
using NerinLib.Analyzers.Binder;
using NerinLib.Diagnostics;
using NerinLib.Symbols;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
                    globalScope = Binder.BindGlobal(Previous?.GlobalScope, Tree.Root);
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
            BoundStatement expr = GlobalScope.Statement;

            ImmutableArray<Diagnostic> diagnostics = Tree.Diagnostics.Concat(globalScope.Diagnostics).ToImmutableArray();

            if (diagnostics.Any())
            {
                return new EvaluationResult(null, diagnostics);
            }

            Evaulator evaulator = new Evaulator(expr, variables);
            return new EvaluationResult(evaulator.Evaluate(), Array.Empty<Diagnostic>());
        }
    }

    // In future will contain errors and some information
    public class EvaluationResult
    {
        public object Value { get; }
        public IReadOnlyList<Diagnostic> Diagnostics;

        public EvaluationResult(object value, IEnumerable<Diagnostic> diagnostics) 
        {
            Diagnostics = diagnostics.ToArray();
            Value = value;
        }
    }

    public class CompilationUnit : Syntax
    {
        public override TokensKind Kind => TokensKind.CompilationUnit;
        public Statement Statement { get; }
        public SyntaxToken End { get; }

        public CompilationUnit(Statement statement, SyntaxToken end)
        {
            Statement = statement;
            End = end;
        }
    }

    
}
