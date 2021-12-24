using System.Collections;

namespace ProductionSystem
{
    internal class KnowledgeBase : IKnowledgeBase
    {
        private Storage storage;

        public KnowledgeBase(IRule[] rules)
        {
            storage = new Storage(rules);
        }

        public IEnumerator<ILocker<IRule>> GetEnumerator()
        {
            return new Enumerator(storage);
        }

        public void Reset()
        {
            storage.Reset();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Storage
        {
            private IRule[] rules;
            public int ValidLimiter { get; private set; }

            public Storage(IRule[] rules)
            {
                this.rules = rules;
                ValidLimiter = this.rules.Length;
            }

            public IRule this[int i]
            {
                get
                {
                    return rules[i];
                }
            }

            public void Reset()
            {
                ValidLimiter = rules.Length;
            }

            public void Lock(int i)
            {
                ValidLimiter--;

                IRule tmp = rules[i];
                rules[i] = rules[ValidLimiter];
                rules[ValidLimiter] = tmp;
            }
        }

        private class Locker : ILocker<IRule>
        {
            public IRule Value { get; }
            private int i;
            private Storage storage;
            private bool ready = true;

            public Locker(IRule value, int i, Storage storage)
            {
                Value = value;
                this.i = i;
                this.storage = storage;
            }

            public void Lock()
            {
                storage.Lock(i);
            }
        }

        private class Enumerator : IEnumerator<ILocker<IRule>>
        {
            private int i = -1;
            private Storage storage;
            private ILocker<IRule>? locker = null;

            public Enumerator(Storage storage)
            {
                this.storage = storage;
            }

            public ILocker<IRule> Current
            {
                get
                {
                    if (locker == null)
                        throw new InvalidOperationException();

                    return locker;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                //Empty
            }

            public bool MoveNext()
            {
                i++;

                if (i < storage.ValidLimiter)
                {
                    locker = new Locker(storage[i], i, storage);
                    return true;
                }

                locker = null;
                return false;
            }

            public void Reset()
            {
                i = -1;
            }
        }
    }
}