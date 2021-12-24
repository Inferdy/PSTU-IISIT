namespace ProductionSystem
{
    internal class InferenceEngine : IInferenceEngine
    {
        private ILocker<IRule>? locker = null;

        public FixedFact? Infer(IFactsProvider factsProvider, IPrinter printer, IPrinter logger, IAsker asker)
        {
            FixedFact? fact = locker.Value.Activate(factsProvider, printer, logger, asker);

            locker.Lock();
            locker = null;

            return fact;
        }

        public bool IsActive()
        {
            return locker != null;
        }

        public bool Sort(ILockEnumerator<IRule> enumerator, IFactsProvider factsProvider)
        {
            locker = null;

            while (enumerator.MoveNext())
                if (enumerator.Current.IsActive(factsProvider))
                {
                    if (locker == null)
                        locker = enumerator.GetLocker();
                    else
                    if (enumerator.Current.Importance > locker.Value.Importance)
                        locker = enumerator.GetLocker();
                }

            return locker != null;
        }
    }
}