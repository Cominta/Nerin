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
                    globalScope = Binder.BindGlobal(Previous?.GlobalScope, new CompilationUnit(Tree.Root.Statement));
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

    public class CompilationUnit : Syntax
    {
        public override TokensKind Kind => TokensKind.CompilationUnit;
        public Statement Statement { get; }
        //public SyntaxToken TokenEnd { get; }

        public CompilationUnit(Statement statement/*, SyntaxToken End*/)
        {
            Statement = statement; 
            //TokenEnd = End;
        }
    }

    public abstract class Statement : Syntax
    {
         
    }

    public class BlockStatement : Statement
    {
        public SyntaxToken OpenBrace { get; }
        public SyntaxToken CloseBrace { get; }
        public ImmutableArray<Statement> Statements { get; }
        public override TokensKind Kind => TokensKind.BlockStatement;

        public BlockStatement(SyntaxToken openBrace, ImmutableArray<Statement> statements, SyntaxToken closeBrace)
        {
            OpenBrace = openBrace;
            CloseBrace = closeBrace;
            Statements = statements;
        }
    }

    public class ExprStatement : Statement
    {
        public override TokensKind Kind => TokensKind.ExpressionStatement;
        public Expr Expression { get; }

        public ExprStatement(Expr expression)
        {
            Expression = expression;
        }
    }
}
