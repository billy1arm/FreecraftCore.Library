using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FreecraftCore.API.Common;


namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Initial Authentication payload
	/// </summary>
	[WireDataContract]
	[AuthenticationPayload(AuthOperationCode.AUTH_LOGON_CHALLENGE, AuthOperationDestinationCode.Server)] //TODO: Figure out how to support linking with the limited information.
	//[AuthenticationPayload(AuthOperationCode.AUTH_RECONNECT_CHALLENGE)]
	public class AuthLogonChallengeRequest : AuthenticationPayload
	{
		/// <inheritdoc />
		public override bool isValid => Protocol.VerifyIsDefined() && Game.VerifyIsDefined() && Expansion.VerifyIsDefined();

		/// <summary>
		/// Authentication protocol version to use.
		/// Trinitycore/Mangos has this marked as an error but Ember https://github.com/EmberEmu/Ember/blob/spark-new/src/login/grunt/client/LoginChallenge.h has this
		/// marked as the protocol field.
		/// </summary>
		[WireMember(1)]
		public ProtocolVersion Protocol { get; private set; }

		//We don't need to expose this really. Shouldn't need to be checked. This isn't C++
		/// <summary>
		/// Packet size. Computed by Trinitycore as the size of the payload + the username size.
		/// </summary>
		[WireMember(2)]
		private readonly ushort size;

		//Could be reversed?
		/// <summary>
		/// Game the client is for.
		/// </summary>
		[EnumString]
		[KnownSize(3)]
		[WireMember(3)]
		public GameType Game { get; private set; }

		/// <summary>
		/// Indicates the expansion this client is authenticating for.
		/// </summary>
		[WireMember(4)]
		public ExpansionType Expansion { get; private set; }

		/// <summary>
		/// Indicates the major patch version (Ex. x.3.x)
		/// </summary>
		[WireMember(5)]
		public byte MajorPatchVersion { get; private set; }

		/// <summary>
		/// Indicates the major patch version (Ex. x.x.5)
		/// </summary>
		[WireMember(6)]
		public byte MinorPatchVersion { get; private set; }

		//TODO: Enumerate this maybe?
		[WireMember(7)]
		public ClientBuild Build { get; private set; }

		/// <summary>
		/// Indicates the platform/arc (Ex. 32bit or 64bit)
		/// </summary>
		[EnumString]
		[ReverseData]
		[KnownSize(3)]
		[WireMember(8)]
		public PlatformType Platform { get; private set; }

		/// <summary>
		/// Indicates the operating system the client is running on (Ex. Win or Mac)
		/// </summary>
		[EnumString]
		[ReverseData]
		[KnownSize(3)]
		[WireMember(9)]
		public OperatingSystemType OperatingSystem { get; private set; }

		/// <summary>
		/// Indicates the Locale of the client. (Ex. En-US)
		/// </summary>
		[EnumString]
		[ReverseData]
		[DontTerminate] //Locale also doesn't terminate. It is a char[4] like "SUne" without a terminator.
		[KnownSize(4)]
		[WireMember(10)]
		public LocaleType Locale { get; private set; }

		//TODO: Timezone bias? Investigate values.
		[WireMember(11)]
		private uint TimeZoneBias { get; set; }

		[KnownSize(4)]
		[WireMember(12)]
		private readonly byte[] ipAddressInBytes;

		//Lazily cached Ip built from wired bytes
		private Lazy<IPAddress> cachedIp { get; }

		//TODO: Thread safety
		/// <summary>
		/// IP Address of the client.
		/// </summary>
		public IPAddress IP => cachedIp.Value;

		/// <summary>
		/// Could be Username or maybe Email.
		/// </summary>
		//TODO: Check Mangos if they look for a null terminator on Identity
		[DontTerminate] //JackPoz doesn't terminate and it looks like Trinitycore doesn't really expect a null terminator either.
		[SendSize(SendSizeAttribute.SizeType.Byte)]
		[WireMember(13)]
		public string Identity { get; private set; }

		public AuthLogonChallengeRequest(ProtocolVersion protocol, GameType game, ExpansionType expansion, byte majorPatch, byte minorPatch, ClientBuild build, PlatformType platform, OperatingSystemType operatingSystem, LocaleType locale, IPAddress clientIp, string identity)
			: this()
		{
			//TODO: Very long ctor. Maybe use builder in the future.
			Protocol = protocol;
			Game = game;
			Expansion = expansion;
			MajorPatchVersion = majorPatch;
			MinorPatchVersion = minorPatch;
			Build = build;
			Platform = platform;
			OperatingSystem = operatingSystem;
			Locale = locale;

			//Convert IP to bytes
			//TODO: Check size
			ipAddressInBytes = clientIp.GetAddressBytes(); //Trinitycore expects an int32 but an array of bytes should work

			Identity = identity;

			//Now we can compute size. Jackpoz does this with a literal. Trinitycore uses constants. I think we'll just use a literal though.
			size = (ushort)(identity.Length + 30);
		}

		public AuthLogonChallengeRequest()
		{
			//Use a Lazy IPAddress to create the IP from the bytes coming across the network
			cachedIp = new Lazy<IPAddress>(() => new IPAddress(ipAddressInBytes), true);
		}
	}
}

/*typedef struct AUTH_LOGON_CHALLENGE_C
{
	uint8   cmd;
	uint8   error;
	uint16  size;
	uint8   gamename[4];
	uint8   version1;
	uint8   version2;
	uint8   version3;
	uint16  build;
	uint8   platform[4];
	uint8   os[4];
	uint8   country[4];
	uint32  timezone_bias;
	uint32  ip;
	uint8   I_len;
	uint8   I[1];
} sAuthLogonChallenge_C;*/
