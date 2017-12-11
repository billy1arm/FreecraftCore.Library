using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Network;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Represents the full context of an game message.
	/// </summary>
	public class GameNetworkMessageContext : INetworkMessageContext<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>
	{
		/// <inheritdoc />
		[NotNull]
		public INetworkPacket<NetworkOperationCode, IGamePacketHeader, GamePacketPayload> NetworkMessage { get; }

		/// <summary>
		/// Basically the JAMLink or information about the connection this message context is associated with.
		/// </summary>
		[NotNull]
		public INetworkConnectionContext ConnectionLink { get; }

		/// <summary>
		/// Creates a new game network message context.
		/// </summary>
		/// <param name="networkMessage"></param>
		/// <param name="connectionLink"></param>
		public GameNetworkMessageContext([NotNull] INetworkPacket<NetworkOperationCode, IGamePacketHeader, GamePacketPayload> networkMessage, [NotNull] INetworkConnectionContext connectionLink)
		{
			if (networkMessage == null) throw new ArgumentNullException(nameof(networkMessage));
			if (connectionLink == null) throw new ArgumentNullException(nameof(connectionLink));

			//We should check header and payload to make sure they are valid.
			if(networkMessage.Header == null)
				throw new ArgumentException($"Provided {nameof(networkMessage)} contains no header.", nameof(networkMessage));

			if (networkMessage.Payload == null)
				throw new ArgumentException($"Provided {nameof(networkMessage)} contains no payload.", nameof(networkMessage));

			NetworkMessage = networkMessage;
			ConnectionLink = connectionLink;
		}
	}
}
