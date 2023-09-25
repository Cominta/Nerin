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

            while (current.Kind != TokensKind.End)
            {
                current = lexer.NextToken();

                if (current.Kind != TokensKind.Space)
                {
                    tokens.Add(current);
                }
            }
        }

        public Expr ParseTerm()
        {
            Expr left = ParseFactor();

            while (Current.Kind == TokensKind.Plus || Current.Kind == TokensKind.Minus)
            {
                SyntaxToken _operator = NextToken();
                Expr right = ParseFactor();

                left = new BinaryExpr(left, right, _operator);
            }

            return left;
        }

        public Expr ParseFactor()
        {
            Expr left = ParsePrimary();

            while (Current.Kind == TokensKind.Multi || Current.Kind == TokensKind.Divide)
            {
                SyntaxToken _operator = NextToken();
                Expr right = ParsePrimary(); 

                left = new BinaryExpr(left, right, _operator);
            }

            return left;
        }

        private Expr ParsePrimary()
        {
            SyntaxToken number = Match(TokensKind.Number);

            return new NumberExpr(number);
        }
    }
}
