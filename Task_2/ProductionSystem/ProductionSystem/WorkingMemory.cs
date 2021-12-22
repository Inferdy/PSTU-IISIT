namespace ProductionSystem
{
    public class WorkingMemory : IWorkingMemory
    {
        private Dictionary<FixedFact, Rule> storage = new Dictionary<FixedFact, Rule>();

        public bool AddFact(FixedFact fact)
        {
            foreach (KeyValuePair<FixedFact, Rule> pair in storage)
                if (pair.Key.Name == fact.Name)
                {
                    if (pair.Key.Value == fact.Value)
                        return true;
                    else
                        throw new UnexpectedContradictionException();
                }

            return false;
        }

        public FixedFact? GetFact(string name)
        {
            foreach (KeyValuePair<FixedFact, Rule> pair in storage)
                if (pair.Key.Name == name)
                    return pair.Key;

            return null;
        }

        public Tuple<FixedFact, Rule?>? GetFactAndRule(string factName)
        {
            foreach (KeyValuePair<FixedFact, Rule> pair in storage)
                if (pair.Key.Name == factName)
                    return new Tuple<FixedFact, Rule?>(pair.Key, pair.Value);

            return null;
        }

        public void Reset()
        {
            storage.Clear();
        }
    }
}