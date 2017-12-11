using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Changes the destination pipeline type to target <see cref="NetworkPipelineTypes"/>.Payload.
	/// </summary>
	public class PayloadPipeline : IPipelineTypeFlagsStrategy
	{
		public static NetworkPipelineTypes PipelineType { get; } = NetworkPipelineTypes.Payload;

		public class Main : IPipelineTypeFlagsStrategy
		{
			/// <inheritdoc />
			public NetworkPipelineTypes TransformTypeFlags(NetworkPipelineTypes types)
			{
				//Transform the flags to include payloads
				return types | PipelineType | NetworkPipelineTypes.Main;
			}
		}

		public class Error : IPipelineTypeFlagsStrategy
		{
			/// <inheritdoc />
			public NetworkPipelineTypes TransformTypeFlags(NetworkPipelineTypes types)
			{
				//Transform the flags to include payloads
				return types | PipelineType | NetworkPipelineTypes.Error;
			}
		}

		/// <inheritdoc />
		public NetworkPipelineTypes TransformTypeFlags(NetworkPipelineTypes types)
		{
			//Transform the flags to include payloads
			return types | PipelineType;
		}
	}
}
