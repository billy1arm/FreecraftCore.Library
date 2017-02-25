using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Type alia for the generic <see cref="WireWriterInputPipelineService{TMessageType,TNetworkOperationCodeType,THeaderType,TPayloadType}"/>
	/// used for authentication messages.
	/// </summary>
	public class AuthenticationWireWriterInputPipelineService : WireWriterInputPipelineService<AuthenticationPayload, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
	{

	}
}
