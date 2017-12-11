using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Small packet header.
	/// </summary>
	public class IncomingClientSmallPacketHeader : IncomingClientPacketHeader
	{
		//[a][b]
		//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
		[KnownSize(2)]
		[WireMember(1)]
		private readonly byte[] encodedSizeBytes; 

		//[cc]
		//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
		/// <inheritdoc />
		[WireMember(2)] //after the 2 bytes of encoded size
		public override NetworkOperationCode OperationCode { get; protected set; }

		//Should be the size of the encoded size in bytes (2) and the OpCode (2)
		/// <inheritdoc />
		public override int HeaderSize { get; } = 2 + sizeof(NetworkOperationCode);

		/// <inheritdoc />
		protected override int ComputePayloadSize()
		{
			if (encodedSizeBytes == null)
				throw new InvalidOperationException($"{nameof(IncomingClientSmallPacketHeader)} did not contain any encoded bytes.");

			//See: https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/docs/WorldHeader.md
			return (int)(((uint)encodedSizeBytes[0]) << 8 | encodedSizeBytes[1]) - sizeof(NetworkOperationCode);
		}

		//Just check the validity of the encoded size bytes.
		/// <inheritdoc />
		public override bool isValid => encodedSizeBytes != null && encodedSizeBytes.Length == HeaderSize;

		public IncomingClientSmallPacketHeader()
		{
		
		}
	}
}
