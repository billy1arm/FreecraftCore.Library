using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Crypto;
using FreecraftCore.Network;
using FreecraftCore.Packet;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore
{
	/// <summary>
	/// Extension methods for generic pipeline services.
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
		public static INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithPayloadPipeline<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] INetworkReaderInputPipelineListenerAsync<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineComponent)
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
		public static INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithHeaderPipeline<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] INetworkReaderInputPipelineListenerAsync<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineComponent)
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
	}

	public static class ARC4PipelineComponentRegisterationExtensions
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
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithARC4PayloadPipeline<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] ISessionPacketCryptoService cryptoService)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (cryptoService == null) throw new ArgumentNullException(nameof(cryptoService));

			pipelineRegister.TryRegisterPipeline(new ARC4StreamReadingPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(cryptoService), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);

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
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithARC4HeaderPipeline<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] ISessionPacketCryptoService cryptoService)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (cryptoService == null) throw new ArgumentNullException(nameof(cryptoService));

			pipelineRegister.TryRegisterPipeline(new ARC4StreamReadingPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(cryptoService), NetworkPipelineTypes.Header | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}
	}

	public static class PayloadSizingPipelineComponentRegisterationExtensions
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
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithPayloadSizingPipeline<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] ISessionPacketCryptoService cryptoService)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>, IHeaderPayloadSizeable
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (cryptoService == null) throw new ArgumentNullException(nameof(cryptoService));

			pipelineRegister.TryRegisterPipeline(new PayloadSizingStreamInsertionPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}
	}
}
