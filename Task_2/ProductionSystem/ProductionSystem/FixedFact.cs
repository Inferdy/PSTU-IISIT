namespace ProductionSystem
{
    public class FixedFact
    {
        public readonly string Name;
        public readonly string Value;

        public FixedFact(string factName, string factValue)
        {
            Name = factName;
            Value = factValue;
        }
    }
}