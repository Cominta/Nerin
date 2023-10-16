using Nerin.Analyzers;
using Nerin.Analyzers.Items;
using NerinLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib.Analyzers
{
    public class SyntaxTree
    {
        public CompilationUnit Root { get; }
        public SourceText Text { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }

        public SyntaxTree(SourceText text) 
        {
            Parser parser = new Parser(text);
            CompilationUnit root = parser.Parse();
            ImmutableArray<Diagnostic> diagnostics = parser.Diagnostics.ToImmutableArray();

            Text = text;
            Root = root;
            Diagnostics = diagnostics;
        }

        public static SyntaxTree Parse(string text)
        {
            return new SyntaxTree(SourceText.From(text));
        }
    }
}
