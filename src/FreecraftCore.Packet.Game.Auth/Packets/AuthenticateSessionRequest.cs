using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FreecraftCore.API.Common;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.CMSG_AUTH_SESSION)]
	public class SessionAuthProofRequest : GamePacketPayload
	{
		public override bool isValid => SessionDigest != null && SessionDigest.Length == 20
			&& String.IsNullOrEmpty(AccountName) && RandomSeedBytes != null &&
			RandomSeedBytes.Length == 4 && RealmIdentity != null;

		/// <summary>
		/// The build number of the client.
		/// </summary>
		[WireMember(1)]
		public ClientBuild ClientBuildNumber { get; private set; }

		//For some reason Trinitycore expects a 4 byte clientbuild number
		//But clientbuild number is only a short, on the authentication process,
		//so it's likely these are unknown bytes
		[WireMember(2)]
		private short unknownOne { get; set; } = 0;

		//Not checked on Trinitycore
		//Was probably used for loadbalancing so it knows
		//which server to ask for the session key.
		[WireMember(3)]
		private int LoginServiceId { get; set; } = 0;

		/// <summary>
		/// The account name attempting to authentication their session.
		/// </summary>
		[WireMember(4)]
		public string AccountName { get; private set; } //is a null terminated string

		//Not check on trinitycore
		//Don't know what the type of login server would mean.
		[WireMember(5)]
		private uint LoginServerType { get; set; } = 0; // 0 GRUNT, 1 Battle.net

		[NotNull]
		[KnownSize(4)]
		[WireMember(6)]
		public byte[] RandomSeedBytes { get; private set; }

		[NotNull]
		[WireMember(7)]
		public RealmIdentification RealmIdentity { get; private set; }

		//Don't know what this is
		//Trinitycore doesn't use it
		[WireMember(8)]
		private ulong DosResponse { get; set; }

		[NotNull]
		[KnownSize(20)]
		[WireMember(9)]
		public byte[] SessionDigest { get; private set; }

		//Trintycore will crash if you don't send this
		//Though it'll probably be fixed soon
		//TODO: Add uint strategy
		[WireMember(10)]
		[SendSize(SendSizeAttribute.SizeType.Int32)] //it's probably uint but we don't have that yet
		private byte[] AddonData { get; set; } = new byte[0];
		

		public SessionAuthProofRequest(ClientBuild clientBuildNumber, [NotNull] string accountName, [NotNull] byte[] randomSeedBytes,
			[NotNull] RealmIdentification realmIdentity, [NotNull] byte[] sessionDigest)
		{
			if (!Enum.IsDefined(typeof(ClientBuild), clientBuildNumber))
				throw new InvalidEnumArgumentException(nameof(clientBuildNumber), (int)clientBuildNumber, typeof(ClientBuild));

			if (randomSeedBytes == null) throw new ArgumentNullException(nameof(randomSeedBytes));
			if (realmIdentity == null) throw new ArgumentNullException(nameof(realmIdentity));
			if (sessionDigest == null) throw new ArgumentNullException(nameof(sessionDigest));

			if (string.IsNullOrEmpty(accountName))
				throw new ArgumentException("Value cannot be null or empty.", nameof(accountName));

			ClientBuildNumber = clientBuildNumber;
			AccountName = accountName;
			RandomSeedBytes = randomSeedBytes;
			RealmIdentity = realmIdentity;
			SessionDigest = sessionDigest;
		}

		protected SessionAuthProofRequest()
		{
			
		}
	}
}
