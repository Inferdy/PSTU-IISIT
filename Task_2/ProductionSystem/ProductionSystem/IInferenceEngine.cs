namespace ProductionSystem
{
	public interface IInferenceEngine
	{
		void Sort(ILockEnumerator<Rule> lockEnumerator);

		FixedFact? Infer();
	}
}
