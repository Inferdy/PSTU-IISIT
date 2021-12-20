namespace ProductionSystem
{
	public interface IRulePart
	{
		bool? GetValue(IFactsProvider factsProvider);
	}
}
