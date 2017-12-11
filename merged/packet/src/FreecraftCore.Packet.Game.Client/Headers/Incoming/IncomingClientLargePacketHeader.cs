using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Large packet header.
	/// </summary>
	[WireDataContract]
	public class IncomingClientLargePacketHeader : IncomingClientPacketHeader
	{
		//[a][bb]
		//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
		[KnownSize(3)]
		[WireMember(1)]
		private readonly byte[] encodedSizeBytes;

		//[cc]
		//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
		/// <inheritdoc />
		[WireMember(2)] //after the 3 bytes of encoded size
		public override NetworkOperationCode OperationCode { get; protected set; }

		//Should be the size of the encoded size in bytes (3) and the OpCode (2)
		/// <inheritdoc />
		public override int HeaderSize { get; } = 3 + sizeof(NetworkOperationCode);

		/// <inheritdoc />
		protected override int ComputePayloadSize()
		{
			if (encodedSizeBytes == null)
				throw new InvalidOperationException($"{nameof(IncomingClientLargePacketHeader)} did not contain any encoded bytes.");

			//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
			return (int)(((((uint)encodedSizeBytes[0]) & 0x7F) << 16) | (((uint)encodedSizeBytes[1]) << 8) | encodedSizeBytes[2]) - sizeof(NetworkOperationCode);
		}

		//Just check the validity of the encoded size bytes.
		/// <inheritdoc />
		public override bool isValid => encodedSizeBytes != null && encodedSizeBytes.Length == HeaderSize;

		public IncomingClientLargePacketHeader()
		{
			
		}

		
	}
}
