using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Represents a logical grouping of messages. According to JAM GDC talk
	/// a Protocol groups messages together for service routing and for locking policy.
	/// </summary>
	public enum ProtocolCode : byte
	{
		/// <summary>
		/// Indicates no protocol
		/// </summary>
		None = 0,

		/// <summary>
		/// The authentication protocol.
		/// </summary>
		Authentication = 1,

		/// <summary>
		/// Current placeholder protocol for all game packets.
		/// </summary>
		Game = 2
	}
}
