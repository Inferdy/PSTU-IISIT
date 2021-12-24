namespace ProductionSystem
{
    public class OrRulePart : IRulePart
    {
        private IRulePart[] ruleParts;

        public OrRulePart(IRulePart[] ruleParts)
        {
            this.ruleParts = ruleParts;
        }

        public Tuple<ExclusiveList<FixedFact>?, bool> Explain(IFactsProvider factsProvider)
        {
            bool failed = true;

            ExclusiveList<FixedFact> list = new ExclusiveList<FixedFact>();

            Tuple<ExclusiveList<FixedFact>?, bool> tuple;

            foreach (IRulePart rulePart in ruleParts)
            {
                tuple = rulePart.Explain(factsProvider);

                if (tuple.Item2)
                {
                    if (failed)
                    {
                        failed = false;

                        list.Clear();
                    }

                    list.Merge(tuple.Item1);
                    break;
                }
                else
                {
                    if (failed)
                    {
                        list.Merge(tuple.Item1);
                    }
                }
            }

            return new Tuple<ExclusiveList<FixedFact>?, bool>(list, !failed);
        }

        public bool GetValue(IFactsProvider factsProvider)
        {
            foreach (IRulePart rulePart in ruleParts)
                if (rulePart.GetValue(factsProvider))
                    return true;

            return false;
        }
    }
}