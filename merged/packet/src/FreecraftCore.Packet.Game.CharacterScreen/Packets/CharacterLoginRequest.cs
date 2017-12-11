using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.API.Common;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.CMSG_PLAYER_LOGIN)]
	[ProtocolGrouping(ProtocolCode.Game)] //TODO: Change this protocol to something more specific
	public class CharacterLoginRequest : GamePacketPayload
	{
		/// <inheritdoc />
		public override bool isValid { get; } = true; //TODO: When rules/data validation engine is written implement

		[WireMember(1)]
		public ObjectGuid CharacterGuid { get; private set; }

		public CharacterLoginRequest([NotNull] ObjectGuid characterGuid)
		{
			if (characterGuid == null) throw new ArgumentNullException(nameof(characterGuid));

			CharacterGuid = characterGuid;
		}

		protected CharacterLoginRequest()
		{
			//serializer ctor
		}
	}
}
