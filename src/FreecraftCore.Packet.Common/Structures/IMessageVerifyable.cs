using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Packet
{	
	/// <summary>
	/// Contract for message Types that can be in a valid or invalid state.
	/// </summary>
	public interface IMessageVerifyable
	{
		/// <summary>
		/// Indicates if the message is valid.
		/// </summary>
		bool isValid { get; }
	}
}
