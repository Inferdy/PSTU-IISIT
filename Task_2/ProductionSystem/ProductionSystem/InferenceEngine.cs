namespace ProductionSystem
{
    internal class InferenceEngine : IInferenceEngine
    {
        private ILocker<IRule>? locker = null;

        public FixedFact? Infer(IPrinter printer, IAsker asker)
        {
            FixedFact? fact = locker.Value.Activate(printer, asker);

            locker.Lock();
            locker = null;

            return fact;
        }

        public bool IsActive()
        {
            return locker != null;
        }

        public bool Sort(IEnumerator<ILocker<IRule>> enumerator, IFactsProvider factsProvider)
        {
            bool result = false;

            IRule currentRule;

            while (enumerator.MoveNext())
            {
                currentRule = enumerator.Current.Value;

                if (currentRule.IsActive(factsProvider))
                {
                    result = true;

                    if (locker == null)
                        locker = enumerator.Current;
                    else
                    if (currentRule.Importance > locker.Value.Importance)
                        locker = enumerator.Current;
                }
            }

            return result;
        }
    }
}