namespace ProductionSystem
{
	public interface IFactsReceiver : IFactsProvider
	{
		FixedFact? GetNewFact(IFactsProvider currentBase);
	}
}
