﻿using Nerin.Analyzers.Items;
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
                    return 5;

                case TokensKind.Plus:
                case TokensKind.Minus:
                    return 4;

                case TokensKind.Equal:
                case TokensKind.NotEqual:
                    return 3;

                case TokensKind.And:
                    return 2;

                case TokensKind.Or:
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
                case TokensKind.OppositeBool:
                    return 6;

                default:
                    return 0;
            }
        }

        public static TokensKind GetKeywordKind(string word)
        {
            switch (word)
            {
                case "True":
                    return TokensKind.TrueValue;

                case "False":
                    return TokensKind.FalseValue;

                case "var":
                    return TokensKind.VarKeyword;

                case "let":
                    return TokensKind.LetKeyword;

                default:
                    return TokensKind.Name;
            }
        }
    }
}
