using Nerin.Analyzers.Items;
using NerinLib;
using NerinLib.Diagnostics;
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
        private SourceText text;
        private int pos;

        private int start;
        private TokensKind kind;
        private object value;
        private string symbols;

        private DiagnosticBag diagnostics = new DiagnosticBag();
        public DiagnosticBag Diagnostics => diagnostics;

        private char Current => Peek(0);

        private char Peek(int offset)
        {
            if (pos >= text.Length)
            {
                return '\0';
            }

            return text[pos + offset];
        }

        public Lexer(SourceText text)
        {
            SetText(text);
        }

        public Lexer()
        {

        }

        public void SetText(SourceText text)
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

                case '<':
                    if (Peek(1) == '=')
                    {
                        pos += 2;
                        kind = TokensKind.LessOrEqual;
                        value = null;
                    }

                    else
                    {
                        NextPos();
                        kind = TokensKind.Less;
                        value = null;
                    }

                    break;

                case '>':
                    if (Peek(1) == '=')
                    {
                        pos += 2;
                        kind = TokensKind.GreaterOrEqual;
                        value = null;
                    }

                    else
                    {
                        NextPos();
                        kind = TokensKind.Greater;
                        value = null;
                    }

                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    LexDigit();
                    break;

                case ' ':
                case '\t':
                case '\n':
                case '\r':
                    LexWhiteSpace();
                    break;

                default:
                    // True, False, Variables
                    if (char.IsLetter(Current))
                    {
                        LexLetters();
                    }

                    else
                    {
                        diagnostics.ReportBadCharacter(pos, Current);
                        NextPos();
                    }

                    break;
            }

            symbols = text.ToString(start, pos - start);

            return new SyntaxToken(kind, symbols, value, start);
        }

        private void LexDigit()
        {
            while (char.IsDigit(Current))
            {
                NextPos();
            }

            int number = 0;
            string numberStr = text.ToString(start, pos - start);

            if (!int.TryParse(numberStr, out number))
            {
                diagnostics.ReportInvalidNumber(new TextSpan(start, pos - start), numberStr, typeof(int));
            }

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

            string word = text.ToString(start, pos - start);
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