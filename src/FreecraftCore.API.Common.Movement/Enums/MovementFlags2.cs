using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of flags for secondary movement information.
	/// Defined in Trinitycore's Unit.h
	/// </summary>
	[Flags]
	[WireDataContract]
	public enum MovementFlags2 : ushort //a ushort according to jackpoz's bot
	{
		MOVEMENTFLAG2_NONE = 0x00000000,
		MOVEMENTFLAG2_NO_STRAFE = 0x00000001,
		MOVEMENTFLAG2_NO_JUMPING = 0x00000002,
		MOVEMENTFLAG2_UNK3 = 0x00000004,        // Overrides various clientside checks
		MOVEMENTFLAG2_FULL_SPEED_TURNING = 0x00000008,
		MOVEMENTFLAG2_FULL_SPEED_PITCHING = 0x00000010,
		MOVEMENTFLAG2_ALWAYS_ALLOW_PITCHING = 0x00000020,
		MOVEMENTFLAG2_UNK7 = 0x00000040,
		MOVEMENTFLAG2_UNK8 = 0x00000080,
		MOVEMENTFLAG2_UNK9 = 0x00000100,
		MOVEMENTFLAG2_UNK10 = 0x00000200,
		MOVEMENTFLAG2_INTERPOLATED_MOVEMENT = 0x00000400,
		MOVEMENTFLAG2_INTERPOLATED_TURNING = 0x00000800,
		MOVEMENTFLAG2_INTERPOLATED_PITCHING = 0x00001000,
		MOVEMENTFLAG2_UNK14 = 0x00002000,
		MOVEMENTFLAG2_UNK15 = 0x00004000,
		MOVEMENTFLAG2_UNK16 = 0x00008000
	};
}
