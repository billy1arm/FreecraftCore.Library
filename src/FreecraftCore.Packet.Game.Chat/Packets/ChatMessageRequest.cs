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
	[GamePayloadOperationCode(NetworkOperationCode.CMSG_MESSAGECHAT)]
	[ProtocolGrouping(ProtocolCode.Game)] //TODO: Change this protocol to something more specific
	public class ChatMessageRequest : GamePacketPayload
	{
		/// <inheritdoc />
		public override bool isValid { get; }

		/// <summary>
		/// The chat message.
		/// </summary>
		[WireMember(1)]
		public PlayerChatMessage Message { get; private set; }

		public ChatMessageRequest([NotNull] PlayerChatMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			Message = message;
		}

		protected ChatMessageRequest()
		{
			
		}
	}
}
