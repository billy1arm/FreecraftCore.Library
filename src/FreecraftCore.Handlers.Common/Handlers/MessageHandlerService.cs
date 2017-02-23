using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FreecraftCore.Network;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Generic message handler service.
	/// </summary>
	/// <typeparam name="TMessageType"></typeparam>
	/// <typeparam name="TOperationCode"></typeparam>
	/// <typeparam name="THeaderType"></typeparam>
	/// <typeparam name="TPayloadType"></typeparam>
	public class MessageHandlerService<TMessageType, TOperationCode, THeaderType, TPayloadType> 
		: IHandlerRegisterService<INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType>, TOperationCode>, IHandlerRegisterService<INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType>, TOperationCode>,
		INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType>
		where TMessageType : INetworkMessageContext<TOperationCode, THeaderType, TPayloadType> 
		where TOperationCode : struct 
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCode> 
		where TPayloadType : IMessageVerifyable

	{
		/// <summary>
		/// Dicitionary that maps operation codes to their respective syncronous message handlers.
		/// </summary>
		[NotNull]
		private Dictionary<TOperationCode, INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType>> SyncMessageHandlerMap { get; }

		/// <summary>
		/// Dicitionary that maps operation codes to their respective async message handlers.
		/// </summary>
		[NotNull]
		private Dictionary<TOperationCode, INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType>> AsyncMessageHandlerMap { get; }

		public MessageHandlerService()
		{
			AsyncMessageHandlerMap = new Dictionary<TOperationCode, INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType>>();
			SyncMessageHandlerMap = new Dictionary<TOperationCode, INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType>>();
		}

		/// <inheritdoc />
		public bool TryRegister([NotNull] INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType> handler, TOperationCode code)
		{
			if (handler == null) throw new ArgumentNullException(nameof(handler));

			if(!Enum.IsDefined(typeof(TOperationCode), code))
				throw new ArgumentException($"Provided {typeof(TOperationCode).FullName} was not in a valid range. Value was: {code}", nameof(code));

			SyncMessageHandlerMap[code] = handler;
			return true;
		}

		/// <inheritdoc />
		public bool TryRegister([NotNull] INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType> handler, TOperationCode code)
		{
			if (handler == null) throw new ArgumentNullException(nameof(handler));

			if (!Enum.IsDefined(typeof(TOperationCode), code))
				throw new ArgumentException($"Provided {typeof(TOperationCode).FullName} was not in a valid range. Value was: {code}", nameof(code));

			AsyncMessageHandlerMap[code] = handler;
			return true;
		}

		/// <inheritdoc />
		public async Task<NetworkMessageContextState> RecievePipelineMessage([NotNull] TMessageType input, NetworkMessageContextState currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (!Enum.IsDefined(typeof(NetworkMessageContextState), currentState))
				throw new ArgumentOutOfRangeException(nameof(currentState), "Value should be defined in the NetworkMessageContextState enum.");
			
			//TODO: Should we check header validity?
			TOperationCode code = input.NetworkMessage.Header.OperationCode;

			if (SyncMessageHandlerMap.ContainsKey(code))
				return SyncMessageHandlerMap[code].RecievePipelineMessage(input, currentState);
			else if (AsyncMessageHandlerMap.ContainsKey(code))
				return await AsyncMessageHandlerMap[code].RecievePipelineMessage(input, currentState);

			throw new InvalidOperationException($"Requested {typeof(TOperationCode).FullName} with Value: {code} did not have a matching handler contained in the service.");
		}
	}
}
