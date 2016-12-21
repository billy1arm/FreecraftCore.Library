using FreecraftCore.API.Common;
using FreecraftCore.API.Common.Auth;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// The response payload to a <see cref="AuthLogonChallengeRequest"/>.
	/// </summary>
	[WireMessage]
	[AuthenticationPayload(AuthOperationCode.AUTH_LOGON_CHALLENGE)]
	public class AuthLogonChallengeResponse : IAuthenticationPayload
	{
		/// <summary>
		/// Unknown 0x00 byte sent from the server.
		/// </summary>
		[WireMember(1)]
		private readonly byte unknownResponseByte;

		//Second piece of data sent is a result
		[WireMember(2)]
		public AuthenticationResult Result { get; private set; }

		/// <summary>
		/// The SRP Token provided by the server.
		/// See: http://srp.stanford.edu/design.html for more information
		/// </summary>
		[WireMember(3)]
		public SRPToken Token { get; private set; }

		//TODO: Create enum
		/// <summary>
		/// Flags that indicate what security additional measures are required for authentication
		/// (Ex. Authenticator or Pin) Will usually be 0 meaning none
		/// </summary>
		[WireMember(4)]
		public byte securityFlags { get; private set; }

		//The server is suppose to send additional info depending on the flags
		//However, the serializer isn't capable of conditionally reading different sizes based on flags
		//It's all just 0s or constants anyway in Trinitycore

		//If this is true then it will contain token data
		public bool isSuccess()
		{
			return Result == AuthenticationResult.Success || Result == AuthenticationResult.Success_Survey;
		}

		public AuthLogonChallengeResponse()
		{

		}
	}
}
