using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Service for handling game network inputs utilizing the pipeline system.
	/// (basically an alias for now for the ugly complex generic type <see cref="NetworkInputPipelineService{TContextBuilderType,TNetworkOperationCodeType,THeaderType,TPayloadType}"/>
	/// </summary>
	public class GameWireReaderInputPipelineService : WireReaderInputPipelineService<DefaultNetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>
	{
		/// <inheritdoc />
		public GameWireReaderInputPipelineService([NotNull] INetworkMessageContextBuilderFactory<DefaultNetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> contextBuilderFactory) 
			: base(contextBuilderFactory)
		{

		}
	}
}
