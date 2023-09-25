using Nerin.Analyzers;
using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentStr = "";
            Parser parser = new Parser();

            while (true)
            {
                Console.Write("> ");
                currentStr = Console.ReadLine();

                Expr result = null;
                parser.SetText(currentStr);
                result = parser.ParseTerm();
                Evaulator evaulator = new Evaulator(result);

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                Print("  ", result);
                Console.WriteLine($"Result = {evaulator.Evaluate()}");

                Console.ForegroundColor = color;
            }
        }

        static void Print(string indent, Expr node, bool isLast = true)
        {
            // ├ │ └ ─ 
            string marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken token && token.Value != null) 
            {
                Console.Write(" ");
                Console.Write(token.Value);
            }

            Console.WriteLine();
            indent += isLast ? "     " : "│    ";

            object lastChild = node.GetChild().LastOrDefault();

            foreach (Expr child in node.GetChild())
            {
                Print(indent, child, child == lastChild);
            }
        }
    }
}
