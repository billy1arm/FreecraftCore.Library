using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public class DefaultNetworkChatMessage : NetworkChatMessage
	{
		/// <summary>
		/// Don't really know what this would be for most messages.
		/// </summary>
		[WireMember(1)]
		public ObjectGuid RecieverGuid { get; }

		/// <summary>
		/// A null terminated chat string with Length equal to the length of the text
		/// plus one for null terminator.
		/// </summary>
		[WireMember(2)]
		[SendSize(SendSizeAttribute.SizeType.Int32)] //WoW sends the size which includes the null terminator; this is likely done for efficiency
		public string MessageText { get; }

		/// <summary>
		/// Indicates the current chat tag.
		/// (Ex. DND or AFK)
		/// </summary>
		[WireMember(3)]
		public ChatStateTag Tag { get; }

		protected DefaultNetworkChatMessage()
		{

		}
	}
}
