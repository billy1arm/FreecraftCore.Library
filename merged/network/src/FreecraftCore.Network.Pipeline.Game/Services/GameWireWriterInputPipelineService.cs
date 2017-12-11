using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Type alia for the generic <see cref="WireWriterInputPipelineService{TMessageType,TNetworkOperationCodeType,THeaderType,TPayloadType}"/>
	/// used for game messages.
	/// </summary>
	public class GameWireWriterInputPipelineService : WireWriterInputPipelineService<GamePacketPayload, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>
	{

	}
}
