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
            string[] examples = new string[]
            {
                "1 + 2 * 3",
                "342+4- 2",
                "4*21-4391",
                "25/312-3",
                "32 + 2-23/1",
                "DSD2-2",
                "248_0+2"
            };

            Lexer lexer = new Lexer();

            for (int i = 0; i < examples.Length; i++)
            {
                Console.WriteLine("Current example: " + examples[i]);
                lexer.SetText(examples[i]);
                SyntaxToken current = new SyntaxToken(TokensKind.Space, null, null);

                while (current.Kind != TokensKind.End && current.Kind != TokensKind.Bad)
                {
                    current = lexer.NextToken();

                    if (current.Kind != TokensKind.Space) 
                    {
                        Console.WriteLine($"{current.Kind}: text = {current.Text}, value = {current.Value}");
                    }
                }

                Console.WriteLine("---------------------");
            }
        }
    }
}
