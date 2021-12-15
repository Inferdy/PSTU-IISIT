namespace ProductionSystem
{
    internal interface IRulePart
    {
        public bool? GetState(IFactsProvider factsProvider);

        public IEnumerable<FixedFact> Explain(IFactsProvider factsProvider);
    }
}
