using System;
using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Math;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Enumeration of all potential <see cref="BigInteger"/>
	/// sign values.
	/// </summary>
	public enum BigIntegerSign : int
	{
		/// <summary>
		/// Represents the default sign.
		/// </summary>
		Default = 0,

		/// <summary>
		/// Represents a negative sign value.
		/// </summary>
		Negative = -1,

		/// <summary>
		/// Represents a positive sign value.
		/// </summary>
		Positive = 1,
	}
}
