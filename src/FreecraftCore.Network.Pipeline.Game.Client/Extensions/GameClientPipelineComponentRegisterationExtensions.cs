using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Crypto;
using FreecraftCore.Network;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore
{
	public static class GameClientPipelineComponentRegisterationExtensions
	{
		/// <summary>
		/// Registers a full packet writing component cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Header
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <param name="packetCryptoService"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, GamePacketPayload, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> WithPacketWriting(this INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, GamePacketPayload, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> pipelineRegister, [NotNull] ISerializerService serializer, [NotNull] ISessionPacketCryptoService packetCryptoService)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));
			if (packetCryptoService == null) throw new ArgumentNullException(nameof(packetCryptoService));

			//This is sad to do, because this pipeline system I thought was so well designed, but there is no way to achieve cleanly the ARC4 packet encryption
			//process than to chunk it all together in a single component.
			return pipelineRegister.With(new GameOutgoingClientPacketPipelineComponent(serializer, packetCryptoService), o => o.For<HeaderPipeline.Main>());
		}
	}
}
