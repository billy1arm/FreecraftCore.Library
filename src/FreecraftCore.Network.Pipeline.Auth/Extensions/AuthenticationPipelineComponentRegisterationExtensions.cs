using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Network;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore
{
	/// <summary>
	/// Extension methods for authentication pipeline services.
	/// </summary>
	public static class AuthenticationPipelineComponentRegisterationExtensions
	{
		/// <summary>
		/// Registers a parameterless pipeline component in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithType<TPipelineComponentType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
			where TPipelineComponentType : IPipelineListenerAsync<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>>, new()
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
		public static INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, AuthenticationPayload, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithType<TPipelineComponentType>(this INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, AuthenticationPayload, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
			where TPipelineComponentType : IPipelineListenerAsync<IWireStreamWriterStrategyAsync, IWireStreamWriterStrategyAsync, AuthenticationPayload>, new()
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
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithPayloadDeserializationPipeline<TDeserializablePayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister, [NotNull] ISerializerService serializer, IPipelineOrderingStrategy orderingStrategy = null)
			where TDeserializablePayloadType : AuthenticationPayload
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			if (orderingStrategy == null)
				pipelineRegister.TryRegisterPipeline(new PayloadReadingPipelineComponent<TDeserializablePayloadType, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>(serializer), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);
			else
				pipelineRegister.TryRegisterPipeline(new PayloadReadingPipelineComponent<TDeserializablePayloadType, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>(serializer), new AggregateOptionsWithOrderStrategyDecorated(orderingStrategy, NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main));

			//Return for fluent registeration
			return pipelineRegister;
		}

		/// <summary>
		/// Registers a payload deserialization pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Header
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithHeaderDeserializationPipeline<TDeserializableHeaderType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister, [NotNull] ISerializerService serializer, IPipelineOrderingStrategy orderingStrategy = null)
			where TDeserializableHeaderType : IAuthenticationPacketHeader
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			if (orderingStrategy == null)
				pipelineRegister.TryRegisterPipeline(new HeaderReadingPipelineComponent<TDeserializableHeaderType, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>(serializer), NetworkPipelineTypes.Header | NetworkPipelineTypes.Main);
			else
				pipelineRegister.TryRegisterPipeline(new HeaderReadingPipelineComponent<TDeserializableHeaderType, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>(serializer), new AggregateOptionsWithOrderStrategyDecorated(orderingStrategy, NetworkPipelineTypes.Header | NetworkPipelineTypes.Main));

			//Return for fluent registeration
			return pipelineRegister;
		}

		/// <summary>
		/// Registers a component that adds the authentication destination code to the reader in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <param name="destinationCode"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithAuthDestinationCodeInsertion(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister, [NotNull] ISerializerService serializer, AuthOperationDestinationCode destinationCode, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			if (!Enum.IsDefined(typeof(AuthOperationDestinationCode), destinationCode))
				throw new ArgumentOutOfRangeException(nameof(destinationCode), "Value should be defined in the AuthOperationDestinationCode enum.");

			//Return for fluent registeration
			return pipelineRegister.With(new InsertAuthPayloadDestinationCodePipelineComponent(serializer, destinationCode), options);
		}

		/// <summary>
		/// Registers a header writing pipeline component cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Header
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, AuthenticationPayload, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithHeaderWriting(this INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, AuthenticationPayload, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister, [NotNull] ISerializerService serializer)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			//Just register through generic with HeaderPipeline.Main
			return pipelineRegister.With(new AuthenticationHeaderWritingPipelineComponent(serializer), o => o.For<HeaderPipeline.Main>());
		}
	}
}
