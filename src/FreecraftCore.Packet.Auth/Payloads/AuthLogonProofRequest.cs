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
		//TODO: Add BigInter serialization to our "JAM"
		/// <summary>
		/// SRP A. One of the public keys. Created by the client.
		/// See http://srp.stanford.edu/design.html for more information.
		/// </summary>
		[KnownSize(32)]
		[WireMember(1)]
		public byte[] A { get; private set; }

		/// <summary>
		/// SRP6 M. Essentially a hash of the provided token data and SRP generated data on the client.
		/// (M = H(H(N) xor H(g), H(I), salt, A, B, H(S)) where S is session key.
		/// </summary>
		[KnownSize(20)]
		[WireMember(2)]
		public byte[] TokenHash { get; private set; }

		//TODO: Implement clientside CRC hash response. EmberEmu extracts 4 bytes and calls it SurveyID?
		//This is not a CRC. http://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-emulator-servers/wow-emu-questions-requests/236273-auth-proof-crc-hash.html
		//Says that it is a SHA1(A, HMAC(client_files)) meaning SRP6 A public component hashed with an HMAC of WoW.ex, Divx and Unicows (maybe).
		/// <summary>
		/// Supposedly a CRC hash using the salt provided in <see cref="AuthLogonChallengeResponse"/> involving some client files.
		/// Most servers don't implement this.
		/// </summary>
		[KnownSize(20)]
		[WireMember(2)]
		public byte[] ClientCrcHash { get; private set; }

		//Documentation says it's never used
		//TODO: Find out what this is. Trinitycore doesn't seem to reference it. Neither does Mangos.
		[WireMember(3)]
		private readonly byte KeyCount = 0;

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
