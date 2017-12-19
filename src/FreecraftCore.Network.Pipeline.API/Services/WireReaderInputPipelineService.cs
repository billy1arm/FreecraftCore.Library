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
	/// Base implementation of a service that provides pipeline binding/registeration and context construction.
	/// </summary>
	/// <typeparam name="TContextBuilderType">The context builder type.</typeparam>
	/// <typeparam name="TNetworkOperationCodeType">The operationcode type.</typeparam>
	/// <typeparam name="THeaderType">The header type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type.</typeparam>
	public abstract class WireReaderInputPipelineService<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> : WireNetworkInputPipelineService<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>,
		INetworkMessageContextFactory<IWireStreamReaderStrategyAsync, TNetworkOperationCodeType, THeaderType, TPayloadType>
		where TNetworkOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
		where TPayloadType : IMessageVerifyable
		where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType> //new is dangerous. Use a compiled lambda to construct instead of new
	{
		/// <summary>
		/// Factory service that can build the contexts.
		/// </summary>
		[NotNull]
		public INetworkMessageContextBuilderFactory<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> ContextBuilderFactory { get; }

		protected WireReaderInputPipelineService([NotNull] INetworkMessageContextBuilderFactory<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> contextBuilderFactory)

		{
			if (contextBuilderFactory == null) throw new ArgumentNullException(nameof(contextBuilderFactory));

			ContextBuilderFactory = contextBuilderFactory;
		}
		public INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType> ConstructNetworkContext(IWireStreamReaderStrategyAsync reader)
		{
			throw new NotImplementedException();
		}

		//TODO: Handle more pipelines

		public async Task<INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType>> ConstructNetworkContextAsync(IWireStreamReaderStrategyAsync reader)
		{
			//Need a new context builder for every network context we try to build
			TContextBuilderType contextBuilder = ContextBuilderFactory.CreateNew();

			//Make a new ref so that we can maintain the decorated input
			IWireStreamReaderStrategyAsync readerDecorated = reader;

			//Pass the reader and contextbuilder to all header pipelines
			await PassThroughPipeline(HeaderPipelines, contextBuilder, readerDecorated);

			//Important to reset the reader for the payload pipeline
			readerDecorated = reader;

			await PassThroughPipeline(PayloadPipelines, contextBuilder, readerDecorated);

			//Return the built context
			return contextBuilder.Build();
		}
	}
}
