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
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
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

	public static class OperationCodeInsertionPipelineComponentRegisterationExtensions
	{
		/// <summary>
		/// Registers a pipeline component that reinserts the operation code into the reader on the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <param name="serializer"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithOpCodeReinsertionPayloadPipeline<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] ISerializationService serializer)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			pipelineRegister.TryRegisterPipeline(new OperationCodeReinsertPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(serializer), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}

		//Don't do this, I can't think of a real purpose for this
		/// <summary>
		/// Registers a pipeline component that reinserts the operation code into the reader on the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <param name="serializer"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithOpCodeReinsertionHeaderPipeline<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] ISerializationService serializer)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			pipelineRegister.TryRegisterPipeline(new OperationCodeReinsertPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(serializer), NetworkPipelineTypes.Header | NetworkPipelineTypes.Main);

			//Return for fluent registeration
			return pipelineRegister;
		}
	}

	public static class PeekSupportPipelineComponentRegisterationExtensions
	{
		/// <summary>
		/// Registers a pipeline component provides peeking/seeking support functionality for readers on the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithPeekStreamPayloadPipeline<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister)
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
		/// Registers a pipeline component provides peeking/seeking support functionality for readers on the <see cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithPeekStreamHeaderPipeline<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister)
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
