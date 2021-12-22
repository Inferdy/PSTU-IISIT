namespace ProductionSystem
{
	public interface IFactsProvider
	{
		FixedFact? GetFact(string name);
	}
}