using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Interface metadata market for game payloads.
	/// </summary>
	[WireDataContract]
	public interface IGamePacketPayload
	{
		/// <summary>
		/// Indicates if the payload is in a valid state.
		/// </summary>
		bool isValid { get; }
	}
}
