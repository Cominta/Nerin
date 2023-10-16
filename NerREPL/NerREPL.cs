using Nerin.Analyzers;
using Nerin.Analyzers.Items;
using NerinLib;
using NerinLib.Analyzers;
using NerinLib.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerREPL
{
    public class NerREPL
    {
        public static void Main(string[] args)
        {
            string currentStr = "";
            Parser parser = new Parser();
            Dictionary<VariableSymbol, object> variables = new Dictionary<VariableSymbol, object>();
            Compilation previous = null;
            ConsoleColor color = Console.ForegroundColor;

            StringBuilder builder = new StringBuilder();

            while (true)
            {
                if (builder.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("> ");
                    Console.ForegroundColor = color;
                }

                else
                {
                    Console.Write("| ");
                }

                currentStr = Console.ReadLine();

                if (builder.Length == 0 && string.IsNullOrWhiteSpace(currentStr))
                {
                    break; 
                }

                builder.AppendLine(currentStr);
                string text = builder.ToString();

                SyntaxTree tree = SyntaxTree.Parse(text);

                if (!string.IsNullOrWhiteSpace(currentStr) && tree.Diagnostics.Any())
                {
                    continue;
                }

                Compilation compilation = previous == null ? new Compilation(tree) : previous.ContinueWith(tree);
                EvaluationResult resultBound = compilation.EvaluateResult(variables);

                if (resultBound.Diagnostics.Any())
                {
                    SourceText textS = tree.Text;

                    foreach (var diagnostic in resultBound.Diagnostics)
                    {
                        int lineIndex = textS.GetLineIndex(diagnostic.Span.Start);
                        TextLine line = tree.Text.Lines[lineIndex];
                        int lineNumber = lineIndex + 1;
                        int character = diagnostic.Span.Start - line.Start + 1;

                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"({lineNumber}, {character}): ");
                        Console.WriteLine(diagnostic.ToString());
                        Console.ForegroundColor = color;

                        TextSpan prefixSpan = TextSpan.FromBounds(line.Start, diagnostic.Span.Start);
                        TextSpan suffixSpan = TextSpan.FromBounds(diagnostic.Span.End, line.End);

                        string prefix = tree.Text.ToString(prefixSpan);
                        string error = tree.Text.ToString(diagnostic.Span);
                        string suffix = tree.Text.ToString(suffixSpan);

                        Console.Write("    ");
                        Console.Write(prefix);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(error);
                        Console.ForegroundColor = color;

                        Console.Write(suffix);

                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"Result = {resultBound.Value}\n");
                    Console.ForegroundColor = color;
                    previous = compilation;
                }

                builder.Clear();
            }
        }

        static void Print(string indent, Expr node, ref bool errors, bool isLast = true)
        {
            // ├ │ └ ─ 
            string marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);

            if (node is SyntaxToken token && token.Kind == TokensKind.Bad)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                errors = true;
            }

            Console.Write(node.Kind);
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            if (node is SyntaxToken _token && _token.Value != null)
            {
                Console.Write(" ");
                Console.Write(_token.Value);
            }

            Console.WriteLine();
            indent += isLast ? "    " : "│   ";

            object lastChild = node.GetChild().LastOrDefault();

            foreach (Expr child in node.GetChild())
            {
                Print(indent, child, ref errors, child == lastChild);
            }
        }
    }
}
