namespace ProductionSystem
{
    internal class FactExistsRulePart : IRulePart
    {
        private string factName;

        public FactExistsRulePart(string factName)
        {
            this.factName = factName;
        }

        public Tuple<ExclusiveList<FixedFact>?, bool> Explain(IFactsProvider factsProvider)
        {
            FixedFact? fact = factsProvider.GetFact(factName);

            if (fact != null)
            {
                ExclusiveList<FixedFact> list = new ExclusiveList<FixedFact>();

                list.AddLast(fact);

                return new Tuple<ExclusiveList<FixedFact>?, bool>(list, true);
            }

            return new Tuple<ExclusiveList<FixedFact>?, bool>(null, false);
        }

        public bool GetValue(IFactsProvider factsProvider)
        {
            return factsProvider.GetFact(factName) != null;
        }
    }
}