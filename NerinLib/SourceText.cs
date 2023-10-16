using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib
{
    public struct TextSpan
    {
        public int Start { get; }
        public int Len { get; }
        public int End => Start + Len;

        public TextSpan(int start, int len)
        {
            Start = start;
            Len = len;
        }

        public static TextSpan FromBounds(int start, int end)
        {
            int length = end - start;
            return new TextSpan(start, length);
        }
    }

    public class SourceText
    {
        public ImmutableArray<TextLine> Lines;
        private string Text;
        public char this[int index] => Text[index];
        public int Length => Text.Length;

        private SourceText(string text) 
        {
            Lines = ParseLines(this, text);
            Text = text;
        }

        public int GetLineIndex(int pos)
        {
            int upper = Lines.Length - 1;
            int lower = 0;

            while (lower <= upper)
            {
                int index = lower + (upper - lower) / 2;
                int start = Lines[index].Start;

                if (start == pos)
                {
                    return index;
                }

                if (start > pos)
                {
                    upper = index - 1;
                }

                else
                {
                    lower = index + 1;
                }
            }

            return lower - 1;
        }

        private ImmutableArray<TextLine> ParseLines(SourceText source, string text)
        {
            ImmutableArray<TextLine>.Builder result = ImmutableArray.CreateBuilder<TextLine>();

            int pos = 0;
            int lineStart = 0;

            while (pos < text.Length)
            {
                int lineBreakWidth = GetLineBreakWidth(text, pos);

                if (lineBreakWidth == 0)
                {
                    pos++;
                }

                else
                {
                    AddLine(result, source, pos, lineStart, lineBreakWidth);

                    pos += lineBreakWidth;
                    lineStart = pos;
                }
            }

            if (pos >= lineStart)
            {
                AddLine(result, source, pos, lineStart, 0);
            }

            return result.ToImmutable();
        }

        private static void AddLine(ImmutableArray<TextLine>.Builder builder, SourceText source, int pos, int lineStart, int lineBreakWidth)
        {
            int len = pos - lineStart;
            int lenIncludeLineBreak = len + lineBreakWidth;
            TextLine line = new TextLine(source, lineStart, len, lenIncludeLineBreak);

            builder.Add(line);
        }

        private int GetLineBreakWidth(string text, int i)
        {
            char c = text[i];
            char l = i + 1 >= text.Length ? '\0' : text[i + 1];

            if (c == '\r' && l == '\n')
            {
                return 2;
            }

            if (c == '\r' || c == '\n')
            {
                return 1;
            }

            return 0;
        }

        public static SourceText From(string text)
        {
            return new SourceText(text);
        }

        public override string ToString() => Text;
        public string ToString(int start, int len) => Text.Substring(start, len);
        public string ToString(TextSpan span) => ToString(span.Start, span.Len);
    }

    public class TextLine
    {
        public SourceText Text { get; }
        public int Start { get; }
        public int Len { get; }
        public int End => Start + Len;
        public int LenIncludeLineBreak { get; }
        public TextSpan Span => new TextSpan(Start, Len);
        public TextSpan SpanIncludeLineBreak => new TextSpan(Start, LenIncludeLineBreak);

        public TextLine(SourceText text, int start, int len, int lenIncludeLineBreak)
        {
            Text = text;
            Start = start;
            Len = len;
            LenIncludeLineBreak = lenIncludeLineBreak;
        }

        public override string ToString() => Text.ToString(Span);
    }
}
