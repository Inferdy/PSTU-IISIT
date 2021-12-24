namespace ProductionSystem
{
	internal interface IInferenceEngine
	{
		bool Sort(IEnumerator<ILocker<IRule>> enumerator, IFactsProvider factsProvider);

		bool IsActive();

		FixedFact? Infer(IPrinter printer, IAsker asker);
	}
}