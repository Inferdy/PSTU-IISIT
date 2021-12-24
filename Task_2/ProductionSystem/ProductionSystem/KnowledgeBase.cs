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

        public ILockEnumerator<IRule> GetEnumerator()
        {
            return new Enumerator(storage);
        }

        public void Reset()
        {
            storage.Reset();
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

        private class Enumerator : ILockEnumerator<IRule>
        {
            private int i = -1;
            private Storage storage;
            private IRule? current = null;

            public Enumerator(Storage storage)
            {
                this.storage = storage;
            }

            public IRule Current
            {
                get
                {
                    if (current == null)
                        throw new InvalidOperationException();

                    return current;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                //Empty
            }

            public ILocker<IRule> GetLocker()
            {
                return new Locker(Current, i, storage);
            }

            public bool MoveNext()
            {
                i++;

                if (i < storage.ValidLimiter)
                {
                    current = storage[i];
                    return true;
                }

                current = null;
                return false;
            }

            public void Reset()
            {
                i = -1;
            }
        }
    }
}