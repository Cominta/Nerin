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
        UnaryExpr
    }

    public abstract class BoundNode
    {
        public abstract BoundNodeKind Kind { get; }
    }

    public abstract class BoundExpr : BoundNode 
    {
        public abstract Type Type { get; }
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
        public override Type Type => Operand.Type;
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
        LogicalOr // ||
    }

    class BoundBinaryExpr : BoundExpr
    {
        public BoundExpr Left { get; }
        public BoundExpr Right { get; }
        public BoundBinaryOperator Operator { get; }
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpr;
        public override Type Type => Left.Type;

        public BoundBinaryExpr(BoundExpr left, BoundExpr right, BoundBinaryOperator _operator)
        {
            Left = left;
            Right = right;
            Operator = _operator;
        }
    }
}
