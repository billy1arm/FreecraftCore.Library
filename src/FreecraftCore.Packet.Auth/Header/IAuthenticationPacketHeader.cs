using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// The header for authentication packets.
	/// </summary>
	public interface IAuthenticationPacketHeader : IMessageVerifyable, IOperationIdentifable<AuthOperationCode>
	{

	}
}
