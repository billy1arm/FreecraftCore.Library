using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.API.Common;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.SMSG_CHAR_ENUM)]
	[ProtocolGrouping(ProtocolCode.Game)] //TODO: Change this protocol to something more specific
	public class CharacterListResponse : GamePacketPayload
	{
		/// <inheritdoc />
		public override bool isValid { get; } = true; //TODO: Implement data validation

		[WireMember(1)]
		[SendSize(SendSizeAttribute.SizeType.Byte)] //Jackpoz's bot shows it sends count as byte
		public CharacterScreenCharacter[] Characters { get; private set; }

		protected CharacterListResponse()
		{
			//serialization ctor
		}
	}
}
