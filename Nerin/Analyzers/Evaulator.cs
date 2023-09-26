using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers
{
    public class Evaulator
    {
        private Expr root { get; }

        public Evaulator(Expr root)
        {
            this.root = root;
        }

        public int Evaluate()
        {
            return Evaluate(root);
        }

        private int Evaluate(Expr node)
        {
            if (node.Kind == TokensKind.LiteralExpr)
            {
                return (int)((LiteralExpr)node).Literal.Value;
            }

            if (node.Kind == TokensKind.UnaryExpr)
            {
                int result = Evaluate(((UnaryExpr)node).Expression);
                
                switch (((UnaryExpr)node).Unary.Kind)
                {
                    case TokensKind.Plus:
                        return result;

                    case TokensKind.Minus: 
                        return -result;
                }
            }

            if (node.Kind == TokensKind.BinaryExpr)
            {
                int left = Evaluate(((BinaryExpr)node).Left);
                int right = Evaluate(((BinaryExpr)node).Right);

                switch (((BinaryExpr)node).Operator.Kind)
                {
                    case TokensKind.Plus:
                        return left + right;

                    case TokensKind.Minus:
                        return left - right;

                    case TokensKind.Multi:
                        return left * right;

                    case TokensKind.Divide:
                        return left / right;
                }
            }

            if (node.Kind == TokensKind.BracketsExpr)
            {
                return Evaluate(((BracketsExpr)node).Expression);
            }

            throw new Exception("Failed to evaluate");
        }
    }
}