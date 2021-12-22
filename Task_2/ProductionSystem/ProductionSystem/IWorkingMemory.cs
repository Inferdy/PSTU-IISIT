namespace ProductionSystem
{
	public interface IWorkingMemory : IFactsProvider
	{
		bool AddFact(FixedFact fact);

		Tuple<FixedFact, Rule?>? GetFactAndRule(string factName);

		void Reset();
	}
}