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
			throw new System.NotImplementedException();
		}
	}
}
