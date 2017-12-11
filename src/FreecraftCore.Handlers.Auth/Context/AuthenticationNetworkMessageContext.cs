using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Network;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Represents the full context of an authentication message.
	/// </summary>
	public class AuthenticationNetworkMessageContext : INetworkMessageContext<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
	{
		/// <inheritdoc />
		[NotNull]
		public INetworkPacket<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> NetworkMessage { get; }

		/// <summary>
		/// Basically the JAMLink or information about the connection this message context is associated with.
		/// </summary>
		[NotNull]
		public INetworkConnectionContext ConnectionLink { get; }

		/// <summary>
		/// Creates a new authentication network message context.
		/// </summary>
		/// <param name="networkMessage"></param>
		/// <param name="connectionLink"></param>
		public AuthenticationNetworkMessageContext([NotNull] INetworkPacket<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> networkMessage, [NotNull] INetworkConnectionContext connectionLink)
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
