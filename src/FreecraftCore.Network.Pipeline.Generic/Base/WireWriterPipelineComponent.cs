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
	/// <typeparam name="TNetworkStateType">The network context type.</typeparam>
	/// <typeparam name="TOperationCodeType">The operation code.</typeparam>
	/// <typeparam name="THeaderType">The header type for the context.</typeparam>
	/// <typeparam name="TPayloadType">The payload for the context.</typeparam>
	public abstract class WireWriterPipelineComponent<TNetworkStateType, TOperationCodeType, THeaderType, TPayloadType> : INetworkInputPipelineListenerAsync<IWireStreamWriterStrategyAsync, TNetworkStateType, TOperationCodeType, THeaderType, TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <inheritdoc />
		public abstract Task<IWireStreamWriterStrategyAsync> RecievePipelineMessage(IWireStreamWriterStrategyAsync input, TNetworkStateType currentState);
	}
}
