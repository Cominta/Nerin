using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers.Items
{
    public enum TokensKind
    {
        Space,
        Bad,
        End,

        Number,

        Plus,
        Minus,
        Divide,
        Multi,

        LeftBracket,
        RightBracket,

        BinaryExpr,
        BracketsExpr,
        LiteralExpr
    }

    // Low-level token
    public class SyntaxToken : Expr
    {
        public override TokensKind Kind { get; }
        public string Text { get; }
        public object Value { get; }

        public SyntaxToken(TokensKind kind, string text, object value)
        {
            Kind = kind;
            Text = text;
            Value = value;
        }

        public override IEnumerable<object> GetChild()
        {
            return Enumerable.Empty<Expr>();
        }
    }

    public abstract class Expr
    {
        public abstract TokensKind Kind { get; }
        public abstract IEnumerable<object> GetChild();
    }

    class BinaryExpr : Expr
    {
        public Expr Left { get; }
        public Expr Right { get; }
        public SyntaxToken Operator { get; }
        public override TokensKind Kind => TokensKind.BinaryExpr;

        public BinaryExpr(Expr left, Expr right, SyntaxToken _operator)
        {
            Left = left;
            Right = right;
            Operator = _operator;
        }

        public override IEnumerable<object> GetChild()
        {
            yield return Left;
            yield return Operator;
            yield return Right;
        }
    }

    class BracketsExpr : Expr
    {
        public SyntaxToken LeftBracket { get; }
        public SyntaxToken RightBracket { get; }
        public Expr Expression { get; }
        public override TokensKind Kind => TokensKind.BracketsExpr;

        public BracketsExpr(SyntaxToken leftBracket, SyntaxToken rightBracket, Expr expression)
        {
            LeftBracket = leftBracket;
            RightBracket = rightBracket;
            Expression = expression;
        }

        public override IEnumerable<object> GetChild()
        {
            yield return LeftBracket;
            yield return Expression;
            yield return RightBracket;
        }
    }

    class LiteralExpr : Expr
    {
        public SyntaxToken Literal { get; }
        public override TokensKind Kind => TokensKind.LiteralExpr;

        public LiteralExpr(SyntaxToken literal)
        {
            Literal = literal;
        }

        public override IEnumerable<object> GetChild()
        {
            yield return Literal;
        }
    }
}