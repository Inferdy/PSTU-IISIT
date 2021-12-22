namespace ProductionSystem
{
	public interface IProductionSystem
	{
		Tuple<string, IEnumerable<FixedFact>?>? Explain(string factName);

		event FactFixedEventHandler OnFactFixed;

		void Run(List<string> importantFacts);
	}
}