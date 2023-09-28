using Nerin.Analyzers.Items;
using Nerin.Analyzers.Binder.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers.Binder
{
    public class Binder
    {
        public BoundExpr Bind(Expr expr)
        {
            switch (expr.Kind)
            {
                case TokensKind.LiteralExpr:
                    return BindLiteralExpr((LiteralExpr)expr);

                case TokensKind.UnaryExpr:
                    return BindUnaryExpr((UnaryExpr)expr);

                case TokensKind.BinaryExpr:
                    return BindBinaryExpr((BinaryExpr)expr);

                default:
                    throw new Exception("In Binder.Bind: Incorrect Expr");
            }
        }

        private BoundBinaryExpr BindBinaryExpr(BinaryExpr expr) 
        {
            BoundExpr left = Bind(expr.Left);
            BoundExpr right = Bind(expr.Right);
            BoundBinaryOperator _operator = BoundBinaryOperator.Bind(expr.Operator.Kind, left.Type, right.Type);

            BoundBinaryExpr boundExpr = new BoundBinaryExpr(left, right, _operator);
            return boundExpr;
        }

        private BoundUnaryExpr BindUnaryExpr(UnaryExpr expr)
        {
            BoundExpr operand = Bind(expr.Expression);
            BoundUnaryOperator _operator = BoundUnaryOperator.Bind(expr.Unary.Kind, operand.Type);

            BoundUnaryExpr boundExpr = new BoundUnaryExpr(_operator, operand);
            return boundExpr;
        }

        private BoundLiteralExpr BindLiteralExpr(LiteralExpr expr)
        {
            return new BoundLiteralExpr(expr.Literal.Value);
        }

        private BoundUnaryOperatorKind? BindUnaryOperatorKind(TokensKind kind, Type type)
        {
            if (type == typeof(int))
            {
                switch (kind)
                {
                    case TokensKind.Plus:
                        return BoundUnaryOperatorKind.Plus;

                    case TokensKind.Minus:
                        return BoundUnaryOperatorKind.Minus;

                    default:
                        throw new Exception("In Binder.BindBinaryOperatorKind: Incorrect TokensKind kind");
                }
            }

            else if (type == typeof(bool))
            {
                switch (kind)
                {
                    case TokensKind.OppositeBool:
                        return BoundUnaryOperatorKind.LogicalNegation;

                    default:
                        throw new Exception("In Binder.BindBinaryOperatorKind: Incorrect TokensKind kind");
                }
            }

            return null;
        }

        private BoundBinaryOperatorKind? BindBinaryOperatorKind(TokensKind kind, Type leftType, Type rightType)
        {
            if (leftType == typeof(int) && rightType == typeof(int))
            {
                switch (kind)
                {
                    case TokensKind.Plus:
                        return BoundBinaryOperatorKind.Addition; 
                
                    case TokensKind.Minus:
                        return BoundBinaryOperatorKind.Substraction;

                    case TokensKind.Multi:
                        return BoundBinaryOperatorKind.Multiplication;

                    case TokensKind.Divide:
                        return BoundBinaryOperatorKind.Division;

                    default:
                        throw new Exception("In Binder.BindBinaryOperatorKind: Incorrect TokensKind kind");
                }
            }

            else if (leftType == typeof(bool) && rightType == typeof(bool))
            {
                switch (kind)
                {
                    case TokensKind.And:
                        return BoundBinaryOperatorKind.LogicalAnd;

                    case TokensKind.Or:
                        return BoundBinaryOperatorKind.LogicalOr;

                    default:
                        throw new Exception("In Binder.BindBinaryOperatorKind: Incorrect TokensKind kind");
                }
            }

            return null;
        }
    }
}
