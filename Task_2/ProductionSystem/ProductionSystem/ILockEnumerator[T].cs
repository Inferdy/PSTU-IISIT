namespace ProductionSystem
{
	public interface ILockEnumerator<T> : IEnumerator<T>
	{
		void LockCurrent();
	}
}