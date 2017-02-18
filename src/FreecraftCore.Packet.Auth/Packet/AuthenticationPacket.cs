using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	//We cannot just deserialize this object because there is not a 1:1 mapping for
	//Authentication Operation Codes
	public class AuthenticationPacket : IAuthenticationPacket
	{
		[NotNull]
		public IAuthenticationPacketHeader AuthPacketHeader { get; }

		[NotNull]
		public IAuthenticationPayload AuthPayload { get; }

		public AuthenticationPacket([NotNull] IAuthenticationPacketHeader authPacketHeader, [NotNull] IAuthenticationPayload authPayload)
		{
			if (authPacketHeader == null) throw new ArgumentNullException(nameof(authPacketHeader));
			if (authPayload == null) throw new ArgumentNullException(nameof(authPayload));

			AuthPacketHeader = authPacketHeader;
			AuthPayload = authPayload;
		}
	}
}
