using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Packet.Auth
{
	public interface IAuthenticationPacket : INetworkPacket<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
	{

	}
}
