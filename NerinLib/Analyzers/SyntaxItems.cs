using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
        OppositeBool,
        And,
        Or,
        Equal,
        NotEqual,
        TrueValue,
        FalseValue,
        Name,
        Assigment,

        Plus,
        Minus,
        Divide,
        Multi,

        LeftBracket,
        RightBracket,
        OpenBrace,
        CloseBrace,

        BinaryExpr,
        BracketsExpr,
        LiteralExpr,
        UnaryExpr,

        CompilationUnit,
        BlockStatement,
        ExpressionStatement
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

    public abstract class Syntax
    {
        public abstract TokensKind Kind { get; }
    }

    public abstract class Expr : Syntax
    {
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

    class NameExpr : Expr
    {
        public SyntaxToken Token { get; }
        public override TokensKind Kind => TokensKind.Name;

        public NameExpr(SyntaxToken token)
        {
            Token = token;
        }

        public override IEnumerable<object> GetChild()
        {
            yield return Token;
        }
    }

    class AssigmentExpr : Expr
    {
        public SyntaxToken Token { get; }
        public Expr Expression { get; }
        public SyntaxToken EqualsToken { get; }
        public override TokensKind Kind => TokensKind.Assigment;

        public AssigmentExpr(SyntaxToken token, SyntaxToken equalsToken, Expr expr)
        {
            Token = token;
            EqualsToken = equalsToken;
            Expression = expr;
        }

        public override IEnumerable<object> GetChild()
        {
            yield return Token;
            yield return EqualsToken;
            yield return Expression;
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

    class UnaryExpr : Expr
    {
        public Expr Expression { get; }
        public SyntaxToken Unary { get; }
        public override TokensKind Kind => TokensKind.UnaryExpr;

        public UnaryExpr(Expr expression, SyntaxToken token)
        {
            Expression = expression;
            Unary = token;
        }

        public override IEnumerable<object> GetChild()
        {
            yield return Unary;
            yield return Expression;
        }
    }
}