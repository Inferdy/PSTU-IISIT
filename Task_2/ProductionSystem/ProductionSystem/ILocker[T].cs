namespace ProductionSystem
{
    internal interface ILocker<T>
    {
        T Value { get; }

        void Lock();
    }
}