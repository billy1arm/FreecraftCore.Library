using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	public interface IPipelineTypeFlagsProvider
	{
		/// <summary>
		/// The pipeline type flags for registeration.
		/// </summary>
		NetworkPipelineTypes PipelineFlags { get; }
	}
}
