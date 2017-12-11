using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Network;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore
{
	/// <summary>
	/// Extension methods for game pipeline services.
	/// </summary>
	public static class GamePipelineComponentRegisterationExtensions
	{
		/// <summary>
		/// Registers a parameterless pipeline component in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> WithType<TPipelineComponentType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> pipelineRegister, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
			where TPipelineComponentType : IPipelineListenerAsync<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>>, new()
		{
			//This just Activator.CreateInstance a pipeline component that doesn't require any parameters.
			//Provides a slightly clean looking API
			//Return for fluent registeration
			return pipelineRegister.With(new TPipelineComponentType(), options);
		}

		/// <summary>
		/// Registers a parameterless pipeline component in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, GamePacketPayload, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> WithType<TPipelineComponentType>(this INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, GamePacketPayload, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> pipelineRegister, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
			where TPipelineComponentType : IPipelineListenerAsync<IWireStreamWriterStrategyAsync, IWireStreamWriterStrategyAsync, GamePacketPayload>, new()
		{
			//This just Activator.CreateInstance a pipeline component that doesn't require any parameters.
			//Provides a slightly clean looking API
			//Return for fluent registeration
			return pipelineRegister.With(new TPipelineComponentType(), options);
		}

		/// <summary>
		/// Registers a payload deserialization pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> WithPayloadDeserializationPipeline<TDeserializablePayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> pipelineRegister, [NotNull] ISerializerService serializer, IPipelineOrderingStrategy orderingStrategy = null)
			where TDeserializablePayloadType : GamePacketPayload
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			if (orderingStrategy == null)
				pipelineRegister.TryRegisterPipeline(new PayloadReadingPipelineComponent<TDeserializablePayloadType, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>(serializer), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);
			else
				pipelineRegister.TryRegisterPipeline(new PayloadReadingPipelineComponent<TDeserializablePayloadType, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>(serializer), new AggregateOptionsWithOrderStrategyDecorated(orderingStrategy, NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main));

			//Return for fluent registeration
			return pipelineRegister;
		}

		/// <summary>
		/// Registers a payload deserialization pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Header
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> WithHeaderDeserializationPipeline<TDeserializableHeaderType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload> pipelineRegister, [NotNull] ISerializerService serializer, IPipelineOrderingStrategy orderingStrategy = null)
			where TDeserializableHeaderType : IGamePacketHeader
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			if (orderingStrategy == null)
				pipelineRegister.TryRegisterPipeline(new HeaderReadingPipelineComponent<TDeserializableHeaderType, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>(serializer), NetworkPipelineTypes.Header | NetworkPipelineTypes.Main);
			else
				pipelineRegister.TryRegisterPipeline(new HeaderReadingPipelineComponent<TDeserializableHeaderType, INetworkMessageContextBuilder<NetworkOperationCode, IGamePacketHeader, GamePacketPayload>, NetworkOperationCode, IGamePacketHeader, GamePacketPayload>(serializer), new AggregateOptionsWithOrderStrategyDecorated(orderingStrategy, NetworkPipelineTypes.Header | NetworkPipelineTypes.Main));

			//Return for fluent registeration
			return pipelineRegister;
		}
	}
}
