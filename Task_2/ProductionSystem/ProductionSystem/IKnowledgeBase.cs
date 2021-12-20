namespace ProductionSystem
{
	public interface IKnowledgeBase
	{
		ILockEnumerator<Rule> GetUnlockedEnumerator();

		void Reset();
	}
}
