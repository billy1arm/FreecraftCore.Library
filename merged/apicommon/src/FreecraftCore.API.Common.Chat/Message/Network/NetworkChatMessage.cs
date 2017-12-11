using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	[DefaultChild(typeof(UnhandledChatTypeMessage))]
	[WireDataContractBaseType((int)ChatMessageType.CHAT_MSG_GUILD, typeof(DefaultNetworkChatMessage))] //for guild chat messages
	[WireDataContractBaseType((int)ChatMessageType.CHAT_MSG_WHISPER, typeof(DefaultNetworkChatMessage))] //for player whispers
	[WireDataContract(WireDataContractAttribute.KeyType.Byte, InformationHandlingFlags.DontConsumeRead | InformationHandlingFlags.DontWrite)]
	public abstract class NetworkChatMessage
	{
		/// <summary>
		/// Indicates the message type of the chat message.
		/// </summary>
		[WireMember(1)]
		public ChatMessageType MessageType { get; }

		/// <summary>
		/// Indicates the language of the chat message.
		/// </summary>
		[WireMember(2)]
		public ChatLanguage Language { get; }

		/// <summary>
		/// The <see cref="ObjectGuid"/> of the entity that sent the message.
		/// </summary>
		[WireMember(3)]
		public ObjectGuid SenderGuid { get; }

		//TODO: Find out what this is
		[WireMember(4)]
		private int UnknownOne { get; }

		protected NetworkChatMessage()
		{
			//serializer ctor
		}
	}
}
