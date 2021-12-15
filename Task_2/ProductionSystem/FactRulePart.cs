namespace ProductionSystem
{
    internal class FactRulePart : IRulePart
    {
        public readonly string Name;
        public readonly string Value;

        public FactRulePart(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public IEnumerable<FixedFact> Explain(IFactsProvider factsProvider)
        {
            return new FixedFact[] { factsProvider.GetFact(Name) };
        }

        public bool? GetState(IFactsProvider factsProvider)
        {
            FixedFact? fact = factsProvider.GetFact(Name);

            if (fact == null)
                return null;

            return (fact.Value == Value);
        }
    }
}
