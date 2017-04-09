using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public sealed class DefaultPlayerChatMessage : PlayerChatMessage
	{
		/// <summary>
		/// The chat message being sent.
		/// </summary>
		[WireMember(1)]
		public string Message { get; } //null terminated string message

		/// <inheritdoc />
		public DefaultPlayerChatMessage(ChatMessageType messageType, ChatLanguage language, [NotNull] string message) 
			: base(messageType, language)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			Message = message;
		}

		protected DefaultPlayerChatMessage()
		{
			
		}
	}
}
