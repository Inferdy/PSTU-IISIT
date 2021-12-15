namespace ProductionSystem
{
    internal interface IFactsProvider
    {
        public FixedFact? GetFact(string name);
    }
}
