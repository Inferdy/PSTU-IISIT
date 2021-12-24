using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    internal class PrintRule : IRule
    {
        public string RuleName { get; }
        public int Importance { get; }

        private string text;

        private IRulePart rootRulePart;

        public PrintRule(string ruleName, int importance, string text, IRulePart rootRulePart)
        {
            RuleName = ruleName;
            Importance = importance;
            this.text = text;
            this.rootRulePart = rootRulePart;
        }

        public FixedFact? Activate(IPrinter printer, IAsker asker)
        {
            printer.Print(text);

            return null;
        }

        public bool IsActive(IFactsProvider factsProvider)
        {
            return rootRulePart.GetValue(factsProvider);
        }
    }
}
