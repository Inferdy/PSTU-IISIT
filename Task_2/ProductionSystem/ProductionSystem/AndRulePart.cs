namespace ProductionSystem
{
	public class AndRulePart : IRulePart
	{
		private IRulePart[] ruleParts;

		public AndRulePart(IRulePart[] ruleParts)
		{
			this.ruleParts = ruleParts;
		}

        public Tuple<ExclusiveList<FixedFact>?, bool?> Explain(IFactsProvider factsProvider)
        {
            bool defined = true;
            bool ok = true;

            ExclusiveList<FixedFact> list = new ExclusiveList<FixedFact>();

            Tuple<ExclusiveList<FixedFact>?, bool?> value;

            foreach (IRulePart rulePart in ruleParts)
            {
                value = rulePart.Explain(factsProvider);

                switch (value.Item2)
                {
                    case false:
                        if (ok)
                        {
                            ok = false;

                            list.Clear();
                        }

                        list.Merge(value.Item1);
                        break;
                    case null:
                        if (ok && defined)
                            list.Clear();

                        defined = false;
                        break;
                    case true:
                        if (ok && defined)
                            list.Merge(value.Item1);
                        break;
                }
            }

            if (ok)
            {
                if (defined)
                    return new Tuple<ExclusiveList<FixedFact>?, bool?>(list, true);
                else
                    return new Tuple<ExclusiveList<FixedFact>?, bool?>(null, null);
            }
            else
                return new Tuple<ExclusiveList<FixedFact>?, bool?>(list, false);
        }

        public bool? GetValue(IFactsProvider factsProvider)
        {
            bool defined = true;

            foreach (IRulePart rulePart in ruleParts)
                switch (rulePart.GetValue(factsProvider))
                {
                    case false:
                        return false;
                    case null:
                        defined = false;
                        break;
                    case true:
                        break;
                }

            if (defined)
                return true;

            return null;
        }
    }
}