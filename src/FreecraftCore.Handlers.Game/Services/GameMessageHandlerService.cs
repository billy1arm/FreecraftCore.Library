using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Type alias for a closed generic version of <see cref="MessageHandlerService{TMessageType,TOperationCode,THeaderType,TPayloadType}"/>
	/// </summary>
	public class GameMessageHandlerService : MessageHandlerService<GameNetworkMessageContext, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>
	{
		//Right now this is merely a type alias for the generic message handler service
	}
}
