namespace ProductionSystem
{
	public class TwoHeadedRule : Rule
	{
		private string reverseValue;

		public TwoHeadedRule(
			string ruleName,
			string factName,
			int importance,
			string directValue,
			string reverseValue,
			IRulePart rootRulePart) : base(ruleName, factName, importance, directValue, rootRulePart)
		{
			this.reverseValue = reverseValue;			
		}

        public override FixedFact GetFact(bool boolValue)
        {
			if (boolValue)
				return new FixedFact(FactName, directValue);
			else
				return new FixedFact(FactName, reverseValue);
        }

        public override bool IsActive(bool boolValue)
        {
			return true;
        }
    }
}