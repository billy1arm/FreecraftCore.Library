using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Generic pipeline component that reinserts the byte representation of the <typeparamref name="TOperationCodeType"/> operation code
	/// back into the reader.
	/// </summary>
	/// <typeparam name="TNetworkContextBuilderType">The type of the context builder.</typeparam>
	/// <typeparam name="TOperationCodeType">The type of the operation code.</typeparam>
	/// <typeparam name="THeaderType">The type of the header.</typeparam>
	/// <typeparam name="TPayloadType">The type of the payload.</typeparam>
	public sealed class OperationCodeReinsertPipelineComponent<TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType> : INetworkInputPipelineListenerAsync<IWireStreamReaderStrategyAsync, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType>
		where TNetworkContextBuilderType : INetworkMessageContextBuilder<TOperationCodeType, THeaderType, TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <summary>
		/// The serializer service.
		/// </summary>
		[NotNull]
		private ISerializationService Serializer { get; }

		public OperationCodeReinsertPipelineComponent([NotNull] ISerializationService serializer)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			Serializer = serializer;
		}

		/// <inheritdoc />
		public Task<IWireStreamReaderStrategyAsync> RecievePipelineMessage([NotNull] IWireStreamReaderStrategyAsync input, TNetworkContextBuilderType currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (currentState == null) throw new ArgumentNullException(nameof(currentState));

			//If the header is null then this pipeline component is out of order or the header failed to deserialize
			if (currentState.GameHeader == null)
				throw new InvalidOperationException($"Failed to read the {typeof(TOperationCodeType).FullName} from the stream. There was no {typeof(THeaderType).FullName} in the {typeof(TNetworkContextBuilderType).FullName} available.");

			//Doesn't need to be async because serializing doesn't need to be async unless we serialize directly to a network stream.
			return Task.FromResult(input.PreprendWithBytesAsync(Serializer.Serialize(currentState.GameHeader.OperationCode)));
		}
	}
}
