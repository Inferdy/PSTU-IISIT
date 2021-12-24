namespace ProductionSystem
{
    internal interface ILockEnumerator<T> : IEnumerator<T>
    {
        ILocker<T> GetLocker();
    }
}