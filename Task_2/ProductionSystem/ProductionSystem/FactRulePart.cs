namespace ProductionSystem
{
    public class FactRulePart : IRulePart
    {
        private string name;
        private string value;

        public FactRulePart(string factName, string factValue)
        {
            name = factName;
            value = factValue;
        }

        public Tuple<ExclusiveList<FixedFact>?, bool> Explain(IFactsProvider factsProvider)
        {
            FixedFact? fact = factsProvider.GetFact(name);

            if (fact == null)
                return new Tuple<ExclusiveList<FixedFact>?, bool>(null, false);

            ExclusiveList<FixedFact> list = new ExclusiveList<FixedFact>();

            list.AddLast(fact);

            return new Tuple<ExclusiveList<FixedFact>?, bool>(list, fact.Value == value);
        }

        public bool GetValue(IFactsProvider factsProvider)
        {
            FixedFact? fact = factsProvider.GetFact(name);

            if (fact == null)
                return false;

            return fact.Value == value;
        }
    }
}