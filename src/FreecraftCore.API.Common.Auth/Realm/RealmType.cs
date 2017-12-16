using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of valid realm types.
	/// </summary>
	public enum RealmType : byte //Ember has it as an uint32 but Trinity uses a byte for sure
	{
		/// <summary>
		/// PvE or Normal server.
		/// </summary>
		PvE,

		/// <summary>
		/// PvP server.
		/// </summary>
		PvP,

		/// <summary>
		/// RP server.
		/// </summary>
		RP = 6,

		/// <summary>
		/// A PvP RP server.
		/// </summary>
		RPPvP = PvP | RP
	}
}
