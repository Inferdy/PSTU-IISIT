namespace ProductionSystem
{
	public class OrRulePart : IRulePart
	{
		private IRulePart[] ruleParts;

		public OrRulePart(IRulePart[] ruleParts)
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
                    case true:
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
                    case false:
                        if (ok && defined)
                            list.Merge(value.Item1);
                        break;
                }
            }

            if (ok)
            {
                if (defined)
                    return new Tuple<ExclusiveList<FixedFact>?, bool?>(list, false);
                else
                    return new Tuple<ExclusiveList<FixedFact>?, bool?>(null, null);
            }
            else
                return new Tuple<ExclusiveList<FixedFact>?, bool?>(list, true);
        }

        public bool? GetValue(IFactsProvider factsProvider)
        {
            bool defined = true;

            foreach (IRulePart rulePart in ruleParts)
                switch (rulePart.GetValue(factsProvider))
                {
                    case true:
                        return true;
                    case null:
                        defined = false;
                        break;
                    case false:
                        break;
                }

            if (defined)
                return false;

            return null;
        }
    }
}