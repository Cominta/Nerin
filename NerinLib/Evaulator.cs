using Nerin.Analyzers;
using Nerin.Analyzers.Binder.Items;
using NerinLib;
using NerinLib.Analyzers.Statements;
using NerinLib.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin
{
    public class Evaulator
    {
        private BoundStatement root { get; }
        Dictionary<VariableSymbol, object> Variables;

        private object lastValue;

        public Evaulator(BoundStatement root, Dictionary<VariableSymbol, object> variables)
        {
            this.root = root;
            Variables = variables;
        }

        public object Evaluate()
        {
            EvaluateStatement(root);
            return lastValue;
        }

        private void EvaluateStatement(BoundStatement statement)
        {
            switch (statement.Kind)
            {
                case BoundNodeKind.BlockStatement:
                    EvaluateBlockStatement((BoundBlockStatement)statement);
                    break;

                case BoundNodeKind.VariableDeclarationStatement:
                    EvaluateVariableDeclarationStatement((BoundVariableDeclarationStatement)statement);
                    break;

                case BoundNodeKind.IfStatement:
                    EvaluateIfStatement((BoundIfStatement)statement);
                    break;

                case BoundNodeKind.ExprStatement:
                    EvaluateExprStatement((BoundExprStatement)statement);
                    break;

                default:
                    throw new Exception("Failed to evaluate");
            }

            //if (node.Kind == BoundNodeKind.BracketsExpr)
            //{
            //    return Evaluate(((BracketsExpr)node).Expression);
            //}
        }

        private void EvaluateBlockStatement(BoundBlockStatement block)
        {
            foreach (BoundStatement stat in block.Statements)
            {
                EvaluateStatement(stat);
            }
        }

        private void EvaluateVariableDeclarationStatement(BoundVariableDeclarationStatement statement)
        {
            object value = EvaluateExpr(statement.Initializer);
            Variables[statement.Variable] = value;
            lastValue = value;
        }

        private void EvaluateIfStatement(BoundIfStatement statement)
        {
            bool condition = (bool)EvaluateExpr(statement.Condition);

            if (condition)
            {
                EvaluateStatement(statement.Then);
            }

            else if (statement.ElseStatement != null)
            {
                EvaluateStatement(statement.ElseStatement);
            }
        }

        private void EvaluateExprStatement(BoundExprStatement statement)
        {
            lastValue = EvaluateExpr(statement.Expression);
        }

        private object EvaluateExpr(BoundExpr node)
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
            return Variables[node.VarSymbol];
        }

        private object EvaluateAssigment(BoundAssigmentExpr node)
        {
            object value = EvaluateExpr(node.Expression);
            Variables[node.Var] = value;

            return value;
        }

        private object EvaluateUnaryExpr(BoundUnaryExpr node)
        {
            object result = EvaluateExpr(((BoundUnaryExpr)node).Operand);

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
            object left = EvaluateExpr(((BoundBinaryExpr)node).Left);
            object right = EvaluateExpr(((BoundBinaryExpr)node).Right);

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

                case BoundBinaryOperatorKind.Less:
                    return (int)left < (int)right;

                case BoundBinaryOperatorKind.Greater:
                    return (int)left > (int)right;

                case BoundBinaryOperatorKind.LessOrEqual:
                    return (int)left <= (int)right;

                case BoundBinaryOperatorKind.GreaterOrEqual:
                    return (int)left >= (int)right;
            }

            throw new Exception();
        }
    }
}