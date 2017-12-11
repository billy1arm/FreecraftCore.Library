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
	/// <typeparam name="TStateType">The context builder type.</typeparam>
	/// <typeparam name="TNetworkOperationCodeType">The operationcode type.</typeparam>
	/// <typeparam name="THeaderType">The header type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type.</typeparam>
	/// <typeparam name="TNetworkInputType">The network input type.</typeparam>
	public abstract class WireNetworkInputPipelineService<TNetworkInputType, TStateType, TNetworkOperationCodeType, THeaderType, TPayloadType> :
		INetworkInputPipelineRegister<TNetworkInputType, TStateType, TNetworkOperationCodeType, THeaderType, TPayloadType>
		where TNetworkOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <summary>
		/// Enumerable list of header pipelines.
		/// </summary>
		protected List<IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TStateType>> HeaderPipelines { get; }

		/// <summary>
		/// Enumerable list of header pipelines.
		/// </summary>
		protected List<IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TStateType>> PayloadPipelines { get; }

		protected WireNetworkInputPipelineService()
		{
			//For now we just construct new collections
			PayloadPipelines = new List<IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TStateType>>();
			HeaderPipelines = new List<IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TStateType>>();
		}

		//TODO: Support ordering and location
		/// <inheritdoc />
		public bool TryRegisterPipeline(IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TStateType> pipelineComponent, NetworkPipelineTypes pipelineType)
		{
			//On bottom is the default so use that
			return TryRegisterPipeline(pipelineComponent, pipelineType, new OnBottom());
		}

		//TODO: Support ordering and location
		/// <inheritdoc />
		private bool TryRegisterPipeline(IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TStateType> pipelineComponent, NetworkPipelineTypes pipelineType, IPipelineOrderingStrategy ordedStrategy)
		{
			bool registered = false;

			//TODO: Handle error/main flags and others
			if (pipelineType.HasFlag(NetworkPipelineTypes.Header))
			{
				ordedStrategy.RegisterVisitor(HeaderPipelines, pipelineComponent);
				registered = true;
			}

			if (pipelineType.HasFlag(NetworkPipelineTypes.Payload))
			{
				ordedStrategy.RegisterVisitor(PayloadPipelines, pipelineComponent);
				registered = true;
			}

			if (!registered)
				throw new NotImplementedException($"Unable to handle pipelines with {pipelineType} flags right now. Must be only payload or header pipelines.");

			return true;
		}

		protected async Task PassThroughPipeline(IReadOnlyCollection<IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TStateType>> pipelines, TStateType contextBuilder, TNetworkInputType inputDecorated)
		{
			//Pass the reader and contextbuilder to all provided pipelines
			if (pipelines.Count != 0)
				foreach (var pipeline in pipelines)
					inputDecorated = await pipeline.RecievePipelineMessage(inputDecorated, contextBuilder);
		}

		/// <inheritdoc />
		public bool TryRegisterPipeline(IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TStateType> pipelineComponent, ICompleteOptionsReadable options)
		{
			return TryRegisterPipeline(pipelineComponent, options.PipelineFlags, options);
		}
	}
}
