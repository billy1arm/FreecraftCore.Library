using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Interface metadata market for game payloads.
	/// </summary>
	public interface IGamePacketPayload : IMessageVerifyable, IProtocolGroupable, IOperationCodeProvidable<NetworkOperationCode>
	{

	}
}
