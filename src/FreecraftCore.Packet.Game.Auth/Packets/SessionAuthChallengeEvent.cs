using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// First message sent by the server after connections (as an event)
	/// and is used to authenticate a session on that server.
	/// </summary>
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.SMSG_AUTH_CHALLENGE)] //the packet the worldserver first sends
	public class SessionAuthChallengeEvent : GamePayload
	{
		/// <inheritdoc />
		public override bool isValid => SeedOne != null &&
			SeedTwo != null && SeedOne.Length == 16 && SeedOne.Length == 16;

		//Trinitycore always sends 1
		//Not sure what this is
		//It is not sent in 1.12.1 Mangos
		[WireMember(1)]
		private uint unknownOne { get; set; }

		//Trinitycore initializes this field as rand32()
		//0 between UINT32 max
		/// <summary>
		/// Random seed sent by the server.
		/// </summary>
		[WireMember(2)]
		public uint AuthenticationSeed { get; private set; }

		/// <summary>
		/// A 16 byte non-cryptographically secure BigInteger.
		/// It is not stored on Trinitycore or Mangos, isn't sent by EmberEmu
		/// and isn't used by Jazkpoz's bot.
		/// </summary>
		[KnownSize(16)] //jackpoz shows this is a 16 byte BigInt
		[WireMember(3)]
		private byte[] SeedOne { get; set; }

		/// <summary>
		/// A 16 byte non-cryptographically secure BigInteger.
		/// It is not stored on Trinitycore or Mangos, isn't sent by EmberEmu
		/// and isn't used by Jazkpoz's bot.
		/// </summary>
		[KnownSize(16)] //jackpoz shows this is a 16 byte BigInt
		[WireMember(4)]
		private byte[] SeedTwo { get; set; }

		public SessionAuthChallengeEvent()
		{
			//TODO: If we ever make a server we'll need a real ctor for this packet.
		}
	}
}
