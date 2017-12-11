using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Service for handling game network inputs utilizing the pipeline system.
	/// (basically an alias for now for the ugly complex generic type <see cref="NetworkInputPipelineService{TContextBuilderType,TNetworkOperationCodeType,THeaderType,TPayloadType}"/>
	/// </summary>
	public class AuthenticationWireReaderInputPipelineService : WireReaderInputPipelineService<DefaultNetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
	{
		/// <inheritdoc />
		public AuthenticationWireReaderInputPipelineService([NotNull] INetworkMessageContextBuilderFactory<DefaultNetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> contextBuilderFactory) 
			: base(contextBuilderFactory)
		{

		}
	}
}
