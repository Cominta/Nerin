using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers
{
    public static class SyntaxPriority
    {
        static public int GetBinaryOperatorPriority(this TokensKind kind)
        {
            switch (kind)
            {
                case TokensKind.Multi:
                case TokensKind.Divide:
                    return 2;

                case TokensKind.Plus:
                case TokensKind.Minus:
                    return 1;

                default:
                    return 0;
            }
        }

        static public int GetUnaryOperatorPriority(this TokensKind kind)
        {
            switch (kind)
            {
                case TokensKind.Plus:
                case TokensKind.Minus:
                    return 3;

                default:
                    return 0;
            }
        }
    }
}
