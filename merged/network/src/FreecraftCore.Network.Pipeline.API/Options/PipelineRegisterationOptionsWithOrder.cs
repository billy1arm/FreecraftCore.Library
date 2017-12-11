using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	public class PipelineRegisterationOptionsWithOrder<TOrderStrategyType> : ICompleteOptionsReadable
		where TOrderStrategyType : IPipelineOrderingStrategy, new()
	{
		/// <inheritdoc />
		public NetworkPipelineTypes PipelineFlags { get; set; }

		public PipelineRegisterationOptionsWithOrder(NetworkPipelineTypes pipelineFlags)
		{
			//TODO: Check flags

			PipelineFlags = pipelineFlags;
		}

		/// <inheritdoc />
		public void RegisterVisitor<TPipelineType>(IList<TPipelineType> pipelines, TPipelineType pipeline)
		{
			new TOrderStrategyType().RegisterVisitor(pipelines, pipeline);
		}
	}
}
