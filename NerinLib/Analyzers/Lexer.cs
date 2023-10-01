using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
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
            // End
            if (pos >= text.Length)
            {
                return new SyntaxToken(TokensKind.End, "\0", null);
            }

            // Number
            else if (char.IsDigit(Current))
            {
                int startIndex = pos;

                while (char.IsDigit(Current))
                {
                    NextPos();
                }

                int number = 0;
                string numberStr = text.Substring(startIndex, pos - startIndex);
                int.TryParse(numberStr, out number);

                return new SyntaxToken(TokensKind.Number, numberStr, number);
            }

            // 
            else if (char.IsLetter(Current))
            {
                int startIndex = pos;

                while (char.IsLetter(Current))
                {
                    NextPos();
                }

                string word = text.Substring(startIndex, pos - startIndex);
                TokensKind kind = SyntaxPriority.GetKeywordKind(word);

                return new SyntaxToken(kind, word, bool.Parse(word));
            }

            // Operators
            switch (Current)
            {
                case '+':
                    NextPos();
                    return new SyntaxToken(TokensKind.Plus, "+", null);

                case '-':
                    NextPos();
                    return new SyntaxToken(TokensKind.Minus, "-", null);

                case '*':
                    NextPos();
                    return new SyntaxToken(TokensKind.Multi, "*", null);

                case '/':
                    NextPos();
                    return new SyntaxToken(TokensKind.Divide, "/", null);

                case '(':
                    NextPos();
                    return new SyntaxToken(TokensKind.LeftBracket, "(", null);

                case ')':
                    NextPos();
                    return new SyntaxToken(TokensKind.RightBracket, ")", null);

                case '&':
                    if (Peek(1) == '&')
                    {
                        pos += 2;
                        return new SyntaxToken(TokensKind.And, "&&", null);
                    }
                    break;

                case '|':
                    if (Peek(1) == '|')
                    {
                        pos += 2;
                        return new SyntaxToken(TokensKind.Or, "||", null);
                    }
                    break;

                case '=':
                    if (Peek(1) == '=')
                    {
                        pos += 2;
                        return new SyntaxToken(TokensKind.Equal, "==", null);
                    }
                    break;

                case '!':
                    if (Peek(1) == '=')
                    {
                        pos += 2;
                        return new SyntaxToken(TokensKind.NotEqual, "!=", null);
                    }

                    NextPos();
                    return new SyntaxToken(TokensKind.OppositeBool, "!", null);
            }

            if (Current == ' ')
            {
                NextPos();
                return new SyntaxToken(TokensKind.Space, " ", null);
            }

            return new SyntaxToken(TokensKind.Bad, null, null);
        }
    }
}