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

        public FixedFact? Activate(IFactsProvider factsProvider, IPrinter printer, IPrinter logger, IAsker asker)
        {
            ExclusiveList<FixedFact>? list = rootRulePart.Explain(factsProvider).Item1;

            if (list == null)
                list = new ExclusiveList<FixedFact>();

            IEnumerator<FixedFact> enumerator = list.GetEnumerator();

            logger.Print("PrintRule \"" + RuleName + "\" (");

            if (enumerator.MoveNext())
            {
                logger.Print(enumerator.Current.Name + " = " + enumerator.Current.Value);

                while (enumerator.MoveNext())
                {
                    logger.Print(", ");
                    logger.Print(enumerator.Current.Name + " = " + enumerator.Current.Value);
                }
            }

            logger.PrintLine(")");

            printer.PrintLine(text);

            return null;
        }

        public bool IsActive(IFactsProvider factsProvider)
        {
            return rootRulePart.GetValue(factsProvider);
        }
    }
}