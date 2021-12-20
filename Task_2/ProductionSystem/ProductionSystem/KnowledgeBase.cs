namespace ProductionSystem
{
	public class KnowledgeBase : IKnowledgeBase
	{
		public class Storage
		{
			public Rule[] rules;
			public int validLimiter;

			public Storage(Rule[] rules)
			{
				throw new System.NotImplementedException();
			}
		}

		public class Enumerator : ILockEnumerator<T>
		{
			private Storage storage;

			public Enumerator(Storage storage)
			{
				throw new System.NotImplementedException();
			}
		}

		private Storage storage;

		public KnowledgeBase(Rule[] rules)
		{
			throw new System.NotImplementedException();
		}
	}
}
