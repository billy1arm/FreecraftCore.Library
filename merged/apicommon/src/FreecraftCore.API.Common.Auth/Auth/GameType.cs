using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of all games that authenticate with the WoW protocol
	/// </summary>
	public enum GameType : byte //uint8 gamename[4]; (Enum string)
	{
		//TODO: Find other names?

		/// <summary>
		/// World of Warcraft
		/// </summary>
		WoW = 1,
	}
}
