namespace ProductionSystem
{
	public class ProductionSystem : IProductionSystem
	{
		private IKnowledgeBase knowledgeBase;
		private IInferenceEngine inferenceEngine;
		IFactsReceiver factsReceiver;
		IWorkingMemory workingMemory;

		public ProductionSystem(string wmPath, IFactsReceiver factsReceiver)
		{
            Rule[] rules = JsonRuleReader.Parse(wmPath).ToArray();

            knowledgeBase = new KnowledgeBase(rules);

            inferenceEngine = new InferenceEngine();

            this.factsReceiver = factsReceiver;

            workingMemory = new WorkingMemory();
		}

        public event FactFixedEventHandler? OnFactFixed;

        public Tuple<string, IEnumerable<FixedFact>?>? Explain(string factName)
        {
            Tuple<FixedFact, Rule?>? tuple = workingMemory.GetFactAndRule(factName);

            if (tuple == null || tuple.Item2 == null)
                return null;

            string ruleName = tuple.Item2.RuleName;

            ExclusiveList<FixedFact>? facts = tuple.Item2.Explain(workingMemory);

            return new Tuple<string, IEnumerable<FixedFact>?>(ruleName, facts);
        }

        public void Run(List<string> importantFacts)
        {
            for(FixedFact? newFact = factsReceiver.GetNewFact(workingMemory);
                newFact != null;
                newFact = factsReceiver.GetNewFact(workingMemory))
                do
                {
                    //Добавляем факт в рабочую память
                    workingMemory.AddFact(newFact);

                    //Событие при добавлении нового факта в рабочую память
                    OnFactFixed?.Invoke(newFact);

                    //Перечислитель назблокированных правил
                    ILockEnumerator<Rule> enumerator = knowledgeBase.GetUnlockedEnumerator();

                    //Добавление в МЛВ активных правил и блокировка недействительных
                    inferenceEngine.Sort(enumerator, workingMemory);

                    //Выводим новый факт с помощью МЛВ
                    newFact = inferenceEngine.Infer();
                }
                //Пока не закончатся активные правила в МЛВ
                while (newFact != null);
        }
    }
}