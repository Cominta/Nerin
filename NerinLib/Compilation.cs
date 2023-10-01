using Nerin;
using Nerin.Analyzers;
using Nerin.Analyzers.Binder;
using Nerin.Analyzers.Binder.Items;
using Nerin.Analyzers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib
{
    public class Compilation
    {
        public Expr Token { get; }
        public string Text { get; }

        public Compilation(string text) 
        {
            Text = text;

            Parser parser = new Parser(text);
            Expr parserResult = parser.Parse();
            Token = parserResult;
        }

        public EvaluationResult EvaluateResult(Dictionary<string, object> variables)
        {
            Binder binder = new Binder(variables);
            BoundExpr expr = binder.Bind(Token);

            Evaulator evaulator = new Evaulator(expr, variables);
            return new EvaluationResult(evaulator.Evaluate());
        }
    }

    // In future will contain errors and some information
    public class EvaluationResult
    {
        public object Value { get; }

        public EvaluationResult(object value) 
        {
            Value = value;
        }
    }
}
