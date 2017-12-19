using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	public interface IPipelineTypeFlagsMutatable : IPipelineTypeFlagsProvider
	{
		/// <summary>
		/// The pipeline type flags for registeration.
		/// </summary>
		new NetworkPipelineTypes PipelineFlags { get; set; }
	}
}
