namespace ProductionSystem
{
    internal class TrueRulePart : IRulePart
    {
        public Tuple<ExclusiveList<FixedFact>?, bool> Explain(IFactsProvider factsProvider)
        {
            return new Tuple<ExclusiveList<FixedFact>?, bool>(null, true);
        }

        public bool GetValue(IFactsProvider factsProvider)
        {
            return true;
        }
    }
}