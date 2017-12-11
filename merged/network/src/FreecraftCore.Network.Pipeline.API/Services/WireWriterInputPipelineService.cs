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
	/// <typeparam name="TNetworkOperationCodeType">The operationcode type.</typeparam>
	/// <typeparam name="THeaderType">The header type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type.</typeparam>
	/// <typeparam name="TMessageType"></typeparam>
	public abstract class WireWriterInputPipelineService<TMessageType, TNetworkOperationCodeType, THeaderType, TPayloadType> : WireNetworkInputPipelineService<IWireStreamWriterStrategyAsync, TMessageType, TNetworkOperationCodeType, THeaderType, TPayloadType>,
		INetworkMessageWriterHandler<TMessageType, IWireStreamWriterStrategyAsync>
		where TNetworkOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
		where TPayloadType : IMessageVerifyable
		where TMessageType : IOperationCodeProvidable<TNetworkOperationCodeType> //must be a message that can produce a valid operation code
	{
		protected WireWriterInputPipelineService()
			: base()
		{

		}

		public async Task<IWireStreamWriterStrategyAsync> RecievePipelineMessage([NotNull] TMessageType input, [NotNull] IWireStreamWriterStrategyAsync currentState)
		{
			//Pass the writer to the header and payload pipelines
			await PassThroughPipeline(HeaderPipelines, input, currentState);

			await PassThroughPipeline(PayloadPipelines, input, currentState);

			//Return the built context
			return currentState;
		}
	}
}
