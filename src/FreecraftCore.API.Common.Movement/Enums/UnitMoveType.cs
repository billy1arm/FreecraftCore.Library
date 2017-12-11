using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of all the motion types a unit can be in.
	/// </summary>
	[WireDataContract]
	public enum UnitMoveType : int //afaik this it not sent over the network. Therefore int for dictionary efficiency is best
	{
		MOVE_WALK = 0,
		MOVE_RUN = 1,
		MOVE_RUN_BACK = 2,
		MOVE_SWIM = 3,
		MOVE_SWIM_BACK = 4,
		MOVE_TURN_RATE = 5,
		MOVE_FLIGHT = 6,
		MOVE_FLIGHT_BACK = 7,
		MOVE_PITCH_RATE = 8
	}

//On 1.12.1:
//enum UnitMoveType
//{
//	MOVE_WALK           = 0,
//	MOVE_RUN            = 1,
//	MOVE_RUN_BACK       = 2,
//	MOVE_SWIM           = 3,
//	MOVE_SWIM_BACK      = 4,
//	MOVE_TURN_RATE      = 5
//}
}
