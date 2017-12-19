using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using JetBrains.Annotations;

namespace FreecraftCore.Packet.Auth
{
	//We cannot just deserialize this object because there is not a 1:1 mapping for
	//Authentication Operation Codes
	/// <summary>
	/// Represents a strongly typed DTO for an authentication message.
	/// </summary>
	/// <typeparam name="TPacketPayloadType"></typeparam>
	public class AuthenticationPacket : IAuthenticationPacket
	{
		/// <inheritdoc />
		public IAuthenticationPacketHeader Header { get; }

		/// <inheritdoc />
		public AuthenticationServerPayload Payload { get; }

		public AuthenticationPacket([NotNull] IAuthenticationPacketHeader authPacketHeader, [NotNull] AuthenticationServerPayload authPayload)
		{
			if (authPacketHeader == null) throw new ArgumentNullException(nameof(authPacketHeader));
			if (authPayload == null) throw new ArgumentNullException(nameof(authPayload));

			Header = authPacketHeader;
			Payload = authPayload;
		}
	}
}
