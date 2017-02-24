using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Simplified type alias for a <see cref="WireReaderInputPipelineService{TContextBuilderType,TNetworkOperationCodeType,THeaderType,TPayloadType}"/> for authentication
	/// network stream inputes.
	/// </summary>
	public abstract class AuthenticationWireReaderPipelineComponent : WireReaderPipelineComponent<INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
	{

	}
}
