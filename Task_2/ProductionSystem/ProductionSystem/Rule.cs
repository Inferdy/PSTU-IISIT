namespace ProductionSystem
{
	public class Rule
	{
		public readonly string RuleName;
		public readonly string FactName;
		public readonly int Importance;
		protected string directValue;
		private IRulePart rootRulePart;

		public Rule(
			string ruleName,
			string factName,
			int importance,
			string directValue,
			IRulePart rootRulePart)
		{
			RuleName = ruleName;
			FactName = factName;
			Importance = importance;
			this.directValue = directValue;
			this.rootRulePart = rootRulePart;
		}

		public bool? GetBoolValue(IFactsProvider factsProvider)
		{
			return rootRulePart.GetValue(factsProvider);
		}

		public virtual bool IsActive(bool boolValue)
		{
			return boolValue = true;
		}

		public virtual string GetFactValue(bool boolValue)
        {
			if (boolValue)
				return directValue;

			throw new UnexpectedContradictionException();
        }

		public virtual FixedFact GetFact(bool boolValue)
		{
			return new FixedFact(FactName, GetFactValue(boolValue));
		}

		public ExclusiveList<FixedFact>? Explain(IFactsProvider factsProvider)
        {
            Tuple<ExclusiveList<FixedFact>?, bool?> tuple = rootRulePart.Explain(factsProvider);

			return tuple.Item1;
        }
	}
}