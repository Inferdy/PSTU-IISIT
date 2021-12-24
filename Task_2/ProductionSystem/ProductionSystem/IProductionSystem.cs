namespace ProductionSystem
{
	public interface IProductionSystem
	{
		void Run(IPrinter printer, IAsker asker);
	}
}