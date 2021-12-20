namespace ProductionSystem
{
	public interface IProductionSystem
	{
		Tuple<string, IEnumerable<FixedFact>> Explain(string factName);

		event FactFixedEventHandler OnFactFixed;

		Task Run(List<string> importantFacts);
	}
}
