namespace ProductionSystem
{
	public class AndRulePart : IRulePart
	{
		private IRulePart[] ruleParts;

		public AndRulePart(IRulePart[] ruleParts)
		{
			this.ruleParts = ruleParts;
		}

        public Tuple<ExclusiveList<FixedFact>?, bool> Explain(IFactsProvider factsProvider)
        {
            bool ok = true;

            ExclusiveList<FixedFact> list = new ExclusiveList<FixedFact>();

            Tuple<ExclusiveList<FixedFact>?, bool> tuple;

            foreach (IRulePart rulePart in ruleParts)
            {
                tuple = rulePart.Explain(factsProvider);

                if (tuple.Item2)
                {
                    if (ok)
                        list.Merge(tuple.Item1);
                    break;
                }
                else
                {
                    if (ok)
                    {
                        ok = false;

                        list.Clear();
                    }

                    list.Merge(tuple.Item1);
                }
            }

            return new Tuple<ExclusiveList<FixedFact>?, bool>(list, ok);
        }

        public bool GetValue(IFactsProvider factsProvider)
        {
            foreach (IRulePart rulePart in ruleParts)
                if (!rulePart.GetValue(factsProvider))
                    return false;

            return true;
        }
    }
}