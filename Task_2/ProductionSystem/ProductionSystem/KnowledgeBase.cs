using System.Collections;

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
				this.rules = rules;
                validLimiter = rules.Length;
			}
		}

		public class Enumerator : ILockEnumerator<Rule>
		{
			private Storage storage;
            private int i = -1;

			public Enumerator(Storage storage)
			{
				this.storage = storage;
			}

            public Rule Current
            {
                get
                {
                    try
                    {
                        return storage.rules[i];
                    }
                    catch
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                //Empty
            }

            public void LockCurrent()
            {
                storage.validLimiter--;

                Rule[] rules = storage.rules;
                int limiter = storage.validLimiter;

                Rule tmp = rules[i];
                rules[i] = rules[limiter];
                rules[limiter] = tmp;

                i--;
            }

            public bool MoveNext()
            {
                i++;

                return i < storage.validLimiter;
            }

            public void Reset()
            {
                i = -1;
            }
        }

        private Storage storage;

		public KnowledgeBase(Rule[] rules)
		{
            storage = new Storage(rules);
		}

        public ILockEnumerator<Rule> GetUnlockedEnumerator()
        {
            return new Enumerator(storage);
        }

        public void Reset()
        {
			storage.validLimiter = storage.rules.Length;
        }
    }
}