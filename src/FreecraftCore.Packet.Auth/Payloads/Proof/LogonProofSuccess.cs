using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.API.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Sent by the server when a logon proof request was successful.
	/// Only for >= 2.x.x clients. 1.12.1 clients recieve something slightly different.
	/// </summary>
	[WireDataContract]
	public class LogonProofSuccess : ILogonProofResult
	{
		/// <summary>
		/// Indicates that the result of the logon attempt was successful.
		/// </summary>
		public AuthenticationResult Result { get; } = AuthenticationResult.Success;

		/// <summary>
		/// SRP6 M2. See http://srp.stanford.edu/design.html for more information.
		/// (M2 = H(A, M (computed by client), K) where K is H(S) and S is session key. M2 proves server computed same K and recieved M1/M
		/// </summary>
		[WireMember(1)]
		[KnownSize(20)]
		public byte[] M2 { get; private set; }

		//TODO: Accountflags? Trinitycore says this is:  0x01 = GM, 0x08 = Trial, 0x00800000 = Pro pass (arena tournament) but they always send "Pro Pass"?
		/// <summary>
		/// Indicates the authorization the client was granted.
		/// </summary>
		[WireMember(2)] //sent as a uint32
		public AccountAuthorizationFlags AccountAuthorization { get; private set; }

		//TODO: What is survey ID? Always 0 on Trinitycore. Check mangos and EmberEmu
		[WireMember(3)]
		private readonly uint surveyId = 0;

		//TODO: What is this? Always 0 from Trinitycore.
		[WireMember(4)]
		private readonly ushort unk3 = 0;

		//TODO: Proper Ctor. Right now we only implement client stuff. Server sends this.

		public LogonProofSuccess()
		{

		}
	}

	/*uint8   M2[20];
	uint32  AccountFlags;
	uint32  SurveyId;
	uint16  unk3;*/
}
