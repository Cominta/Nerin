using Nerin.Analyzers.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerinLib.Diagnostics
{
    public class Diagnostic
    {
        public TextSpan Span { get; }
        public string Message { get; }

        public Diagnostic(TextSpan span, string message)
        {
            Span = span;
            Message = message;
        }

        public override string ToString() => Message;
    }

    public class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();
        
        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Report(TextSpan span, string message)
        {
            _diagnostics.Add(new Diagnostic(span, message));
        }

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }

        public void ReportInvalidNumber(TextSpan span, string number, Type type)
        {
            string message = $"The number {number} isn`t valid {type}";
            Report(span, message);
        }

        public void ReportBadCharacter(int pos, char character)
        {
            string message = $"Bad Charachter input: {character}";
            TextSpan span = new TextSpan(pos, 1);
            Report(span, message);
        }

        public void ReportUnexpectedToken(TextSpan span, TokensKind kindCurrent, TokensKind kindExpected)
        {
            string message = $"Unexpected token <{kindCurrent}>, expected <{kindExpected}>";
            Report(span, message);
        }

        public  void ReportUndefinedUnaryOperator(TextSpan span, string text, Type type)
        {
            string message = $"Undefined unary operator '{text}' for type <{type}>";
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string text, Type type1, Type type2)
        {
            string message = $"Undefined binary operator '{text}' for type <{type1}> and <{type2}>";
            Report(span, message);
        }

        public void ReportUndefinedName(TextSpan span, string name)
        {
            string message = $"Variable '{name}' doesn`t exist";
            Report(span, message);
        }

        public void ReportVariableAlreadyDeclared(TextSpan span, string name)
        {
            string message = $"Variable '{name}' is already declared";
            Report(span, message);
        }

        public void ReportCannotConvert(TextSpan span, Type type1, Type type2)
        {
            string message = $"Cannot convert <{type1}> to <{type2}>";
            Report(span, message);
        }

        public void ReportCannotAssign(TextSpan span, string name)
        {
            string message = $"Variable '{name}' is read only and cannot be assigned to";
            Report(span, message);
        }
    }
}
