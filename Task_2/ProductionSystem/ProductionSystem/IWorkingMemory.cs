namespace ProductionSystem
{
	public interface IWorkingMemory : IFactsProvider
	{
		void AddFact(FixedFact fact);

		void Reset();
	}
}
