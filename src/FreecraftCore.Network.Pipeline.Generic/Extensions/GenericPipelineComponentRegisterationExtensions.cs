using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Extension methods for the 
	/// </summary>
	public static class GenericPipelineComponentRegisterationExtensions
	{
		/// <summary>
		/// Registers a pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <typeparam name="TNetworkInputType"></typeparam>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <param name="pipelineComponent"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithPayloadPipeline<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] INetworkInputPipelineAsyncListener<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineComponent)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (pipelineComponent == null) throw new ArgumentNullException(nameof(pipelineComponent));

			pipelineRegister.TryRegisterPipeline(pipelineComponent, NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}

		/// <summary>
		/// Registers a pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <typeparam name="TNetworkInputType"></typeparam>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <param name="pipelineComponent"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithHeaderPipeline<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] INetworkInputPipelineAsyncListener<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineComponent)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (pipelineComponent == null) throw new ArgumentNullException(nameof(pipelineComponent));

			pipelineRegister.TryRegisterPipeline(pipelineComponent, NetworkPipelineTypes.Header | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}

		/// <summary>
		/// Registers a pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <param name="serializer"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithPayloadStreamPeekPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, ISerializerService serializer)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			pipelineRegister.TryRegisterPipeline(new PeekSupportPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}

		/// <summary>
		/// Registers a pipeline in the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <param name="serializer"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithHeaderStreamPeekPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, ISerializerService serializer)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			pipelineRegister.TryRegisterPipeline(new PeekSupportPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(), NetworkPipelineTypes.Header | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}
	}
}
