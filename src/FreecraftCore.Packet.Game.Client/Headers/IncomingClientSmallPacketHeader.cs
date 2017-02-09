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
		[KnownSize(2)]
		[WireMember(1)]
		private readonly byte[] encodedSizeBytes; //[a][b]

		/// <inheritdoc />
		public override int HeaderSize { get; } = 2; //this indicates that the first 2 bytes of the stream involve the header

		/// <inheritdoc />
		protected override int ComputePayloadSize()
		{
			if (encodedSizeBytes == null)
				throw new InvalidOperationException($"{nameof(IncomingClientSmallPacketHeader)} did not contain any encoded bytes.");

			//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
			return (int)(((uint)encodedSizeBytes[0]) << 8 | encodedSizeBytes[1]);
		}

		//Just check the validity of the encoded size bytes.
		/// <inheritdoc />
		public override bool isValid => encodedSizeBytes != null && encodedSizeBytes.Length == HeaderSize;
	}
}
