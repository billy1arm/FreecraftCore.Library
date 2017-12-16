using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Contract for types that can provide the size of a payload.
	/// </summary>
	public interface IHeaderPayloadSizeable
	{
		/// <summary>
		/// Provides the size of the payload.
		/// (is a unsigned in WoW but .NET suggests using int when possible)
		/// </summary>
		int PayloadSize { get; }
	}
}
