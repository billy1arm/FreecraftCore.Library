using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Indicates the authorization the account has been granted.
	/// (GM/Arena Tourny/Trial and etc)
	/// </summary>
	public enum AccountAuthorizationFlags : uint //uint32 AccountFlags; (kind of large for no reason)
	{
		//0x01 = GM, 0x08 = Trial, 0x00800000 = Pro pass (arena tournament)
		None = 0,

		/// <summary>
		/// GM?
		/// </summary>
		GM = 0x01,

		/// <summary>
		/// Has limited trial access.
		/// </summary>
		Trial = 0x08,

		/// <summary>
		/// Arena Tournament granted access.
		/// </summary>
		ProPass = 0x00800000
	}
}
