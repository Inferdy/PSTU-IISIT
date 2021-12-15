namespace ProductionSystem
{
    internal class NotRulePart : IRulePart
    {
        private IRulePart _rulePart;

        public NotRulePart(IRulePart rulePart)
        {
            _rulePart = rulePart;
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
