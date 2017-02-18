using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// The header for authentication packets.
	/// </summary>
	public interface IAuthenticationPacketHeader
	{
		/// <summary>
		/// Indicates the operation code for the authentication packet.
		/// </summary>
		AuthOperationCode AuthenticationOpCode { get; }
	}
}
