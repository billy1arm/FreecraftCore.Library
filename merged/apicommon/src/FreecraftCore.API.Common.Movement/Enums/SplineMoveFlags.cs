using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	//Same on 1.12.1 I think; see https://github.com/mangoszero/server/blob/d0a4af096e528533044e7029446d1c5c9e98a16c/src/game/movement/spline.h
	/// <summary>
	/// Flags enumeration of all spline movement flags.
	/// Based on Trinitycore's MoveSpline.h MoveSplineFlag eFlags enum.
	/// </summary>
	[WireDataContract]
	public enum SplineMoveFlags : uint
	{
		/// <summary>
		/// Indicates no flags.
		/// </summary>
		None = 0x00000000,

		// x00-xFF(first byte) used as animation Ids storage in pair with Animation flag
		Done = 0x00000100,

		/// <summary>
		/// Affects elevation computation, can't be combined with Parabolic flag
		/// </summary>
		Falling = 0x00000200,
		No_Spline = 0x00000400,

		/// <summary>
		/// Affects elevation computation, can't be combined with Falling flag
		/// </summary>
		Parabolic = 0x00000800,
		Walkmode = 0x00001000,

		/// <summary>
		/// Smooth movement(Catmullrom interpolation mode), flying animation
		/// </summary>
		Flying = 0x00002000,

		/// <summary>
		/// Model orientation fixed
		/// </summary>
		OrientationFixed = 0x00004000,
		Final_Point = 0x00008000,
		Final_Target = 0x00010000,
		Final_Angle = 0x00020000,

		/// <summary>
		/// Used Catmullrom interpolation mode
		/// </summary>
		Catmullrom = 0x00040000,

		/// <summary>
		/// Movement by cycled spline
		/// </summary>
		Cyclic = 0x00080000,

		/// <summary>
		/// Everytimes appears with cyclic flag in monster move packet, erases first spline vertex after first cycle done
		/// </summary>
		Enter_Cycle = 0x00100000,

		/// <summary>
		/// Plays animation after some time passed
		/// </summary>
		Animation = 0x00200000,

		/// <summary>
		/// Will never arrive.
		/// </summary>
		Frozen = 0x00400000,
		TransportEnter = 0x00800000,
		TransportExit = 0x01000000,
		Unknown7 = 0x02000000,
		Unknown8 = 0x04000000,
		OrientationInversed = 0x08000000,
		Unknown10 = 0x10000000,
		Unknown11 = 0x20000000,
		Unknown12 = 0x40000000,
		Unknown13 = 0x80000000,

		// Masks
		Mask_Final_Facing = Final_Point | Final_Target | Final_Angle,

		/// <summary>
		/// Animation ids stored here, see AnimType enum, used with Animation flag
		/// </summary>
		Mask_Animations = 0xFF,

		/// <summary>
		///  flags that shouldn't be appended into SMSG_MONSTER_MOVE\SMSG_MONSTER_MOVE_TRANSPORT packet, should be more probably
		/// </summary>
		Mask_No_Monster_Move = Mask_Final_Facing | Mask_Animations | Done,

		/// <summary>
		/// CatmullRom interpolation mode used
		/// </summary>
		Mask_CatmullRom = Flying | Catmullrom,

		// Unused, not suported flags
		Mask_Unused = No_Spline | Enter_Cycle | Frozen | Unknown7 | Unknown8 | Unknown10 | Unknown11 | Unknown12 | Unknown13
	}
}
