using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib.Analyzers.Statements
{
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

    public class VariableDeclarationStatement : Statement
    {
        public override TokensKind Kind => TokensKind.VariableDeclarationStatement;

        public SyntaxToken Keyword { get; }
        public SyntaxToken Identifier { get; }
        public SyntaxToken EqualsToken { get; }
        public Expr Initializer { get; }

        public VariableDeclarationStatement(SyntaxToken keyword, SyntaxToken identifier, SyntaxToken equalsToken, Expr initializer)
        {
            Keyword = keyword;
            Identifier = identifier;
            EqualsToken = equalsToken;
            Initializer = initializer;
        }
    }
}
