using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	[Flags]
	public enum NetworkMessageContextState
	{
		/// <summary>
		/// Indicates that a message is unhandled.
		/// </summary>
		Unhandled = 1 << 0,

		/// <summary>
		/// Indicates that the message is invalid.
		/// </summary>
		Invalid = 1 << 1,

		/// <summary>
		/// Indicates that the message is in a valid state.
		/// </summary>
		Valid = 1 << 2,

		/// <summary>
		/// Indicates that the message has been handled.
		/// </summary>
		Handled = 1 << 3
	}
}
