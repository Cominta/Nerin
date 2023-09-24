using System;
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
        End
    }

    // Low-level token
    public class SyntaxToken
    {
        public TokensKind Kind { get; }
        public string Text { get; }
        public object Value { get; }

        public SyntaxToken(TokensKind kind, string text, object value)
        {
            Kind = kind;
            Text = text;
            Value = value;
        }
    }

    abstract class Expr
    {

    }

    class BinaryExpr : Expr
    {
        public Expr Left { get; }
        public Expr Right { get; }
        public SyntaxToken Operator { get; }

        public BinaryExpr(Expr left, Expr right, SyntaxToken _operator)
        {
            Left = left;
            Right = right;
            Operator = _operator;
        }
    }

    class NumberExpr : Expr
    {
        public SyntaxToken Number { get; }

        public NumberExpr(SyntaxToken number)
        {
            Number = number;
        }
    }
}
