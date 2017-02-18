using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Packet.Auth
{
	public interface IAuthenticationPacket<TPacketPayloadType>
		where TPacketPayloadType : IAuthenticationPayload
	{
		[NotNull]
		IAuthenticationPacketHeader AuthPacketHeader { get; }

		[NotNull]
		TPacketPayloadType AuthPayload { get; }
	}
}
