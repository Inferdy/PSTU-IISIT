namespace ProductionSystem
{
    internal interface IRule
    {
        string RuleName { get; }

        int Importance { get; }

        bool IsActive(IFactsProvider factsProvider);

        FixedFact? Activate(IFactsProvider factsProvider, IPrinter printer, IPrinter logger, IAsker asker);
    }
}