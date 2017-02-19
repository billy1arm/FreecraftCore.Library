using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Simplified type alise for <see cref="INetworkMessageContext{TOperationCodeType,THeaderType,TPayloadType}"/> for authentication contexts.
	/// See implemented interfaces for real functionality.
	/// </summary>
	public interface IAuthenticationNetworkMessageContextBuilder : INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
	{

	}
}
