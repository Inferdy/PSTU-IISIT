namespace ProductionSystem
{
	internal interface IKnowledgeBase : IEnumerable<ILocker<IRule>>
	{
		void Reset();
	}
}