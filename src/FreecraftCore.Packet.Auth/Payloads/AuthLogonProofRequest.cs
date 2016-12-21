using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.Packet.Auth
{
	[WireMessage]
	[AuthenticationPayload(AuthOperationCode.AUTH_LOGON_PROOF)]
	public class AuthLogonProofRequest : IAuthenticationPayload
	{
		/// <summary>
		/// SRP A. One of the public keys. Created by the client.
		/// See http://srp.stanford.edu/design.html for more information.
		/// </summary>
		[KnownSize(32)]
		[WireMember(1)]
		public byte[] A { get; private set; }

		/// <summary>
		/// A hash of all the token data such as: Salt, Public key components, username hash and etc
		/// This may not be standard SRP6.
		/// </summary>
		[KnownSize(20)]
		[WireMember(2)]
		public byte[] TokenHash { get; private set; }

		//TODO: Implement clientside CRC hash response. EmberEmu extracts 4 bytes and calls it SurveyID?
		/// <summary>
		/// Supposedly a CRC hash using the salt provided in <see cref="AuthLogonChallengeResponse"/> involving some client files.
		/// Most servers don't implement this.
		/// </summary>
		[KnownSize(20)]
		[WireMember(2)]
		public byte[] ClientCrcHash { get; private set; }

		//TODO: Find out what this is. Trinitycore doesn't seem to reference it. Neither does Mangos.
		[WireMember(3)]
		public byte KeyCount { get; private set; }

		//No server implements tokens so security flags are always 0
		[WireMember(4)]
		private readonly byte securityFlags = 0;

		public AuthLogonProofRequest()
		{

		}
	}
}

/*typedef struct AUTH_LOGON_PROOF_C
{
	uint8   cmd;
	uint8   A[32];
	uint8   M1[20];
	uint8   crc_hash[20];
	uint8   number_of_keys;
	uint8   securityFlags;
} sAuthLogonProof_C;
*/
