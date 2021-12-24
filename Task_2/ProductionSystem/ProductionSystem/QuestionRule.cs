using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    internal class QuestionRule : IRule
    {
        public string RuleName { get; }
        public int Importance { get; }

        private string factName;
        private string question;

        private IRulePart rootRulePart;

        public QuestionRule(string ruleName, int importance, string factName, string question, IRulePart rootRulePart)
        {
            RuleName = ruleName;
            Importance = importance;
            this.factName = factName;
            this.question = question;
            this.rootRulePart = rootRulePart;
        }

        public FixedFact? Activate(IPrinter printer, IAsker asker)
        {
            return new FixedFact(factName, asker.Ask(question));
        }

        public bool IsActive(IFactsProvider factsProvider)
        {
            return rootRulePart.GetValue(factsProvider);
        }
    }
}
