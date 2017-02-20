using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Changes the destination pipeline type to target <see cref="NetworkPipelineTypes"/>.Header.
	/// </summary>
	public class HeaderPipeline : IPipelineTypeFlagsStrategy
	{
		/// <inheritdoc />
		public NetworkPipelineTypes TransformTypeFlags(NetworkPipelineTypes types)
		{
			//Transform the flags to include payloads.
			return types | NetworkPipelineTypes.Header;
		}
	}
}
