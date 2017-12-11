using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.API.Common;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.SMSG_AUTH_RESPONSE)] //the response to a request to authenticate a session
	[ProtocolGrouping(ProtocolCode.Authentication)] //Though this isn't part of the actual authserver stuff it's still auth.
	public class AuthenticateSessionResponse : GamePacketPayload
	{
		/// <inheritdoc />
		public override bool isValid { get; } = true;

		/// <summary>
		/// Indicates the result of the session authentication.
		/// </summary>
		[WireMember(1)]
		public SessionAuthenticationResult AuthenticationResult { get; private set; }

		public AuthenticateSessionResponse()
		{
			//TODO: If we ever run a server add a ctor
		}
	}
}
