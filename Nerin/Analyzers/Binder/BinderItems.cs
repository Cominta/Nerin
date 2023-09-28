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
        Identity, // &&
        Negation // !
    }

    public class BoundUnaryExpr : BoundExpr
    {
        public BoundUnaryOperatorKind OperatorKind { get; }
        public BoundExpr Operand { get; }
        public override Type Type => Operand.Type;
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpr;

        public BoundUnaryExpr(BoundUnaryOperatorKind _operator, BoundExpr operand) 
        {
            Operand = operand;
            OperatorKind = _operator;
        }
    }

    public enum BoundBinaryOperatorKind
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }

    class BoundBinaryExpr : BoundExpr
    {
        public BoundExpr Left { get; }
        public BoundExpr Right { get; }
        public BoundBinaryOperatorKind Operator { get; }
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpr;
        public override Type Type => Left.GetType();

        public BoundBinaryExpr(BoundExpr left, BoundExpr right, BoundBinaryOperatorKind _operator)
        {
            Left = left;
            Right = right;
            Operator = _operator;
        }
    }
}
