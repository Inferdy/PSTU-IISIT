namespace ProductionSystem
{
	public class Rule
	{
		public readonly string RuleName;
		public readonly string FactName;
		public readonly int Importance;
		private string directValue;
		private IRulePart rootRulePart;

		public Rule(string ruleName, string factName, int importance, string directValue, IRulePart rootRulePart)
		{
			throw new System.NotImplementedException();
		}

		public bool? GetBoolValue(IFactsProvider factsProvider)
		{
			throw new System.NotImplementedException();
		}

		public bool IsActive(bool boolValue)
		{
			throw new System.NotImplementedException();
		}

		public FixedFact GetFact(bool boolValue)
		{
			throw new System.NotImplementedException();
		}
	}
}
