using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	public class AggregateOptionsWithOrderStrategyDecorated : IPipelineTypeFlagsMutatable, IPipelineTypeFlagsProvider, ICompleteOptionsReadable
	{
		/// <inheritdoc />
		public NetworkPipelineTypes PipelineFlags { get; set; }

		private IPipelineOrderingStrategy DecoratedOrderStrategy { get; }

		public AggregateOptionsWithOrderStrategyDecorated([NotNull] IPipelineOrderingStrategy decoratedOrderStrategy, NetworkPipelineTypes pipelineFlags)
		{
			if (decoratedOrderStrategy == null) throw new ArgumentNullException(nameof(decoratedOrderStrategy));

			DecoratedOrderStrategy = decoratedOrderStrategy;
			PipelineFlags = pipelineFlags;
		}

		/// <inheritdoc />
		public void RegisterVisitor<TPipelineType>(IList<TPipelineType> pipelines, TPipelineType pipeline)
		{
			DecoratedOrderStrategy.RegisterVisitor(pipelines, pipeline);
		}
	}
}
