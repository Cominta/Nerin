using Nerin.Analyzers;
using Nerin.Analyzers.Binder.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin
{
    public class Evaulator
    {
        private BoundExpr root { get; }

        public Evaulator(BoundExpr root)
        {
            this.root = root;
        }

        public object Evaluate()
        {
            return Evaluate(root);
        }

        private object Evaluate(BoundExpr node)
        {
            if (node.Kind == BoundNodeKind.LiteralExpr)
            {
                return ((BoundLiteralExpr)node).Value;
            }

            if (node.Kind == BoundNodeKind.UnaryExpr)
            {
                object result = Evaluate(((BoundUnaryExpr)node).Operand);
                
                switch (((BoundUnaryExpr)node).Operator.BoundKind)
                {
                    case BoundUnaryOperatorKind.Plus:
                        return (int)result;

                    case BoundUnaryOperatorKind.Minus: 
                        return -(int)result;

                    case BoundUnaryOperatorKind.LogicalNegation: 
                        return !(bool)(result);
                }
            }

            if (node.Kind == BoundNodeKind.BinaryExpr)
            {
                object left = Evaluate(((BoundBinaryExpr)node).Left);
                object right = Evaluate(((BoundBinaryExpr)node).Right);

                switch (((BoundBinaryExpr)node).Operator.BoundKind)
                {
                    case BoundBinaryOperatorKind.Addition:
                        return (int)left + (int)right;

                    case BoundBinaryOperatorKind.Substraction:
                        return (int)left - (int)right;

                    case BoundBinaryOperatorKind.Multiplication:
                        return (int)left * (int)right;

                    case BoundBinaryOperatorKind.Division:
                        return (int)left / (int)right;

                    case BoundBinaryOperatorKind.LogicalAnd:
                        return (bool)left && (bool)right;

                    case BoundBinaryOperatorKind.LogicalOr:
                        return (bool)left || (bool)right;
                }
            }

            //if (node.Kind == BoundNodeKind.BracketsExpr)
            //{
            //    return Evaluate(((BracketsExpr)node).Expression);
            //}

            throw new Exception("Failed to evaluate");
        }
    }
}