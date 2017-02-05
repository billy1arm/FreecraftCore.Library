using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Large packet header.
	/// </summary>
	[WireDataContract]
	public class IncomingClientLargePacketHeader : IncomingClientPacketHeader
	{
		//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
		[KnownSize(5)] 
		[WireMember(1)]
		private readonly byte[] encodedSizeBytes; //[b...][u1...]

		/// <inheritdoc />
		public override int HeaderSize { get; } = 5; //this indicates that the first 5 bytes of the stream involve the header

		/// <inheritdoc />
		protected override int ComputePayloadSize()
		{
			//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
			return (int)(((((uint)encodedSizeBytes[0]) & 0x7F) << 16) | (((uint)encodedSizeBytes[1]) << 8) | encodedSizeBytes[2]);
		}
	}
}
