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
		public static INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> With<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<TNetworkInputType, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TContextBuilderType> pipelineComponent, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
		{
			if (pipelineComponent == null) throw new ArgumentNullException(nameof(pipelineComponent));

			if (options == null)
				pipelineRegister.TryRegisterPipeline(pipelineComponent, NetworkPipelineTypes.Default);
			else
				pipelineRegister.TryRegisterPipeline(pipelineComponent, options(new UnorderedPipelineRegisterationOptions()));

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
		/// <param name="cryptoService"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithARC4<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, 
			[NotNull] ISessionPacketCryptoService cryptoService, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (cryptoService == null) throw new ArgumentNullException(nameof(cryptoService));

			//Return for fluent registeration
			return pipelineRegister.With(new ARC4StreamReadingPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(cryptoService), options);
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
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithPayloadStreamResizing<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, IPipelineOrderingStrategy orderStrategy = null)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>, IHeaderPayloadSizeable
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (orderStrategy == null)
				pipelineRegister.TryRegisterPipeline(new PayloadSizingStreamInsertionPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(), NetworkPipelineTypes.Payload | NetworkPipelineTypes.Main);
			else
				pipelineRegister.With(new PayloadSizingStreamInsertionPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(), o => o.For<PayloadPipeline.Main>().WithOrder(orderStrategy));
			//Return for fluent registeration
			return pipelineRegister;
		}
	}

	public static class OperationCodeInsertionPipelineComponentRegisterationExtensions
	{
		//Don't do this, I can't think of a real purpose for this
		/// <summary>
		/// Registers a pipeline component that reinserts the operation code into the reader on the <see cref="pipelineRegister"/>.
		/// </summary>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <param name="serializer"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithOpCodeReinsertion<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] ISerializationService serializer, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			pipelineRegister.With(new OperationCodeReinsertPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(serializer), options);

			//Return for fluent registeration
			return pipelineRegister;
		}
	}

	public static class PeekSupportPipelineComponentRegisterationExtensions
	{
		/// <summary>
		/// Registers a pipeline component provides peeking/seeking support functionality for readers on the <see cref="pipelineRegister"/>.
		/// </summary>
		/// <typeparam name="TContextBuilderType"></typeparam>
		/// <typeparam name="TNetworkOperationCodeType"></typeparam>
		/// <typeparam name="THeaderType"></typeparam>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="pipelineRegister"></param>
		/// <returns></returns>
		public static INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> WithPeekStream<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(this INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [CanBeNull] Func<UnorderedPipelineRegisterationOptions, ICompleteOptionsReadable> options = null)
			where TPayloadType : IMessageVerifyable
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
			where TNetworkOperationCodeType : struct
			where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		{
			//Return for fluent registeration
			return pipelineRegister.With(new PeekSupportReadingPipelineComponent<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>(), options);
		}
	}

	public static class PayloadWritingComponentRegisterationExtensions
	{
		/// <summary>
		/// Registers a payload writing pipeline component cref="pipelineRegister"/> with the <see cref="NetworkPipelineTypes"/> flags NetworkPipelineTypes.Payload
		/// </summary>
		/// <param name="pipelineRegister">Pipeline service to register on.</param>
		/// <param name="serializer"></param>
		/// <returns>Register for fluent chaining.</returns>
		public static INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, TPayloadType, TOperationCodeType, THeaderType, TPayloadType> WithPayloadWriting<TPayloadType, TOperationCodeType, THeaderType>(this INetworkInputPipelineRegister<IWireStreamWriterStrategyAsync, TPayloadType, TOperationCodeType, THeaderType, TPayloadType> pipelineRegister, [NotNull] ISerializerService serializer) 
			where TOperationCodeType : struct 
			where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType> 
			where TPayloadType : IMessageVerifyable
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			//Just register through generic with PayloadPipeline.Main
			return pipelineRegister.With(new PayloadWritingComponent<TPayloadType, TOperationCodeType, THeaderType>(serializer), o => o.For<PayloadPipeline.Main>());
		}
	}

}
