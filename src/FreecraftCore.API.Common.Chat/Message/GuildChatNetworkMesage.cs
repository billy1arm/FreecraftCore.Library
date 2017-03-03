using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public class GuildChatNetworkMesage : NetworkChatMessage
	{
		/// <summary>
		/// Don't really know what this would be for guild messages.
		/// </summary>
		[WireMember(1)]
		public ObjectGuid RecieverGuid { get; private set; }

		/// <summary>
		/// A null terminated chat string with Length equal to the length of the text
		/// plus one for null terminator.
		/// </summary>
		[WireMember(2)]
		[SendSize(SendSizeAttribute.SizeType.Int32)] //WoW sends the size which includes the null terminator; this is likely done for efficiency
		public string MessageText { get; private set; }

		/// <summary>
		/// Indicates the current chat tag.
		/// (Ex. DND or AFK)
		/// </summary>
		[WireMember(3)]
		public ChatStateTag Tag { get; private set; }

		protected GuildChatNetworkMesage()
		{

		}
	}
}
