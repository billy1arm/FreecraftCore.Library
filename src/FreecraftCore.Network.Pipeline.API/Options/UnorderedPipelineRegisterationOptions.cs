using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Structure for option's API to configure a pipeline registeration.
	/// (basically contains and tracks state for building)
	/// </summary>
	public class UnorderedPipelineRegisterationOptions : ICompleteOptionsReadable, IPipelineTypeFlagsMutatable
	{
		/// <summary>
		/// The pipeline type flags for registeration.
		/// </summary>
		public NetworkPipelineTypes PipelineFlags { get; set; }

		NetworkPipelineTypes IPipelineTypeFlagsProvider.PipelineFlags => PipelineFlags;

		/// <inheritdoc />
		public void RegisterVisitor<TPipelineType>(IList<TPipelineType> pipelines, TPipelineType pipeline)
		{
			new DefaltPipelineOrder().RegisterVisitor(pipelines, pipeline);
		}
	}
}
