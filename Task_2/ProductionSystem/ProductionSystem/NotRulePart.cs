namespace ProductionSystem
{
    public class NotRulePart : IRulePart
    {
        private IRulePart rulePart;

        public NotRulePart(IRulePart rulePart)
        {
            this.rulePart = rulePart;
        }

        public Tuple<ExclusiveList<FixedFact>?, bool> Explain(IFactsProvider factsProvider)
        {
            Tuple<ExclusiveList<FixedFact>?, bool> tuple = rulePart.Explain(factsProvider);

            return new Tuple<ExclusiveList<FixedFact>?, bool>(tuple.Item1, !tuple.Item2);
        }

        public bool GetValue(IFactsProvider factsProvider)
        {
            return !rulePart.GetValue(factsProvider);
        }
    }
}