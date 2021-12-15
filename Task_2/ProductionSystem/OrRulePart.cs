namespace ProductionSystem
{
    internal class OrRulePart : IRulePart
    {
        private IRulePart[] _ruleParts;

        public OrRulePart(IRulePart[] ruleParts)
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
