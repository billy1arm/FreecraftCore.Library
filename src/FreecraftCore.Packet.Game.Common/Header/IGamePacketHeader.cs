using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.Packet
{
	//TODO: We may use the serializer for this one day.
	[WireDataContract]
	public interface IGamePacketHeader : IMessageVerifyable, IOperationIdentifable<NetworkOperationCode>
	{
		/// <summary>
		/// Indicates the size of the header. 
		/// (usually 4 or 5 bytes)
		/// (is a uint32 but .NET suggests using int when possible)
		/// </summary>
		int HeaderSize { get; }

		/// <summary>
		/// Indicates the size of the payload in this packet.
		/// (is a ushort but .NET suggests using int when possible)
		/// </summary>
		int PayloadSize { get; }
	}
}
