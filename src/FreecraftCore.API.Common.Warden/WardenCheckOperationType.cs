using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common.Warden
{
	//From Trinitycore Warden.h
	/// <summary>
	/// Enumeration of all potential Warden check operations.
	/// </summary>
	[WireDataContract]
	public enum WardenCheckOperationType : byte
	{
		/// <summary>
		/// 243: byte moduleNameIndex + uint Offset + byte Len (check to ensure memory isn't modified)
		/// </summary>
		MEM_CHECK = 0xF3,

		/// <summary>
		/// 178: uint Seed + byte[20] SHA1 + uint Addr + byte Len (scans all pages for specified hash)
		/// </summary>
		PAGE_CHECK_A = 0xB2,

		/// <summary>
		/// 191: uint Seed + byte[20] SHA1 + uint Addr + byte Len (scans only pages starts with MZ+PE headers for specified hash)
		/// </summary>
		PAGE_CHECK_B = 0xBF,

		/// <summary>
		/// 152: byte fileNameIndex (check to ensure MPQ file isn't modified)
		/// </summary>
		MPQ_CHECK = 0x98,

		/// <summary>
		/// 139: byte luaNameIndex (check to ensure LUA string isn't used)
		/// </summary>
		LUA_STR_CHECK = 0x8B,

		/// <summary>
		/// 113: uint Seed + byte[20] SHA1 + byte driverNameIndex (check to ensure driver isn't loaded)
		/// </summary>
		DRIVER_CHECK = 0x71,

		/// <summary>
		/// 87: empty (check to ensure GetTickCount() isn't detoured)
		/// </summary>
		TIMING_CHECK = 0x57,

		/// <summary>
		/// 126: uint Seed + byte[20] SHA1 + byte moluleNameIndex + byte procNameIndex + uint Offset + byte Len (check to ensure proc isn't detoured)
		/// </summary>
		PROC_CHECK = 0x7E,

		/// <summary>
		/// 217: uint Seed + byte[20] SHA1 (check to ensure module isn't injected)
		/// </summary>
		MODULE_CHECK = 0xD9
	}
}
