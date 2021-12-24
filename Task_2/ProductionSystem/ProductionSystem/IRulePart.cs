namespace ProductionSystem
{
    public interface IRulePart
    {
        bool GetValue(IFactsProvider factsProvider);

        Tuple<ExclusiveList<FixedFact>?, bool> Explain(IFactsProvider factsProvider);
    }
}