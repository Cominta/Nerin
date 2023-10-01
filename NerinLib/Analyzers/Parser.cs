using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers
{
    // Makes High-level tokens (Expr)
    public class Parser
    {
        private string text;
        private int pos;
        List<SyntaxToken> tokens;

        public Parser(string text)
        {
            tokens = new List<SyntaxToken>();
            SetText(text);
        }

        public Parser()
        {
            tokens = new List<SyntaxToken>();
        }

        public void SetText(string text)
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

            return new SyntaxToken(TokensKind.Bad, null, null);
        }

        private void SyntaxParse()
        {
            if (tokens.Count != 0)
            {
                tokens.Clear();
            }

            Lexer lexer = new Lexer(text);
            SyntaxToken current = new SyntaxToken(TokensKind.Space, null, null);

            while (current.Kind != TokensKind.End && current.Kind != TokensKind.Bad)
            {
                current = lexer.NextToken();

                if (current.Kind != TokensKind.Space)
                {
                    tokens.Add(current);
                }
            }
        }

        public Expr Parse()
        {
            Expr expr = ParseExpr();
            return expr;
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

            Expr left = ParseBinary();
            return left;
        }

        public Expr ParseBinary(int parentPriority = 0)
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
            if (Current.Kind == TokensKind.LeftBracket)
            {
                SyntaxToken left = NextToken();
                Expr expr = Parse();
                SyntaxToken right = Match(TokensKind.RightBracket);

                return new BracketsExpr(left, right, expr);
            }

            else if (Current.Kind == TokensKind.TrueValue || 
                     Current.Kind == TokensKind.FalseValue)
            {
                return new LiteralExpr(NextToken());
            }

            else if (Current.Kind == TokensKind.Name)
            {
                SyntaxToken variable = NextToken();
                if (Current.Kind == TokensKind.Assigment)
                {
                    SyntaxToken assigment = NextToken();

                }
                return new NameExpr(variable);
            }

            SyntaxToken number = Match(TokensKind.Number);

            return new LiteralExpr(number);
        }
    }
}