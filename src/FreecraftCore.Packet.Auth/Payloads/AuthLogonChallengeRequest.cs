using FreecraftCore.API.Common;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Initial Authentication payload
	/// </summary>
	[WireMessage]
	[AuthenticationPayload(AuthOperationCode.AUTH_LOGON_CHALLENGE)] //metadata marker for the "cmd"
	[AuthenticationPayload(AuthOperationCode.AUTH_RECONNECT_CHALLENGE)]
	public class AuthLogonChallengeRequest : IAuthenticationPayload
	{
		//TODO: This field is the protocol; not an error. https://github.com/EmberEmu/Ember/blob/spark-new/src/login/grunt/client/LoginChallenge.h
		/// <summary>
		/// Unknown byte-sized error field sent, but not checked, by WoW emulation projects such as Mangos/Trinitycore
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
		[WireMember(5)]
		public byte MinorPatchVersion { get; private set; }

		//TODO: Enumerate this maybe?
		[WireMember(6)]
		public ClientBuild Build { get; private set; }

		/// <summary>
		/// Indicates the platform/arc (Ex. 32bit or 64bit)
		/// </summary>
		[EnumString]
		[ReverseData]
		[KnownSize(3)]
		[WireMember(7)]
		public PlatformType Platform { get; private set; }

		/// <summary>
		/// Indicates the operating system the client is running on (Ex. Win or Mac)
		/// </summary>
		[EnumString]
		[ReverseData]
		[KnownSize(3)]
		[WireMember(8)]
		public OperatingSystemType OperatingSystem { get; private set; }

		/// <summary>
		/// Indicates the Locale of the client. (Ex. En-US)
		/// </summary>
		[EnumString]
		[ReverseData]
		[KnownSize(3)]
		[WireMember(9)]
		public LocaleType Locale { get; private set; }

		//TODO: Timezone bias? Investigate values.
		[WireMember(10)]
		private int TimeZoneBias { get; set; }

		[KnownSize(4)]
		[WireMember(11)]
		private readonly byte[] ipAddressInBytes;

		//cached IP built from wired bytes
		private IPAddress cachedIp;

		//TODO: Thread safety
		/// <summary>
		/// IP Address of the client.
		/// </summary>
		public IPAddress IP { get { return cachedIp == null ? cachedIp = new IPAddress(ipAddressInBytes) : cachedIp; } }

		/// <summary>
		/// Could be Username or maybe Email.
		/// </summary>
		[SendSize(SendSizeAttribute.SizeType.Byte)]
		[WireMember(12)]
		public string Identity { get; private set; }

		public AuthLogonChallengeRequest(ProtocolVersion protocol, GameType game, ExpansionType expansion, byte majorPatch, byte minorPatch, ClientBuild build, PlatformType platform, OperatingSystemType operatingSystem, LocaleType locale, IPAddress clientIp, string identity)
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
