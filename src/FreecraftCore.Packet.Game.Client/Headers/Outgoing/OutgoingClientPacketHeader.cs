using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	[WireDataContract]
	public class OutgoingClientPacketHeader : IGamePacketHeader
	{
		//Header only contains 2 byte (short) packet size and 4 byte opcode
		/// <inheritdoc />
		public int HeaderSize { get; } = 6;

		//Client sends a bigendian ushort representation of the
		//Packet size which is 4 bytes (from WoW header) + Payload.Length
		[ReverseData] //makes bigendian
		[WireMember(1)]
		private readonly ushort _PacketSize;

		[WireMember(2)]
		public NetworkOperationCode OperationCode { get; private set; }

		//This is an unknown chunk of data
		//Trinitycore has the recieval being a 32bit operation code
		//But it's more likely the first 2 bytes are the little endian ordered opcode
		//and these 2 bytes likely represent something different
		[WireMember(3)]
		private readonly byte[] unknownBytes = new byte[2];

		/// <inheritdoc />
		public int PayloadSize => _PacketSize - sizeof(ushort) - sizeof(NetworkOperationCode) - 2; //computed as the packet size minus header/opcode info

		/// <inheritdoc />
		public bool isValid => _PacketSize >= HeaderSize; //Is only valid if we have at least a header

		public OutgoingClientPacketHeader()
		{
			
		}		
	}
}
