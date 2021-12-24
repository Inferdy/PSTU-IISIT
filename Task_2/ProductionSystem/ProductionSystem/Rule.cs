namespace ProductionSystem
{
    public class Rule : IRule
    {
        public string RuleName { get; }
        public int Importance { get; }

        private string factName;
        private string factValue;

        private IRulePart rootRulePart;

        public Rule(string ruleName, int importance, string factName, string factValue, IRulePart rootRulePart)
        {
            RuleName = ruleName;
            Importance = importance;
            this.factName = factName;
            this.factValue = factValue;
            this.rootRulePart = rootRulePart;
        }

        public FixedFact? Activate(IPrinter printer, IAsker asker)
        {
            //TODO print

            return new FixedFact(factName, factValue);
        }

        public bool IsActive(IFactsProvider factsProvider)
        {
            return rootRulePart.GetValue(factsProvider);
        }
    }
}