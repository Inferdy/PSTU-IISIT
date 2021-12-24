using System.Collections;

namespace ProductionSystem
{
    public class WorkingMemory : IWorkingMemory
    {
        private Storage storage = new Storage();

        public FixedFact? GetFact(string name)
        {
            return storage.Get(name);
        }

        public void Reset()
        {
            storage.Clear();
        }

        public void SetFact(FixedFact fact)
        {
            storage.Add(fact);
        }

        private class Storage
        {
            private class Node
            {
                public FixedFact Value;
                public Node? Next;

                public Node(FixedFact value, Node? next)
                {
                    Value = value;
                    Next = next;
                }
            }

            private Node? head = null;

            public void Add(FixedFact fact)
            {
                if (head == null)
                {
                    head = new Node(fact, null);
                    return;
                }

                for (Node? current = head; ;)
                {
                    if (current.Value.Name == fact.Name)
                    {
                        current.Value = fact;
                        return;
                    }

                    if (current.Next == null)
                    {
                        current.Next = new Node(fact, null);
                        return;
                    }

                    current = current.Next;
                }
            }

            public FixedFact? Get(string name)
            {
                for (Node? current = head; current != null; current = current.Next)
                    if (current.Value.Name == name)
                        return current.Value;

                return null;
            }

            public void Clear()
            {
                head = null;
            }
        }
    }
}