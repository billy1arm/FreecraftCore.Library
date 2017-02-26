using FreecraftCore.API.Common;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Response payload sent in response to the <see cref="AuthLogonProofRequest"/>.
	/// </summary>
	[WireDataContract]
	[AuthenticationPayload(AuthOperationCode.AUTH_LOGON_PROOF, AuthOperationDestinationCode.Client)]
	public class AuthLogonProofResponse : AuthenticationPayload
	{
		//TODO: Implement
		public override bool isValid { get; } = true;

		//Not a wire member. Pull from proof result. It eats the byte for type info
		/// <summary>
		/// Indicates the result of the Authentication attempt.
		/// </summary>
		public AuthenticationResult AuthResult => ProofResult.Result;

		/// <summary>
		/// Contains the information sent as a response to the Proof attempt.
		/// </summary>
		[WireMember(1)]
		public ILogonProofResult ProofResult { get; private set; }
		
		//TODO: Add real ctor. Right now we only implement client stuff and this is sent by server.

		public AuthLogonProofResponse()
		{

		}
	}
}
