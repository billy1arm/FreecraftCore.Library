using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	public interface IAuthenticationPacket
	{
		[NotNull]
		IAuthenticationPacketHeader AuthPacketHeader { get; }

		[NotNull]
		IAuthenticationPayload AuthPayload { get; }
	}
}
