namespace ProductionSystem
{
    internal interface IInferenceEngine
    {
        bool Sort(ILockEnumerator<IRule> enumerator, IFactsProvider factsProvider);

        bool IsActive();

        FixedFact? Infer(IFactsProvider factsProvider, IPrinter printer, IPrinter logger, IAsker asker);
    }
}