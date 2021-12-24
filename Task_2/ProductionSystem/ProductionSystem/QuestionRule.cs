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

        public FixedFact? Activate(IFactsProvider factsProvider, IPrinter printer, IPrinter logger, IAsker asker)
        {
            string value = asker.Ask(question);

            ExclusiveList<FixedFact>? list = rootRulePart.Explain(factsProvider).Item1;

            if (list == null)
                list = new ExclusiveList<FixedFact>();

            IEnumerator<FixedFact> enumerator = list.GetEnumerator();

            logger.Print("QuestionRule \"" + RuleName + "\" (");

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
            logger.PrintLine(factName + " = " + value);

            return new FixedFact(factName, value);
        }

        public bool IsActive(IFactsProvider factsProvider)
        {
            return rootRulePart.GetValue(factsProvider);
        }
    }
}