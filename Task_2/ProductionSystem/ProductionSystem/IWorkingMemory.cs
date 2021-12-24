namespace ProductionSystem
{
    public interface IWorkingMemory : IFactsProvider
    {
        void SetFact(FixedFact fact);

        void Reset();
    }
}