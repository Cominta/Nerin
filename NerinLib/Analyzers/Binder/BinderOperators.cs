using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers.Binder.Items
{
    public class BoundUnaryOperator
    {
        public TokensKind Kind { get; }
        public BoundUnaryOperatorKind BoundKind { get; }
        public Type OperandType { get; }
        public Type ResultType { get; }

        private BoundUnaryOperator(TokensKind kind, BoundUnaryOperatorKind boundKind, Type operandType, Type resultType)
        {
            Kind = kind;
            BoundKind = boundKind;
            OperandType = operandType;
            ResultType = resultType;
        }

        private BoundUnaryOperator(TokensKind kind, BoundUnaryOperatorKind boundKind, Type operandType)
        {
            Kind = kind;
            BoundKind = boundKind;
            OperandType = operandType;
            ResultType = operandType;
        }

        private static BoundUnaryOperator[] _operators =
        {
            // !
            new BoundUnaryOperator(TokensKind.OppositeBool, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),

            // +
            new BoundUnaryOperator(TokensKind.Plus, BoundUnaryOperatorKind.Plus, typeof(int)),
            // -
            new BoundUnaryOperator(TokensKind.Minus, BoundUnaryOperatorKind.Minus, typeof(int)),
        };

        public static BoundUnaryOperator Bind(TokensKind kind, Type operandType)
        {
            foreach (BoundUnaryOperator oper in _operators)
            {
                if (oper.Kind == kind && oper.OperandType == operandType)
                {
                    return oper;
                }
            }

            return null;
        }
    }

    public class BoundBinaryOperator
    {
        public TokensKind Kind { get; }
        public BoundBinaryOperatorKind BoundKind { get; }
        public Type LeftType { get; }
        public Type RightType { get; }
        public Type ResultType { get; }

        private BoundBinaryOperator(TokensKind kind, BoundBinaryOperatorKind boundKind, Type type)
        {
            Kind = kind;
            BoundKind = boundKind;
            LeftType = type;
            RightType = type;
            ResultType = type;
        }

        private BoundBinaryOperator(TokensKind kind, BoundBinaryOperatorKind boundKind, Type operandType, Type resultType)
        {
            Kind = kind;
            BoundKind = boundKind;
            LeftType = operandType;
            RightType = operandType;
            ResultType = resultType;
        }

        private BoundBinaryOperator(TokensKind kind, BoundBinaryOperatorKind boundKind, Type leftType, Type rightType, Type resultType)
        {
            Kind = kind;
            BoundKind = boundKind;
            LeftType = leftType;
            RightType = rightType;
            ResultType = resultType;
        }

        private static BoundBinaryOperator[] _operators =
        {
            // +
            new BoundBinaryOperator(TokensKind.Plus, BoundBinaryOperatorKind.Addition,
                                    typeof(int)),
            // -
            new BoundBinaryOperator(TokensKind.Minus, BoundBinaryOperatorKind.Substraction,
                                    typeof(int)),
            // *
            new BoundBinaryOperator(TokensKind.Multi, BoundBinaryOperatorKind.Multiplication,
                                    typeof(int)),
            // /
            new BoundBinaryOperator(TokensKind.Divide, BoundBinaryOperatorKind.Division,
                                    typeof(int)),

            // &&
            new BoundBinaryOperator(TokensKind.And, BoundBinaryOperatorKind.LogicalAnd,
                                    typeof(bool)),
            // ||
            new BoundBinaryOperator(TokensKind.Or, BoundBinaryOperatorKind.LogicalOr,
                                    typeof(bool)),
            // == (bool)
            new BoundBinaryOperator(TokensKind.Equal, BoundBinaryOperatorKind.LogicalEqual,
                                    typeof(bool)),
            // != (bool)
            new BoundBinaryOperator(TokensKind.NotEqual, BoundBinaryOperatorKind.LogicalNotEqual,
                                    typeof(bool)),
            // == (int)
            new BoundBinaryOperator(TokensKind.Equal, BoundBinaryOperatorKind.LogicalEqual,
                                    typeof(int), typeof(bool)),
            // != (int)
            new BoundBinaryOperator(TokensKind.NotEqual, BoundBinaryOperatorKind.LogicalNotEqual,
                                    typeof(int), typeof(bool)),
        };

        public static BoundBinaryOperator Bind(TokensKind kind, Type leftType, Type rightType)
        {
            foreach (BoundBinaryOperator oper in _operators)
            {
                if (oper.Kind == kind && oper.LeftType == leftType && oper.RightType == rightType)
                {
                    return oper;
                }
            }

            return null;
        }
    }
}
