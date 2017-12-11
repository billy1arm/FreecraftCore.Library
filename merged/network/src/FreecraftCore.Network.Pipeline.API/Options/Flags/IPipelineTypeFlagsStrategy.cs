using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Strategy for transformations on <see cref="NetworkPipelineTypes"/>.
	/// </summary>
	public interface IPipelineTypeFlagsStrategy
	{
		/// <summary>
		/// Transforms the provided <see cref="NetworkPipelineTypes"/> flags
		/// based on the defined derived strategy.
		/// </summary>
		/// <param name="types">The type flags to use for transformation.</param>
		/// <returns>The transformed flags.</returns>
		NetworkPipelineTypes TransformTypeFlags(NetworkPipelineTypes types);
	}
}
