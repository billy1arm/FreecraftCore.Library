using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Small packet header.
	/// </summary>
	public class IncomingClientSmallPacketHeader : IncomingClientPacketHeader
	{
		//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
		[KnownSize(4)]
		[WireMember(1)]
		private readonly byte[] encodedSizeBytes; //[b...][u1...]

		/// <inheritdoc />
		public override int HeaderSize { get; } = 4; //this indicates that the first 3 bytes of the stream involve the header

		/// <inheritdoc />
		protected override int ComputePayloadSize()
		{
			//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
			return (int)(((uint)encodedSizeBytes[0]) << 8 | encodedSizeBytes[1]);
		}
	}
}
