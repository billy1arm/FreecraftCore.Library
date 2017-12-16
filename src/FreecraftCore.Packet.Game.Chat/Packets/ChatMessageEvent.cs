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
	[GamePayloadOperationCode(NetworkOperationCode.SMSG_MESSAGECHAT)]
	[ProtocolGrouping(ProtocolCode.Game)] //TODO: Change this protocol to something more specific
	public class ChatMessageEvent : GamePacketPayload
	{
		/// <inheritdoc />
		public override bool isValid { get; }

		/// <summary>
		/// The chat message.
		/// </summary>
		[WireMember(1)]
		public NetworkChatMessage Message { get; private set; }

		protected ChatMessageEvent()
		{
			//serializer ctor
		}
	}
}
