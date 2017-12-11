using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Network;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Base contract for all game message async handlers.
	/// </summary>
	/// <typeparam name="TGamePayloadType">The payload this handler is responsible for.</typeparam>
	public abstract class GameMessageHandlerAsync<TGamePayloadType> : INetworkMessagePipelineListenerAsync<GameNetworkMessageContext, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>
		where TGamePayloadType : GamePacketPayload
	{
		public async Task<NetworkMessageContextState> RecievePipelineMessage([NotNull] GameNetworkMessageContext input, NetworkMessageContextState currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));

			TGamePayloadType stronglyTypedPayload = input.NetworkMessage.Payload as TGamePayloadType;

			//If it didn't cast or was already null then we should yield an invalid result
			if (stronglyTypedPayload == null)
				return NetworkMessageContextState.Invalid | currentState;

			//Pass to message handling method implemented in the child class
			return await RecieveMessage(input, stronglyTypedPayload) | currentState;
		}

		/// <summary>
		/// Message handler method. Provided context and <see cref="stronglyTypedPayload"/> and promised to never be null.
		/// The <see cref="stronglyTypedPayload"/> is merely a strongly typed version of the payload contained in the <see cref="GameNetworkMessageContext"/> for
		/// ease of consumption.
		/// </summary>
		/// <param name="context">The game message context.</param>
		/// <param name="stronglyTypedPayload">The strongly typed casted payload.</param>
		/// <returns></returns>
		protected abstract Task<NetworkMessageContextState> RecieveMessage([NotNull] GameNetworkMessageContext context, [NotNull] TGamePayloadType stronglyTypedPayload);
	}
}
