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
	[GamePayloadOperationCode(NetworkOperationCode.CMSG_TEXT_EMOTE)]
	[ProtocolGrouping(ProtocolCode.Game)] //TODO: Change this protocol to something more specific
	public class EmoteChatMessageRequest : GamePacketPayload
	{
		/// <inheritdoc />
		public override bool isValid { get; } = true;

		[WireMember(1)]
		public TextEmotes Emote { get; private set; }

		[WireMember(2)]
		public uint UnknownEmoteNumber { get; private set; }

		[WireMember(3)]
		public ObjectGuid EmoteTarget { get; private set; }

		public EmoteChatMessageRequest(TextEmotes emote, uint emoteNumber, ObjectGuid guid)
		{
			if (!Enum.IsDefined(typeof(TextEmotes), emote))
				throw new ArgumentOutOfRangeException(nameof(emote), "Value should be defined in the TextEmotes enum.");

			UnknownEmoteNumber = emoteNumber;
			Emote = emote;
			EmoteTarget = guid;
		}

		protected EmoteChatMessageRequest()
		{
			//serializer ctor
		}
	}
}
