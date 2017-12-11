using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Serializer;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Pipeline components that can listen to a stream input.
	/// </summary>
	/// <typeparam name="TNetworkContextBuilderType">The network context type.</typeparam>
	/// <typeparam name="TOperationCodeType">The operation code.</typeparam>
	/// <typeparam name="THeaderType">The header type for the context.</typeparam>
	/// <typeparam name="TPayloadType">The payload for the context.</typeparam>
	public abstract class ReaderInputPipelineComponent<TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType> : INetworkInputPipelineListenerAsync<IWireStreamReaderStrategyAsync, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType>
		where TNetworkContextBuilderType : INetworkMessageContextBuilder<TOperationCodeType, THeaderType, TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <inheritdoc />
		public abstract Task<IWireStreamReaderStrategyAsync> RecievePipelineMessage(IWireStreamReaderStrategyAsync input, TNetworkContextBuilderType currentState);
	}
}
