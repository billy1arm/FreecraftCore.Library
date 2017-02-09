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
		[KnownSize(3)] 
		[WireMember(1)]
		private readonly byte[] encodedSizeBytes; //[a][bb]

		/// <inheritdoc />
		public override int HeaderSize { get; } = 3; //this indicates that the first 3 bytes of the stream involve the header

		/// <inheritdoc />
		protected override int ComputePayloadSize()
		{
			if (encodedSizeBytes == null)
				throw new InvalidOperationException($"{nameof(IncomingClientLargePacketHeader)} did not contain any encoded bytes.");

			//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
			return (int)(((((uint)encodedSizeBytes[0]) & 0x7F) << 16) | (((uint)encodedSizeBytes[1]) << 8) | encodedSizeBytes[2]);
		}

		//Just check the validity of the encoded size bytes.
		/// <inheritdoc />
		public override bool isValid => encodedSizeBytes != null && encodedSizeBytes.Length == HeaderSize;
	}
}
