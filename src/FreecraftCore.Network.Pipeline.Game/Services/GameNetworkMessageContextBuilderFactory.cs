using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	//It may seem silly to have a factory for this but
	//it gives us a later vector to implement message context and builder recycling for perf and GC pressure reduction.
	/// <summary>
	/// Game <see cref="INetworkMessageContextBuilder{TOperationCodeType,THeaderType,TPayloadType}"/> factory.
	/// </summary>
	public class GameNetworkMessageContextBuilderFactory : INetworkMessageContextBuilderFactory<DefaultNetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>
	{
		/// <inheritdoc />
		public DefaultNetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload> CreateNew()
		{
			//This probably looks confusing but it's just a way to have kept these two classes generic.
			//We use a generic type and a complex func to initialize a new authentication packet.
			return new DefaultNetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>(c =>
			{
				return new DefaultNetworkMessageContext<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>(new GamePacket(c.GameHeader, c.GamePayload));
			} );
		}
	}
}
