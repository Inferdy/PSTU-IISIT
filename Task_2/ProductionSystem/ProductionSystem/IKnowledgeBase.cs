namespace ProductionSystem
{
    internal interface IKnowledgeBase
    {
        ILockEnumerator<IRule> GetEnumerator();

        void Reset();
    }
}