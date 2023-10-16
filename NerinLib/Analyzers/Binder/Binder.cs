using Nerin.Analyzers.Items;
using Nerin.Analyzers.Binder.Items;
using System;
using System.Collections.Generic;
using NerinLib.Analyzers.Statements;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using NerinLib.Analyzers.Binder;
using NerinLib;
using NerinLib.Symbols;
using System.Runtime.InteropServices.WindowsRuntime;
using NerinLib.Diagnostics;

namespace Nerin.Analyzers.Binder
{
    public class Binder
    {
        private BoundScope Scope;

        private DiagnosticBag diagnostics = new DiagnosticBag();
        public DiagnosticBag Diagnostics => diagnostics;

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
            ImmutableArray<Diagnostic> _diagnostics = binder.Diagnostics.ToImmutableArray();

            if (previous != null)
            {
                _diagnostics = _diagnostics.InsertRange(0, previous.Diagnostics);
            }

            return new BoundGlobalScope(previous, _diagnostics, variables, statement);
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

                case TokensKind.VariableDeclarationStatement:
                    return BindVariableDeclarationStatement((VariableDeclarationStatement)statement);

                case TokensKind.IfStatement:
                    return BindIfStatement((IfStatement)statement);

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

        private BoundVariableDeclarationStatement BindVariableDeclarationStatement(VariableDeclarationStatement statement)
        {
            string name = statement.Identifier.Text;
            bool isReadOnly = statement.Keyword.Kind == TokensKind.LetKeyword;
            BoundExpr initializer = BindExpr(statement.Initializer);
            VariableSymbol symbol = new VariableSymbol(name, isReadOnly, initializer.Type);

            if (!Scope.TryDeclare(symbol))
            {
                diagnostics.ReportVariableAlreadyDeclared(statement.Identifier.Span, name);
            }

            return new BoundVariableDeclarationStatement(symbol, initializer);
        }

        private BoundStatement BindIfStatement(IfStatement statement)
        {
            BoundExpr condition = BindExpr(statement.Condition, typeof(bool));
            BoundStatement ifStatement = BindStatement(statement.Then);
            BoundStatement elseStatement = statement.Else == null ? null : BindStatement(statement.Else.Then);

            return new BoundIfStatement(condition, ifStatement, elseStatement);
        }

        private BoundBlockStatement BindBlockStatement(BlockStatement statement)
        {
            ImmutableArray<BoundStatement>.Builder statements = ImmutableArray.CreateBuilder<BoundStatement>();
            Scope = new BoundScope(Scope);

            foreach (Statement statementSyntax in statement.Statements)
            {
                BoundStatement stat = BindStatement(statementSyntax);
                statements.Add(stat);
            }

            Scope = Scope.Parent;

            return new BoundBlockStatement(statements.ToImmutable());
        }

        private BoundExpr BindExpr(Expr expr, Type targetType)
        {
            BoundExpr result = BindExpr(expr);

            if (result.Type != targetType)
            {
                diagnostics.ReportCannotConvert(expr.Span, result.Type, targetType);
            }

            return result;
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
                diagnostics.ReportUndefinedName(expr.Token.Span, name);
                return new BoundLiteralExpr(0);
            }

            return new BoundVariableExpr(var);
        }

        private BoundExpr BindAssigment(AssigmentExpr expr)
        {
            BoundExpr exprToAssigment = BindExpr(expr.Expression);
            string name = expr.Token.Text;

            if (!Scope.TryLookup(name, out VariableSymbol var))
            {
                diagnostics.ReportUndefinedName(expr.Token.Span, name);
                return exprToAssigment;
            }

            if (var.isReadOnly)
            {
                diagnostics.ReportCannotAssign(expr.EqualsToken.Span, name);
            }

            if (exprToAssigment.Type != var.Type)
            {
                diagnostics.ReportCannotConvert(expr.Token.Span, exprToAssigment.Type, var.Type);
                return exprToAssigment;
            }

            return new BoundAssigmentExpr(var, exprToAssigment);
        }

        private BoundExpr BindBinaryExpr(BinaryExpr expr) 
        {
            BoundExpr left = BindExpr(expr.Left);
            BoundExpr right = BindExpr(expr.Right);
            BoundBinaryOperator _operator = BoundBinaryOperator.Bind(expr.Operator.Kind, left.Type, right.Type);

            if (_operator == null)
            {
                diagnostics.ReportUndefinedBinaryOperator(expr.Operator.Span, expr.Operator.Text, left.Type, right.Type);
                return left;
            }

            BoundBinaryExpr boundExpr = new BoundBinaryExpr(left, right, _operator);
            return boundExpr;
        }

        private BoundExpr BindUnaryExpr(UnaryExpr expr)
        {
            BoundExpr operand = BindExpr(expr.Expression);
            BoundUnaryOperator _operator = BoundUnaryOperator.Bind(expr.Unary.Kind, operand.Type);

            if (_operator == null)
            {
                diagnostics.ReportUndefinedUnaryOperator(expr.Unary.Span, expr.Unary.Text, operand.Type);
                return operand;
            }

            BoundUnaryExpr boundExpr = new BoundUnaryExpr(_operator, operand);
            return boundExpr;
        }

        private BoundLiteralExpr BindLiteralExpr(LiteralExpr expr)
        {
            return new BoundLiteralExpr(expr.Literal.Value);
        }
    }
}
