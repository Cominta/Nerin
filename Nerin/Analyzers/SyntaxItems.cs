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
        Number,
        Plus,
        Minus,
        Divide,
        Multi,
        LeftBracket,
        RightBracket,
        Space,
        Bad,
        End,
        BinaryExpr,
        NumberExpr
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

    class NumberExpr : Expr
    {
        public SyntaxToken Number { get; }
        public override TokensKind Kind => TokensKind.NumberExpr;

        public NumberExpr(SyntaxToken number)
        {
            Number = number;
        }

        public override IEnumerable<object> GetChild()
        {
            yield return Number;
        }
    }
}
