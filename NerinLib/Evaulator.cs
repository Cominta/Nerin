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
            switch (node.Kind)
            {
                case BoundNodeKind.LiteralExpr:
                    return EvaluateLiteral((BoundLiteralExpr)node);

                case BoundNodeKind.Variable:
                    return EvaluateVariable((BoundVariableExpr)node);

                case BoundNodeKind.Assigment:
                    return EvaluateAssigment((BoundAssigmentExpr)node);

                case BoundNodeKind.UnaryExpr:
                    return EvaluateUnaryExpr((BoundUnaryExpr)node);

                case BoundNodeKind.BinaryExpr:
                    return EvaluateBinaryExpr((BoundBinaryExpr)node);

                default:
                    throw new Exception("Failed to evaluate");
            }

            //if (node.Kind == BoundNodeKind.BracketsExpr)
            //{
            //    return Evaluate(((BracketsExpr)node).Expression);
            //}
        }

        private object EvaluateLiteral(BoundLiteralExpr node)
        {
            return node.Value;
        }

        private object EvaluateVariable(BoundVariableExpr node)
        {
            return Variables[node.Name];
        }

        private object EvaluateAssigment(BoundAssigmentExpr node)
        {
            object value = Evaluate(node.Expression);
            Variables[node.Name] = value;

            return value;
        }

        private object EvaluateUnaryExpr(BoundUnaryExpr node)
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

            throw new Exception();
        }

        private object EvaluateBinaryExpr(BoundBinaryExpr node) 
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

            throw new Exception();
        }
    }
}