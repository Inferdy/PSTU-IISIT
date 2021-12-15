namespace ProductionSystem
{
    internal class AndRulePart : IRulePart
    {
        private IRulePart[] _ruleParts;

        public AndRulePart(IRulePart[] ruleParts)
        {
            _ruleParts = ruleParts;
        }

        public IEnumerable<FixedFact> Explain(IFactsProvider factsProvider)
        {
            throw new NotImplementedException();
        }

        public bool? GetState(IFactsProvider factsProvider)
        {
            throw new NotImplementedException();
        }
    }
}
