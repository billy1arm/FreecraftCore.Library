using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	//It may seem silly to have a factory for this but
	//it gives us a later vector to implement message context and builder recycling for perf and GC pressure reduction.
	/// <summary>
	/// Authentication <see cref="INetworkMessageContextBuilder{TOperationCodeType,THeaderType,TPayloadType}"/> factory.
	/// </summary>
	public class AuthenticationNetworkMessageContextBuilderFactory : INetworkMessageContextBuilderFactory<DefaultNetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
	{
		/// <inheritdoc />
		public DefaultNetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> CreateNew()
		{
			//This probably looks confusing but it's just a way to have kept these two classes generic.
			//We use a generic type and a complex func to initialize a new authentication packet.
			return new DefaultNetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>(c =>
			{
				return new DefaultNetworkMessageContext<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>(new AuthenticationPacket(c.GameHeader, c.GamePayload));
			} );
		}
	}
}
