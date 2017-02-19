using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Serializer;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Base implementation of a service that provides pipeline binding/registeration and context construction.
	/// </summary>
	/// <typeparam name="TContextBuilderType">The context builder type.</typeparam>
	/// <typeparam name="TNetworkOperationCodeType">The operationcode type.</typeparam>
	/// <typeparam name="THeaderType">The header type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type.</typeparam>
	public abstract class WireReaderInputPipelineService<TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> :
		INetworkMessageContextFactory<IWireStreamReaderStrategyAsync, TNetworkOperationCodeType, THeaderType, TPayloadType>,
		INetworkInputPipelineRegister<IWireStreamReaderStrategyAsync, TContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType>
		where TNetworkOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
		where TPayloadType : IMessageVerifyable
		where TContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>, new() //new is dangerous. Use a compiled lambda to construct instead of new
	{
		/// <summary>
		/// Enumerable list of header pipelines.
		/// </summary>
		protected List<IPipelineAsyncListener<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, TContextBuilderType>> HeaderPipelines { get; }

		/// <summary>
		/// Enumerable list of header pipelines.
		/// </summary>
		protected List<IPipelineAsyncListener<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, TContextBuilderType>> PayloadPipelines { get; }

		protected WireReaderInputPipelineService()
		{
			//For now we just construct new collections
			PayloadPipelines = new List<IPipelineAsyncListener<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, TContextBuilderType>>();
			HeaderPipelines = new List<IPipelineAsyncListener<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, TContextBuilderType>>();
		}

		//TODO: Support ordering and location
		/// <inheritdoc />
		public bool TryRegisterPipeline(IPipelineAsyncListener<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, TContextBuilderType> pipelineComponent, NetworkPipelineTypes pipelineType)
		{
			//TODO: Handle all flags
			if (pipelineType.HasFlag(NetworkPipelineTypes.Header))
			{
				HeaderPipelines.Add(pipelineComponent);
				return true;
			}
			else
			{
				if (pipelineType.HasFlag(NetworkPipelineTypes.Payload))
				{
					PayloadPipelines.Add(pipelineComponent);
					return true;
				}
			}

			throw new NotImplementedException($"Unable to handle pipelines with {pipelineType} flags right now. Must be only payload or header pipelines.");
		}

		protected async Task PassThroughPipeline(IReadOnlyCollection<IPipelineAsyncListener<IWireStreamReaderStrategyAsync, IWireStreamReaderStrategyAsync, TContextBuilderType>> pipelines, TContextBuilderType contextBuilder, IWireStreamReaderStrategyAsync readerDecorated)
		{
			//Pass the reader and contextbuilder to all provided pipelines
			if (pipelines.Count != 0)
				foreach (var pipeline in pipelines)
					readerDecorated = await pipeline.RecievePipelineMessageAsync(readerDecorated, contextBuilder);
		}

		public INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType> ConstructNetworkContext(IWireStreamReaderStrategyAsync reader)
		{
			throw new NotImplementedException();
		}

		//TODO: Handle more pipelines

		public async Task<INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType>> ConstructNetworkContextAsync(IWireStreamReaderStrategyAsync reader)
		{
			//Need a new context builder for every network context we try to build
			TContextBuilderType contextBuilder = new TContextBuilderType();

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
