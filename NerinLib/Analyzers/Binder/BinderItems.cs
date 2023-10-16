using Nerin.Analyzers.Items;
using NerinLib.Symbols;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers.Binder.Items
{
    public enum BoundNodeKind
    {
        LiteralExpr,
        BinaryExpr,
        UnaryExpr,
        Variable,
        Assigment,

        BlockStatement,
        ExprStatement,
        VariableDeclarationStatement,
        IfStatement,
        WhileStatement
    }

    public abstract class BoundNode
    {
        public abstract BoundNodeKind Kind { get; }
    }

    public abstract class BoundExpr : BoundNode 
    {
        public abstract Type Type { get; }
    }

    public class BoundVariableExpr : BoundExpr
    {
        public VariableSymbol VarSymbol { get; }
        public override Type Type => VarSymbol.Type;
        public override BoundNodeKind Kind => BoundNodeKind.Variable;

        public BoundVariableExpr(VariableSymbol var)
        {
            VarSymbol = var;
        }
    }

    public class BoundAssigmentExpr : BoundExpr
    {
        public override Type Type => Expression.Type;
        public override BoundNodeKind Kind => BoundNodeKind.Assigment;
        public VariableSymbol Var { get; }
        public BoundExpr Expression { get; }

        public BoundAssigmentExpr(VariableSymbol var, BoundExpr expr)
        {
            Var = var;
            Expression = expr;
        }
    }

    public class BoundLiteralExpr : BoundExpr
    {
        public override Type Type => Value.GetType();
        public object Value { get; }
        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpr;

        public BoundLiteralExpr(object value)
        {
            Value = value;
        }
    }

    public enum BoundUnaryOperatorKind
    {
        Plus,
        Minus, 

        LogicalNegation // !
    }

    public class BoundUnaryExpr : BoundExpr
    {
        public BoundUnaryOperator Operator { get; }
        public BoundExpr Operand { get; }
        public override Type Type => Operator.ResultType;
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpr;

        public BoundUnaryExpr(BoundUnaryOperator _operator, BoundExpr operand) 
        {
            Operand = operand;
            Operator = _operator;
        }
    }

    public enum BoundBinaryOperatorKind
    {
        Addition,
        Substraction,
        Multiplication,
        Division,

        LogicalAnd,     // &&
        LogicalOr,      // ||
        LogicalEqual,   // ==
        LogicalNotEqual,// !=

        Less,           // <
        Greater,        // >
        LessOrEqual,    // <=
        GreaterOrEqual  // >=
    }

    class BoundBinaryExpr : BoundExpr
    {
        public BoundExpr Left { get; }
        public BoundExpr Right { get; }
        public BoundBinaryOperator Operator { get; }
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpr;
        public override Type Type => Operator.ResultType;

        public BoundBinaryExpr(BoundExpr left, BoundExpr right, BoundBinaryOperator _operator)
        {
            Left = left;
            Right = right;
            Operator = _operator;
        }
    }

    public abstract class BoundStatement : BoundNode
    {

    }

    public class BoundBlockStatement : BoundStatement
    {
        public override BoundNodeKind Kind => BoundNodeKind.BlockStatement;
        public ImmutableArray<BoundStatement> Statements { get; }

        public BoundBlockStatement(ImmutableArray<BoundStatement> statements)
        {
            Statements = statements;
        }
    }

    public class BoundExprStatement : BoundStatement
    {
        public override BoundNodeKind Kind => BoundNodeKind.ExprStatement;
        public BoundExpr Expression { get; }

        public BoundExprStatement(BoundExpr expr)
        {
            Expression = expr;
        }
    }

    public class BoundVariableDeclarationStatement : BoundStatement
    {
        public override BoundNodeKind Kind => BoundNodeKind.VariableDeclarationStatement;

        public VariableSymbol Variable { get; }
        public BoundExpr Initializer { get; }

        public BoundVariableDeclarationStatement(VariableSymbol variable, BoundExpr initializer)
        {
            Variable = variable;
            Initializer = initializer;
        }
    }

    public class BoundIfStatement : BoundStatement
    {
        public override BoundNodeKind Kind => BoundNodeKind.IfStatement;

        public BoundExpr Condition { get; }
        public BoundStatement Then { get; }
        public BoundStatement ElseStatement { get; }

        public BoundIfStatement(BoundExpr condition,  BoundStatement then, BoundStatement elseStatement)
        {
            Condition = condition;
            Then = then;
            ElseStatement = elseStatement;
        }
    }

    public class BoundWhileStatement : BoundStatement
    {
        public override BoundNodeKind Kind => BoundNodeKind.WhileStatement;

        public BoundExpr Condition { get; }
        public BoundStatement Body { get; }

        public BoundWhileStatement(BoundExpr condition, BoundStatement body)
        {
            Condition = condition;
            Body = body;
        }
    }
}
