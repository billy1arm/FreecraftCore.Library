using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of all valid platforms.
	/// </summary>
	public enum PlatformType : byte //uint8 platform[4]; (Enum string)
	{
		/// <summary>
		/// 32bit platform.
		/// </summary>
		x86 = 1,

		/// <summary>
		/// 64bit platform.
		/// </summary>
		x64 = 2,
	}
}
