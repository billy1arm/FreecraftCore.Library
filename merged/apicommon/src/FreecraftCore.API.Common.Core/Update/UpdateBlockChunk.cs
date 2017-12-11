using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Contract for types that are update blocks.
	/// </summary>
	public interface IUpdateBlockChunk
	{
		/// <summary>
		/// Indicates that update type of the chunk.
		/// </summary>
		ObjectUpdateType UpdateType { get; }
	}
}
