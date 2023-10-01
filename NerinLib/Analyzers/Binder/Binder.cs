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
        private Dictionary<string, object> Variables;

        public Binder(Dictionary<string, object> variables) 
        {
            Variables = variables;
        }

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

                case TokensKind.BracketsExpr:
                    return BindBrackets((BracketsExpr)expr);

                case TokensKind.Name:
                    return BindName((NameExpr)expr);

                case TokensKind.Assigment:
                    return BindAssigment((AssigmentExpr)expr);

                default:
                    throw new Exception("In Binder.Bind: Incorrect Expr");
            }
        }

        private BoundExpr BindBrackets(BracketsExpr expr)
        {
            return Bind(expr.Expression);
        }

        private BoundExpr BindName(NameExpr expr)
        {
            return new BoundVariableExpr(expr.Token.Text, Variables[expr.Token.Text].GetType());
        }

        private BoundExpr BindAssigment(AssigmentExpr expr)
        {
            BoundExpr exprToAssigment = Bind(expr.Expression);
            string name = expr.Token.Text;

            return new BoundAssigmentExpr(name, exprToAssigment);
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
    }
}
