using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Contract for types that can yield a complete set of options.
	/// </summary>
	public interface ICompleteOptionsReadable : IPipelineTypeFlagsProvider, IPipelineOrderingStrategy
	{

	}
}
