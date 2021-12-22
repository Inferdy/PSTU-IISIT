namespace ProductionSystem
{
	public interface IInferenceEngine
	{
		void Sort(ILockEnumerator<Rule> lockEnumerator, IFactsProvider factsProvider);

		FixedFact? Infer();
	}
}