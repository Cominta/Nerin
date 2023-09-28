using Nerin.Analyzers;
using Nerin.Analyzers.Items;
using Nerin.Analyzers.Binder;
using Nerin.Analyzers.Binder.Items;
using Nerin.NerinIDE;
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
            //NideMain nide = new NideMain();
            //nide.Start();

            string currentStr = "";
            Parser parser = new Parser();
            Binder binder = new Binder();

            while (true)
            {
                Console.Write("> ");
                currentStr = Console.ReadLine();

                Expr resultParse = null;
                BoundExpr resultBound = null;

                parser.SetText(currentStr);
                resultParse = parser.Parse();
                resultBound = binder.Bind(resultParse);
                
                Evaulator evaulator = new Evaulator(resultBound);

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                bool errors = false;
                Print("  ", resultParse, ref errors);

                if (!errors)
                {
                    Console.WriteLine($"Result = {evaulator.Evaluate()}");
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