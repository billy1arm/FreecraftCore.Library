using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.API.Common
{
	//Based on PlayerChatTag in Player.h in Trinitycore.
	/// <summary>
	/// Enumeration of all the chat tag states.
	/// </summary>
	public enum ChatStateTag : byte
	{
		CHAT_TAG_NONE = 0x00,
		CHAT_TAG_AFK = 0x01,
		CHAT_TAG_DND = 0x02,
		CHAT_TAG_GM = 0x04,
		CHAT_TAG_COM = 0x08, // Commentator
		CHAT_TAG_DEV = 0x10
	}
}
