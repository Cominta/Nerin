using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin.Analyzers
{
    // Make Low-level tokens (SyntaxToken)
    public class Lexer
    {
        private string text;
        private int pos;

        private int start;
        private TokensKind kind;
        private object value;
        private string symbols;

        private char Current => Peek(0);

        private char Peek(int offset)
        {
            if (pos >= text.Length)
            {
                return '\0';
            }

            return text[pos + offset];
        }

        public Lexer(string text)
        {
            SetText(text);
        }

        public Lexer()
        {

        }

        public void SetText(string text)
        {
            this.text = text;
            this.pos = 0;
        }

        private void NextPos()
        {
            pos++;
        }

        public SyntaxToken NextToken()
        {
            start = pos;
            kind = TokensKind.Bad;
            value = null;
            symbols = null;

            // Operators
            switch (Current)
            {
                case '\0':
                    kind = TokensKind.End;
                    symbols = "\0";
                    value = null;
                    break;

                case '+':
                    NextPos();
                    kind = TokensKind.Plus;
                    value = null;
                    break;

                case '-':
                    NextPos();
                    kind = TokensKind.Minus;
                    value = null;
                    break;

                case '*':
                    NextPos();
                    kind = TokensKind.Multi;
                    value = null;
                    break;

                case '/':
                    NextPos();
                    kind = TokensKind.Divide;
                    value = null;
                    break;

                case '(':
                    NextPos();
                    kind = TokensKind.LeftBracket;
                    value = null;
                    break;

                case ')':
                    NextPos();
                    kind = TokensKind.RightBracket;
                    value = null;
                    break;

                case '{':
                    NextPos();
                    kind = TokensKind.OpenBrace;
                    value = null;
                    break;

                case '}':
                    NextPos();
                    kind = TokensKind.CloseBrace;
                    value = null;
                    break;

                case '&':
                    if (Peek(1) == '&')
                    {
                        pos += 2;
                        kind = TokensKind.And;
                        value = null;
                    }
                    break;

                case '|':
                    if (Peek(1) == '|')
                    {
                        pos += 2;
                        kind = TokensKind.Or;
                        value = null;
                    }
                    break;

                case '=':
                    if (Peek(1) == '=')
                    {
                        pos += 2;
                        kind = TokensKind.Equal;
                        value = null;
                    }

                    else
                    {
                        NextPos();
                        kind = TokensKind.Assigment;
                        value = null;
                    }

                    break;

                case '!':
                    if (Peek(1) == '=')
                    {
                        pos += 2;
                        kind = TokensKind.NotEqual; 
                        value = null;
                    }

                    else
                    {
                        NextPos();
                        kind = TokensKind.OppositeBool;
                        value = null;
                    }

                    break;

                default:
                    // Number
                    if (char.IsDigit(Current))
                    {
                        LexDigit();
                    }

                    // True, False, Variables
                    else if (char.IsLetter(Current))
                    {
                        LexLetters();
                    }

                    else if (char.IsWhiteSpace(Current))
                    {
                        LexWhiteSpace();
                    }

                    else
                    {
                        kind = TokensKind.Bad;
                        value = null;
                    }

                    break;
            }

            symbols = text.Substring(start, pos - start);

            return new SyntaxToken(kind, symbols, value);
        }

        private void LexDigit()
        {
            while (char.IsDigit(Current))
            {
                NextPos();
            }

            int number = 0;
            string numberStr = text.Substring(start, pos - start);
            int.TryParse(numberStr, out number);

            value = number;
            symbols = numberStr;
            kind = TokensKind.Number;
        }

        private void LexWhiteSpace()
        {
            while (char.IsWhiteSpace(Current))
            {
                NextPos();
            }

            kind = TokensKind.Space;
            value = null;
            symbols = " ";
        }

        private void LexLetters()
        {
            while (char.IsLetter(Current) || char.IsDigit(Current))
            {
                NextPos();
            }

            string word = text.Substring(start, pos - start);
            kind = SyntaxPriority.GetKeywordKind(word);
            value = null;

            if (kind == TokensKind.TrueValue || kind == TokensKind.FalseValue)
            {
                value = bool.Parse(word);
            }

            symbols = word;
        }
    }
}