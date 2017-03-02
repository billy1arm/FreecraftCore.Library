using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Crypto;
using FreecraftCore.Packet;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Pipeline component that builds and writes an outgoing game packet.
	/// </summary>
	public class GameOutgoingClientPacketPipelineComponent : GameWireWriterPipelineComponent
	{
		[NotNull]
		private ISerializerService Serializer { get; }

		private ISessionPacketCryptoService PacketCrypto { get; }

		public GameOutgoingClientPacketPipelineComponent([NotNull] ISerializerService serializer, ISessionPacketCryptoService packetCrypto)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			Serializer = serializer;
			PacketCrypto = packetCrypto;
		}

		/// <inheritdoc />
		public override Task<IWireStreamWriterStrategyAsync> RecievePipelineMessage(IWireStreamWriterStrategyAsync input, GamePacketPayload currentState)
		{
			//Doesn't this just suck? We spend so much time building a pipeline system and it works for everything but outgoing packets for games
			byte[] payloadBytes = Serializer.Serialize(currentState);

			//Encrypt the header including the payload byte lengths
			byte[] headerBytes = PacketCrypto.ProcessBytesToNewBuffer(Serializer.Serialize(new OutgoingClientPacketHeader(payloadBytes.Length, currentState.GetOperationCode())), 0);

			//Write the data
			input.Write(headerBytes);
			input.Write(payloadBytes);

			return Task.FromResult(input);
		}
	}
}
