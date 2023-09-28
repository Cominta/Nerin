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

        public int Evaluate()
        {
            return Evaluate(root);
        }

        private int Evaluate(BoundExpr node)
        {
            if (node.Kind == BoundNodeKind.LiteralExpr)
            {
                return (int)((BoundLiteralExpr)node).Value;
            }

            if (node.Kind == BoundNodeKind.UnaryExpr)
            {
                int result = Evaluate(((BoundUnaryExpr)node).Operand);
                
                switch (((BoundUnaryExpr)node).OperatorKind)
                {
                    case BoundUnaryOperatorKind.Identity:
                        return result;

                    case BoundUnaryOperatorKind.Negation: 
                        return -result;
                }
            }

            if (node.Kind == BoundNodeKind.BinaryExpr)
            {
                int left = Evaluate(((BoundBinaryExpr)node).Left);
                int right = Evaluate(((BoundBinaryExpr)node).Right);

                switch (((BoundBinaryExpr)node).Operator)
                {
                    case BoundBinaryOperatorKind.Addition:
                        return left + right;

                    case BoundBinaryOperatorKind.Substraction:
                        return left - right;

                    case BoundBinaryOperatorKind.Multiplication:
                        return left * right;

                    case BoundBinaryOperatorKind.Division:
                        return left / right;
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