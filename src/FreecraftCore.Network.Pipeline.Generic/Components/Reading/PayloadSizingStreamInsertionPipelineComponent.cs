using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Generic pipeline component that reconstructs the reader/stream making size of bytes specified in the packet header
	/// available.
	/// </summary>
	/// <typeparam name="TNetworkContextBuilderType">The type of the context builder.</typeparam>
	/// <typeparam name="TOperationCodeType">The type of the operation code.</typeparam>
	/// <typeparam name="THeaderType">The type of the header.</typeparam>
	/// <typeparam name="TPayloadType">The type of the payload.</typeparam>
	public class PayloadSizingStreamInsertionPipelineComponent<TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType> : INetworkInputPipelineListenerAsync<IWireStreamReaderStrategyAsync, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType>
		where TNetworkContextBuilderType : INetworkMessageContextBuilder<TOperationCodeType, THeaderType, TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>, IHeaderPayloadSizeable
		where TPayloadType : IMessageVerifyable
	{

		/// <inheritdoc />
		public async Task<IWireStreamReaderStrategyAsync> RecievePipelineMessage(IWireStreamReaderStrategyAsync input, TNetworkContextBuilderType currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (currentState == null) throw new ArgumentNullException(nameof(currentState));

			//If the header is null then this pipeline component is out of order or the header failed to deserialize
			if (currentState.GameHeader == null)
				throw new InvalidOperationException($"Failed to read the {nameof(NetworkOperationCode)} from the stream. There was no {typeof(THeaderType).FullName} in the {typeof(TNetworkContextBuilderType).FullName} available.");

			//TODO: Build an decorator for readers that disables async since this will soak up threadpool for no benefit in subsequent chaining pipeline components
			//Reads N bytes from the reader where N is the payload size defined in the header
			//It then creates a new discrete reader containing only those bytes
			return new DefaultStreamReaderStrategyAsync(await input.ReadBytesAsync(currentState.GameHeader.PayloadSize));
		}
	}
}
