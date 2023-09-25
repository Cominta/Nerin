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
            if (node.Kind == TokensKind.NumberExpr)
            {
                return (int)((NumberExpr)node).Number.Value;
            }

            if (node.Kind == TokensKind.BinaryExpr)
            {
                int left = Evaluate(((BinaryExpr)node).Left);
                int right = Evaluate(((BinaryExpr)node).Right);

                if (((BinaryExpr)node).Operator.Kind == TokensKind.Minus)
                {
                    return left - right;
                }

                if (((BinaryExpr)node).Operator.Kind == TokensKind.Plus)
                {
                    return left + right;
                }

                if (((BinaryExpr)node).Operator.Kind == TokensKind.Multi)
                {
                    return left * right;
                }

                if (((BinaryExpr)node).Operator.Kind == TokensKind.Divide)
                {
                    return left / right;
                }
            }

            throw new Exception("Failed to evaluate");
        }   
    }
}
