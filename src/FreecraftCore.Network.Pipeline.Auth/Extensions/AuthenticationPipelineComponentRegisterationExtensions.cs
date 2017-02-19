using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Network;
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
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithPayloadPipeline<TPipelineComponentType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister)
			where TPipelineComponentType : IPipelineAsyncListener<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>>, new()
		{
			//This just Activator.CreateInstance a pipeline component that doesn't require any parameters.
			//Provides a slightly clean looking API
			pipelineRegister.TryRegisterPipeline(new TPipelineComponentType(), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}

		/// <summary>
		/// Registers a payload deserialization pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithPayloadDeserializationPipeline<TDeserializablePayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister, [NotNull] ISerializerService serializer)
			where TDeserializablePayloadType : AuthenticationPayload
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));
			//This just Activator.CreateInstance a pipeline component that doesn't require any parameters.
			//Provides a slightly clean looking API
			pipelineRegister.TryRegisterPipeline(new PayloadReadingPipelineComponent<TDeserializablePayloadType, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>(serializer), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}

		/// <summary>
		/// Registers a payload deserialization pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Header
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> WithHeaderReadingPipeline<TDeserializableHeaderType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> pipelineRegister, [NotNull] ISerializerService serializer)
			where TDeserializableHeaderType : IAuthenticationPacketHeader
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			//This just Activator.CreateInstance a pipeline component that doesn't require any parameters.
			//Provides a slightly clean looking API
			pipelineRegister.TryRegisterPipeline(new HeaderReadingPipelineComponent<TDeserializableHeaderType, INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>(serializer), NetworkPipelineTypes.Header | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}
	}
}
