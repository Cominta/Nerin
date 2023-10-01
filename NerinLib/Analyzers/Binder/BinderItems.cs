using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Assigment
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
        public string Name { get; }
        public override Type Type { get; }
        public override BoundNodeKind Kind => BoundNodeKind.Variable;

        public BoundVariableExpr(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }

    public class BoundAssigmentExpr : BoundExpr
    {
        public override Type Type => Expression.Type;
        public override BoundNodeKind Kind => BoundNodeKind.Assigment;
        public string Name { get; }
        public BoundExpr Expression { get; }

        public BoundAssigmentExpr(string name, BoundExpr expr)
        {
            Name = name;
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

        LogicalAnd, // &&
        LogicalOr, // ||
        LogicalEqual, // ==
        LogicalNotEqual, // !=
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
}
