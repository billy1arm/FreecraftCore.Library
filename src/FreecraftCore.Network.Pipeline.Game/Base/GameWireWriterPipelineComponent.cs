using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Simplified type alias for a <see cref="WireReaderInputPipelineService{TContextBuilderType,TNetworkOperationCodeType,THeaderType,TPayloadType}"/> for game
	/// network stream inputes.
	/// </summary>
	public abstract class GameWireWriterPipelineComponent : WireWriterPipelineComponent<GamePacketPayload, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>
	{

	}
}
