﻿using Nerin.Analyzers;
using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib.Analyzers
{
    public class SyntaxTree
    {
        public Expr Root { get; }

        public SyntaxTree(Expr root) 
        {
            Root = root;
        }

        public static SyntaxTree Parse(string text)
        {
            Parser parser = new Parser(text);
            return new SyntaxTree(parser.Parse());
        }
    }
}