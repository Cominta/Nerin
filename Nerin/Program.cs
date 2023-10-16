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
using NerinLib;

namespace Nerin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NideMain nide = new NideMain();
            nide.Start();
        }
    }
}