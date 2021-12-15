namespace ProductionSystem
{
    internal enum RuleState
    {
        Valid,
        Invalid,
        Active
    }

    internal class Rule
    {
        public readonly string Name;
        public readonly string DirectValue;

        protected IRulePart _rootRulePart;

        public Rule(IRulePart rootRulePart, string name, string directValue)
        {
            _rootRulePart = rootRulePart;
            Name = name;
            DirectValue = directValue;
        }

        public virtual RuleState GetRuleState(IFactsProvider factsProvider)
        {
            switch (_rootRulePart.GetState(factsProvider))
            {
                case null:
                    return RuleState.Valid;
                case true:
                    return RuleState.Active;
                case false:
                    return RuleState.Invalid;
            }
        }

        public IEnumerable<FixedFact> Explain(IFactsProvider factsProvider)
        {
            return _rootRulePart.Explain(factsProvider);
        }
    }
}