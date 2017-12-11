using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of all player character races.
	/// </summary>
	[WireDataContract]
	public enum CharacterRace : byte
	{
		Human = 1,
		Orc = 2,
		Dwarf = 3,
		Nightelf = 4,
		Undead = 5,
		Tauren = 6,
		Gnome = 7,
		Troll = 8,
		Goblin = 9,
		Bloodelf = 10,
		Draenei = 11,
	}
}
