using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Pipeline order registerating strategy that carries the pipeline type with it.
	/// </summary>
	/// <typeparam name="TPipelineType">The pipeline type.</typeparam>
	public interface IPipelineOrderingStrategy
	{
		/// <summary>
		/// Vistor method for a pipeline collection.
		/// </summary>
		/// <param name="pipelines"></param>
		/// <param name="pipeline"></param>
		void RegisterVisitor<TPipelineType>(IList<TPipelineType> pipelines, TPipelineType pipeline);
	}
}
