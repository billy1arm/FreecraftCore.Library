using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.CMSG_CHAR_ENUM)]
	[ProtocolGrouping(ProtocolCode.Game)] //TODO: Change this protocol to something more specific
	public class CharacterListRequest : GamePacketPayload
	{
		/// <inheritdoc />
		public override bool isValid { get; } = true; //TODO: When rules/data validation engine is written implement

		public CharacterListRequest()
		{
			//We don't send any data
		}
	}
}
