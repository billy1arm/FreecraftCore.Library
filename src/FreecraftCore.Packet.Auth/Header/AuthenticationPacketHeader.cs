using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// The default <see cref="IAuthenticationPacketHeader"/> and only type of authentication
	/// packet header that WoW sends.
	/// </summary>
	[WireDataContract]
	public class AuthenticationPacketHeader : IAuthenticationPacketHeader
	{
		/// <inheritdoc />
		[WireMember(1)]
		public AuthOperationCode AuthenticationOpCode { get; private set; }

		public AuthenticationPacketHeader(AuthOperationCode authenticationOpCode)
		{
			if (!Enum.IsDefined(typeof(AuthOperationCode), authenticationOpCode))
				throw new ArgumentOutOfRangeException(nameof(authenticationOpCode), "Value should be defined in the AuthOperationCode enum.");

			AuthenticationOpCode = authenticationOpCode;
		}
	}
}
