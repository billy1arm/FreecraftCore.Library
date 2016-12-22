using FreecraftCore.API.Common;
using FreecraftCore.API.Common.Auth;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// The response payload to a <see cref="AuthLogonChallengeRequest"/>.
	/// </summary>
	[WireDataContract]
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

		//Not a salt or related to a CRC http://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-emulator-servers/wow-emu-questions-requests/236273-auth-proof-crc-hash.html
		//Ownedcore post indicates that this is the seed used for HMAC generation for client file verification.
		//Process described as SHA1(A, HMAC(client_files))
		/// <summary>
		/// Supposedly is a CRC salt that is used to compute a CRC hash for client files.
		/// (this is usually referred to as unk3 in in most resources)
		/// </summary>
		[KnownSize(16)]
		[WireMember(4)]
		public byte[] CRCSalt { get; private set; }

		//TODO: Create enum
		/// <summary>
		/// Flags that indicate what security additional measures are required for authentication
		/// (Ex. Authenticator or Pin) Will usually be 0 meaning none
		/// </summary>
		[WireMember(5)]
		public byte securityFlags { get; private set; }

		//The server is suppose to send additional info depending on the flags
		//However, the serializer isn't capable of conditionally reading different sizes based on flags
		//It's all just 0s or constants anyway in Trinitycore

		//If this is true then it will contain token data
		public bool isSuccess()
		{
			return Result == AuthenticationResult.Success || Result == AuthenticationResult.Success_Survey;
		}

		//TODO: Proper ctor; right now we don't have a server so we can get away with just default

		public AuthLogonChallengeResponse()
		{

		}
	}
}
