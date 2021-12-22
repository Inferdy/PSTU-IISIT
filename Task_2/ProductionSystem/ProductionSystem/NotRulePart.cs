namespace ProductionSystem
{
    public class NotRulePart : IRulePart
    {
        private IRulePart rulePart;

        public NotRulePart(IRulePart rulePart)
        {
            this.rulePart = rulePart;
        }

        public Tuple<ExclusiveList<FixedFact>?, bool?> Explain(IFactsProvider factsProvider)
        {
            Tuple<ExclusiveList<FixedFact>?, bool?> tuple = rulePart.Explain(factsProvider);

            if (tuple.Item2 == null)
                return new Tuple<ExclusiveList<FixedFact>?, bool?>(null, null);

            return new Tuple<ExclusiveList<FixedFact>?, bool?>(tuple.Item1, !tuple.Item2);
        }

        public bool? GetValue(IFactsProvider factsProvider)
        {
            bool? value = rulePart.GetValue(factsProvider);

            if (value == null)
                return null;

            return !value;
        }
    }
}