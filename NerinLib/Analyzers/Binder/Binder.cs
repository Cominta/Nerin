using Nerin.Analyzers.Items;
using Nerin.Analyzers.Binder.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using NerinLib.Analyzers.Binder;
using NerinLib;
using NerinLib.Symbols;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Nerin.Analyzers.Binder
{
    public class Binder
    {
        private BoundScope Scope;

        public Binder(BoundScope parent) 
        {
            Scope = new BoundScope(parent);
        }

        public static BoundGlobalScope BindGlobal(BoundGlobalScope previous, CompilationUnit compilationUnit)
        {
            BoundScope parentScope = CreateParentScopes(previous);
            Binder binder = new Binder(parentScope);
            BoundStatement statement = binder.BindStatement(compilationUnit.Statement);
            ImmutableArray<VariableSymbol> variables = binder.Scope.GetVariables();

            return new BoundGlobalScope(previous, variables, statement);
        }

        private static BoundScope CreateParentScopes(BoundGlobalScope previous)
        {
            Stack<BoundGlobalScope> stack = new Stack<BoundGlobalScope>();

            while (previous != null)
            {
                stack.Push(previous);
                previous = previous.Previous;
            }

            BoundScope parent = null;

            while (stack.Count > 0)
            {
                previous = stack.Pop();
                BoundScope scope = new BoundScope(parent);

                foreach (VariableSymbol v in previous.Variables)
                {
                    scope.TryDeclare(v);
                }

                parent = scope;
            }

            return parent;
        }

        private BoundStatement BindStatement(Statement statement)
        {
            switch (statement.Kind)
            {
                case TokensKind.BlockStatement:
                    return BindBlockStatement((BlockStatement)statement);

                case TokensKind.ExpressionStatement:
                    return BindExpressionStatement((ExprStatement)statement);

                default:
                    throw new Exception("In Binder.Bind: Incorrect Statement");
            }
        }

        private BoundExprStatement BindExpressionStatement(ExprStatement statement)
        {
            BoundExpr expr = BindExpr(statement.Expression);
            return new BoundExprStatement(expr);
        }

        private BoundBlockStatement BindBlockStatement(BlockStatement statement)
        {
            ImmutableArray<BoundStatement>.Builder statements = ImmutableArray.CreateBuilder<BoundStatement>();

            foreach (Statement statementSyntax in statement.Statements)
            {
                BoundStatement stat = BindStatement(statementSyntax);
                statements.Add(stat);
            }

            return new BoundBlockStatement(statements.ToImmutable());
        }

        private BoundExpr BindExpr(Expr expr)
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
            return BindExpr(expr.Expression);
        }

        private BoundExpr BindName(NameExpr expr)
        {
            string name = expr.Token.Text;
            VariableSymbol var = null;
            
            if (!Scope.TryLookup(name, out var))
            {
                throw new Exception("Incorrect var");
            }

            return new BoundVariableExpr(var);
        }

        private BoundExpr BindAssigment(AssigmentExpr expr)
        {
            BoundExpr exprToAssigment = BindExpr(expr.Expression);
            string name = expr.Token.Text;
            VariableSymbol var = new VariableSymbol(name, exprToAssigment.Type);

            if (!Scope.TryLookup(name, out var))
            {
                var = new VariableSymbol(name, exprToAssigment.Type);
                Scope.TryDeclare(var);
            }

            if (exprToAssigment.Type != var.Type)
            {
                throw new Exception("Cannot convert"); 
            }

            return new BoundAssigmentExpr(var, exprToAssigment);
        }

        private BoundBinaryExpr BindBinaryExpr(BinaryExpr expr) 
        {
            BoundExpr left = BindExpr(expr.Left);
            BoundExpr right = BindExpr(expr.Right);
            BoundBinaryOperator _operator = BoundBinaryOperator.Bind(expr.Operator.Kind, left.Type, right.Type);

            BoundBinaryExpr boundExpr = new BoundBinaryExpr(left, right, _operator);
            return boundExpr;
        }

        private BoundUnaryExpr BindUnaryExpr(UnaryExpr expr)
        {
            BoundExpr operand = BindExpr(expr.Expression);
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
