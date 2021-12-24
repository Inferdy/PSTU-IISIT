namespace ProductionSystem
{
    public class ProductionSystem : IProductionSystem
    {
        private IKnowledgeBase kb;

        private IWorkingMemory wm;

        private IInferenceEngine engine;

        public ProductionSystem(string pathKB)
        {
            IRule[] rules = JsonRuleReader.Parse(pathKB);

            kb = new KnowledgeBase(rules);

            wm = new WorkingMemory();

            engine = new InferenceEngine();
        }

        public void Run(IPrinter printer, IPrinter logger, IAsker asker)
        {
            kb.Reset();
            wm.Reset();

            FixedFact? fact;

            IEnumerator<ILocker<IRule>> enumerator = kb.GetEnumerator();

            while (engine.Sort(enumerator, wm))
            {
                fact = engine.Infer(wm, printer, logger, asker);

                if (fact != null)
                    wm.SetFact(fact);

                enumerator = kb.GetEnumerator();
            }
        }
    }
}