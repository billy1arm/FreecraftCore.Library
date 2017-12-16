using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// The header for packets coming into the client.
	/// </summary>
	[DefaultChild(typeof(IncomingClientSmallPacketHeader))] //if it doesn't contain the 0x80 flag it is a small packet
	[WireDataContractBaseTypeByFlags(0x80, typeof(IncomingClientLargePacketHeader))] //Jackpoz bot shows that if the first byte has a 0x80 flag then it is a big packet
	[WireDataContract(WireDataContractAttribute.KeyType.Byte, InformationHandlingFlags.DontConsumeRead)] //Jackpoz shows that first byte indicates length size.
	public abstract class IncomingClientPacketHeader : IGamePacketHeader, ISerializationEventListener
	{
		/// <inheritdoc />
		public abstract int HeaderSize { get; }

		/// <inheritdoc />
		public int PayloadSize { get; private set; }

		/// <inheritdoc />
		public abstract NetworkOperationCode OperationCode { get; protected set; }

		protected IncomingClientPacketHeader()
		{

		}

		/// <summary>
		/// Computes the payload size.
		/// </summary>
		/// <returns>Returns an integer size for the payload.</returns>
		[Pure]
		protected abstract int ComputePayloadSize();

		public void OnBeforeSerialization()
		{
			//Don't need to do anything here
		}

		public void OnAfterDeserialization()
		{
			PayloadSize = ComputePayloadSize();
		}

		/// <inheritdoc />
		public abstract bool isValid { get; }
	}
}
