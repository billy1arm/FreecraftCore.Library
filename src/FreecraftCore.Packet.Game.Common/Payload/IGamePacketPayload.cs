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
		//Nothing. To explain this is just a placeholder/metadata
		//for the payloads.
		/// <summary>
		/// Indicates the operation code of the payload.
		/// </summary>
		NetworkOperationCode OperationCode { get; }
	}
}
