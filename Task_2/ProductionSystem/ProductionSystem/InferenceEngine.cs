namespace ProductionSystem
{
	public class InferenceEngine : IInferenceEngine
	{
		private class SortedList
        {
            private class Node
            {
                public Rule Rule;
                public bool Value;
                public Node? Next;

                public Node(Rule rule, bool value, Node? next)
                {
                    Rule = rule;
                    Value = value;
                    Next = next;
                }
            }

            private Node? head = null;

            public void Add(Rule rule, bool value)
            {
                if (head == null)
                {
                    head = new Node(rule, value, null);
                }

                int importance = rule.Importance;
                if (importance >= head.Rule.Importance)
                    head = new Node(rule, value, head);

                Node current = head;
                Node? next = current.Next;

                while (next != null)
                {
                    if (next.Rule.Importance <= importance)
                        break;

                    current = next;
                    next = next.Next;
                }

                current.Next = new Node(rule, value, next);
            }

            public Tuple<Rule, bool>? PopFirst()
            {
                if (head == null)
                    return null;

                Rule rule = head.Rule;
                bool value = head.Value;

                head = head.Next;

                return new Tuple<Rule, bool>(rule, value);
            }
        }

        private SortedList storage = new SortedList();

        public FixedFact? Infer()
        {
            Tuple<Rule, bool>? tuple = storage.PopFirst();

            if (tuple == null)
                return null;

            return tuple.Item1.GetFact(tuple.Item2);
        }

        public void Sort(ILockEnumerator<Rule> lockEnumerator, IFactsProvider factsProvider)
        {
            Rule current;
            bool? boolValue;

            while(lockEnumerator.MoveNext())
            {
                current = lockEnumerator.Current;

                boolValue = current.GetBoolValue(factsProvider);

                if (boolValue == null)
                    break;

                if (current.IsActive((bool)boolValue))
                    storage.Add(current, (bool)boolValue);

                lockEnumerator.LockCurrent();
            }    
        }
    }
}