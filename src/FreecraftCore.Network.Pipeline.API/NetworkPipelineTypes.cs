using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Flags enumeration for possible pipeline types.
	/// </summary>
	[Flags]
	public enum NetworkPipelineTypes
	{
		/// <summary>
		/// Indicates a default pipeline.
		/// </summary>
		Default = 1 << 0,

		/// <summary>
		/// Flag for preprocessing. Is a pipeline are pipelines that preprocess
		/// </summary>
		Preprocessing = 1 << 1,

		/// <summary>
		/// Flag for a main pipeline (for header or payload default pipelines)
		/// </summary>
		Main = 1 << 2,

		/// <summary>
		/// Flag for a header pipeline.
		/// </summary>
		Header = 1 << 3,

		/// <summary>
		/// Flag for a payload pipeline
		/// </summary>
		Payload = 1 << 4,

		/// <summary>
		/// Flag for postprocessing. Is a pipeline are pipelines that postprocesses.
		/// </summary>
		PostProcessing = 1 << 5,

		/// <summary>
		/// Flag for the error pipeline.
		/// </summary>
		Error = 1 << 6,
	}
}
