using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Structure for option's API to configure a pipeline registeration.
	/// </summary>
	public class PipelineRegisterationOptions
	{
		/// <summary>
		/// The pipeline type flags for registeration.
		/// </summary>
		internal NetworkPipelineTypes PipelineFlags { get; set; }

		/// <summary>
		/// The order for the pipeline.
		/// </summary>
		internal NetworkPipelineRegisterationOrder PipelineRegisterationOrder { get; set; }
	}
}
