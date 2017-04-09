using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public sealed class WhisperPlayerChatMessage : PlayerChatMessage
	{
		/// <summary>
		/// The targeted reciever of the message.
		/// </summary>
		[WireMember(1)]
		public string Target { get; } //null terminated string reciever

		/// <summary>
		/// The chat message being sent.
		/// </summary>
		[WireMember(2)]
		public string Message { get; } //null terminated string message

		/// <inheritdoc />
		public WhisperPlayerChatMessage(ChatLanguage language, [NotNull] string message, [NotNull] string target) 
			: base(ChatMessageType.CHAT_MSG_WHISPER, language)
		{
			if (target == null)
				throw new ArgumentNullException(nameof(target));
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			Target = target;
			Message = message;
		}

		protected WhisperPlayerChatMessage()
		{
			
		}
	}
}
