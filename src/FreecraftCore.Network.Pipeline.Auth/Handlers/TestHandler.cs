using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
    public class TestHandler : INetworkMessagePipelineListenerAsync<INetworkMessageContext<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
	{
		/// <inheritdoc />
		public Task<NetworkMessageContextState> RecievePipelineMessage(INetworkMessageContext<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> input, NetworkMessageContextState currentState)
		{
			throw new NotImplementedException();
		}
	}
}
