using Nerin.Analyzers.Items;
using NerinLib;
using NerinLib.Diagnostics;
using System;
using NerinLib.Analyzers.Statements;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers
{
    // Makes High-level tokens (Expr)
    public class Parser
    {
        private SourceText text;
        private int pos;
        List<SyntaxToken> tokens;

        private DiagnosticBag diagnostics = new DiagnosticBag();
        public DiagnosticBag Diagnostics => diagnostics;

        public Parser(SourceText text)
        {
            tokens = new List<SyntaxToken>();
            SetText(text);
        }

        public Parser()
        {
            tokens = new List<SyntaxToken>();
        }

        public void SetText(SourceText text)
        {
            this.text = text;
            this.pos = 0;

            SyntaxParse();
        }

        private SyntaxToken Peek(int offset)
        {
            int index = pos + offset;

            if (index >= text.Length)
            {
                return tokens[tokens.Count - 1];
            }

            return tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            SyntaxToken token = Current;
            pos++;
            return token;
        }

        private SyntaxToken Match(TokensKind kind)
        {
            if (kind == Current.Kind)
            {
                return NextToken();
            }

            diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);
            return new SyntaxToken(kind, null, null, pos);
        }

        private void SyntaxParse()
        {
            if (tokens.Count != 0)
            {
                tokens.Clear();
            }

            Lexer lexer = new Lexer(text);
            SyntaxToken current = new SyntaxToken(TokensKind.Space, null, null, pos);

            while (current.Kind != TokensKind.End && current.Kind != TokensKind.Bad)
            {
                current = lexer.NextToken();

                if (current.Kind != TokensKind.Space)
                {
                    tokens.Add(current);
                }
            }

            diagnostics.AddRange(lexer.Diagnostics);
        }

        public CompilationUnit Parse()
        {
            Statement statement = ParseStatement();
            SyntaxToken end = Match(TokensKind.End);
            return new CompilationUnit(statement, end);
        }

        private Statement ParseStatement()
        {

            switch (Current.Kind)
            {
                case TokensKind.OpenBrace:
                    return ParseBlockExpr();

                case TokensKind.LetKeyword:
                case TokensKind.VarKeyword:
                    return ParseVariableDeclaration();

                case TokensKind.IfKeyword:
                    return ParseIfStatement();

                default:
                    return ParseExprStatement();
            }
        }

        private Statement ParseVariableDeclaration()
        {
            TokensKind expected = Current.Kind == TokensKind.LetKeyword ? TokensKind.LetKeyword : TokensKind.VarKeyword;
            SyntaxToken keyword = Match(expected);
            SyntaxToken identifier = Match(TokensKind.Name);
            SyntaxToken equals = Match(TokensKind.Assigment);
            Expr expr = ParseExpr();

            return new VariableDeclarationStatement(keyword, identifier, equals, expr);
        }

        private Statement ParseIfStatement()
        {
            SyntaxToken keyword = Match(TokensKind.IfKeyword);
            Expr condition = ParseExpr();
            Statement statement = ParseStatement();
            ElseStatement elseStatement = ParseElseStatement();

            return new IfStatement(keyword, condition, statement, elseStatement);
        }

        private ElseStatement ParseElseStatement()
        {
            if (Current.Kind != TokensKind.ElseKeyword)
            {
                return null;
            }

            SyntaxToken keyword = Match(TokensKind.ElseKeyword);
            Statement statement = ParseStatement();

            return new ElseStatement(keyword, statement);
        }

        private Statement ParseBlockExpr()
        {
            SyntaxToken openBrace = Match(TokensKind.OpenBrace);
            ImmutableArray<Statement>.Builder statements = ImmutableArray.CreateBuilder<Statement>();

            while (Current.Kind != TokensKind.End && Current.Kind != TokensKind.CloseBrace)
            {
                Statement statement = ParseStatement();
                statements.Add(statement);
            }

            SyntaxToken closeBrace = Match(TokensKind.CloseBrace);

            return new BlockStatement(openBrace, statements.ToImmutable(), closeBrace);
        }

        private ExprStatement ParseExprStatement()
        {
            Expr expr = ParseExpr();
            return new ExprStatement(expr);
        }

        private Expr ParseExpr()
        {
            return ParseAssigment();
        }

        private Expr ParseAssigment()
        {
            if (Current.Kind == TokensKind.Name &&
                Peek(1).Kind == TokensKind.Assigment)
            {
                SyntaxToken variable = NextToken();
                SyntaxToken assigmentToken = NextToken();
                Expr assigmentExpr = ParseAssigment();

                return new AssigmentExpr(variable, assigmentToken, assigmentExpr);
            }

            return ParseBinary();
        }

        private Expr ParseBinary(int parentPriority = 0)
        {
            Expr left;
            int unaryPriority = Current.Kind.GetUnaryOperatorPriority();

            if (unaryPriority != 0 && unaryPriority >= parentPriority)
            {
                SyntaxToken _operator = NextToken();
                Expr operand = ParseBinary(unaryPriority);

                left = new UnaryExpr(operand, _operator);
            }

            else
            {
                left = ParsePrimary();
            }

            while (true)
            {
                int priority = Current.Kind.GetBinaryOperatorPriority();

                if (priority == 0 || priority <= parentPriority)
                {
                    break;
                }

                SyntaxToken _operator = NextToken();
                Expr right = ParseBinary(priority);

                left = new BinaryExpr(left, right, _operator);
            }

            return left;
        }

        private Expr ParsePrimary()
        {
            switch (Current.Kind)
            {
                case TokensKind.LeftBracket:
                    return ParseBrackets();

                case TokensKind.TrueValue:
                case TokensKind.FalseValue:
                    return ParseBoolLiteral();

                case TokensKind.Number:
                    return ParseIntLiteral();

                case TokensKind.Name:
                default:
                    return ParseName();
            }
        }

        private Expr ParseBrackets()
        {
            SyntaxToken left = Match(TokensKind.LeftBracket);
            Expr expr = ParseExpr();
            SyntaxToken right = Match(TokensKind.RightBracket);

            return new BracketsExpr(left, right, expr);
        }

        private Expr ParseBoolLiteral()
        {
            bool isTrue = Current.Kind == TokensKind.TrueValue;
            SyntaxToken token = isTrue ? Match(TokensKind.TrueValue) : Match(TokensKind.FalseValue);
            return new LiteralExpr(token);
        }

        private Expr ParseIntLiteral()
        {
            SyntaxToken number = Match(TokensKind.Number);
            return new LiteralExpr(number);
        }

        private Expr ParseName()
        {
            SyntaxToken name = Match(TokensKind.Name);
            return new NameExpr(name);
        }
    }
}