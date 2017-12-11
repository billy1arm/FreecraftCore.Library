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
	public abstract class MessageHandlerService<TMessageType, TOperationCode, THeaderType, TPayloadType> 
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
		protected Dictionary<TOperationCode, INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType>> SyncMessageHandlerMap { get; }

		/// <summary>
		/// Dicitionary that maps operation codes to their respective async message handlers.
		/// </summary>
		[NotNull]
		protected Dictionary<TOperationCode, INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType>> AsyncMessageHandlerMap { get; }

		//TODO: This is kind of ugly but it's the only way we can support defaults
		/// <summary>
		/// Optional default syncronous handler.
		/// </summary>
		[CanBeNull]
		private INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType> DefaultSyncHandler { get; set; }

		/// <summary>
		/// Optional default async handler.
		/// </summary>
		[CanBeNull]
		private INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType> DefaultAsyncHandler { get; set; }

		protected MessageHandlerService()
		{
			AsyncMessageHandlerMap = new Dictionary<TOperationCode, INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType>>();
			SyncMessageHandlerMap = new Dictionary<TOperationCode, INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType>>();
		}

		/// <inheritdoc />
		public virtual bool TryRegister([NotNull] INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType> handler, TOperationCode code)
		{
			if (handler == null) throw new ArgumentNullException(nameof(handler));

			if(!Enum.IsDefined(typeof(TOperationCode), code))
				throw new ArgumentException($"Provided {typeof(TOperationCode).FullName} was not in a valid range. Value was: {code}", nameof(code));

			SyncMessageHandlerMap[code] = handler;
			return true;
		}

		/// <inheritdoc />
		public virtual bool TryRegister([NotNull] INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType> handler, TOperationCode code)
		{
			if (handler == null) throw new ArgumentNullException(nameof(handler));

			if (!Enum.IsDefined(typeof(TOperationCode), code))
				throw new ArgumentException($"Provided {typeof(TOperationCode).FullName} was not in a valid range. Value was: {code}", nameof(code));

			AsyncMessageHandlerMap[code] = handler;
			return true;
		}

		/// <inheritdoc />
		public virtual async Task<NetworkMessageContextState> RecievePipelineMessage([NotNull] TMessageType input, NetworkMessageContextState currentState)
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

			//Try to find a default
			if (DefaultSyncHandler != null)
				return DefaultSyncHandler.RecievePipelineMessage(input, currentState);
			else if (DefaultAsyncHandler != null)
				return await DefaultAsyncHandler.RecievePipelineMessage(input, currentState);

			throw new InvalidOperationException($"Requested {typeof(TOperationCode).FullName} with Value: {code} did not have a matching handler contained in the service.");
		}

		/// <inheritdoc />
		public bool TryRegisterDefault([NotNull] INetworkMessagePipelineListener<TMessageType, TOperationCode, THeaderType, TPayloadType> handler)
		{
			if (handler == null) throw new ArgumentNullException(nameof(handler));

			//TODO: Should we override this?
			DefaultSyncHandler = handler;

			return true;
		}

		/// <inheritdoc />
		public bool TryRegisterDefault([NotNull] INetworkMessagePipelineListenerAsync<TMessageType, TOperationCode, THeaderType, TPayloadType> handler)
		{
			if (handler == null) throw new ArgumentNullException(nameof(handler));

			//TODO: Should we override this?
			DefaultAsyncHandler = handler;

			return true;
		}
	}
}
