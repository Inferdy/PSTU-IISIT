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

        public FixedFact? Activate(IFactsProvider factsProvider, IPrinter printer, IPrinter logger, IAsker asker)
        {
            ExclusiveList<FixedFact>? list = rootRulePart.Explain(factsProvider).Item1;

            if (list == null)
                list = new ExclusiveList<FixedFact>();

            IEnumerator<FixedFact> enumerator = list.GetEnumerator();

            logger.Print("Rule \"" + RuleName + "\" (");

            if (enumerator.MoveNext())
            {
                logger.Print(enumerator.Current.Name + " = " + enumerator.Current.Value);

                while (enumerator.MoveNext())
                {
                    logger.Print(", ");
                    logger.Print(enumerator.Current.Name + " = " + enumerator.Current.Value);
                }
            }

            logger.Print(") => ");
            logger.PrintLine(factName + " = " + factValue);

            return new FixedFact(factName, factValue);
        }

        public bool IsActive(IFactsProvider factsProvider)
        {
            return rootRulePart.GetValue(factsProvider);
        }
    }
}