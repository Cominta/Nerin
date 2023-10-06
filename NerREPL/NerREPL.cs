using Nerin.Analyzers;
using Nerin.Analyzers.Items;
using NerinLib;
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
            Dictionary<string, object> variables = new Dictionary<string, object>();

            while (true)
            {
                Console.Write("> ");
                currentStr = Console.ReadLine();

                Compilation compilation = new Compilation(currentStr);
                EvaluationResult resultBound = compilation.EvaluateResult(variables);

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                bool errors = false;
                Print("  ", compilation.Token, ref errors);

                if (!errors)
                {
                    Console.WriteLine($"Result = {resultBound.Value}");
                }

                Console.ForegroundColor = color;
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
