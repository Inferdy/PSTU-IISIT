namespace ProductionSystem
{
    public interface IProductionSystem
    {
        void Run(IPrinter printer, IPrinter logger, IAsker asker);
    }
}