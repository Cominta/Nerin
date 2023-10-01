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
        Dictionary<string, object> Variables;

        public Evaulator(BoundExpr root, Dictionary<string, object> variables)
        {
            this.root = root;
            Variables = variables;
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

            if (node.Kind == BoundNodeKind.Variable)
            {
                object value = Variables[((BoundVariableExpr)node).Name];
                return value;
            }

            if (node.Kind == BoundNodeKind.Assigment)
            {
                object value = Evaluate(((BoundAssigmentExpr)node).Expression);
                Variables[((BoundAssigmentExpr)node).Name] = value;

                return value;
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

                    case BoundBinaryOperatorKind.LogicalEqual:
                        return Equals(left, right);

                    case BoundBinaryOperatorKind.LogicalNotEqual:
                        return !Equals(left, right);
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