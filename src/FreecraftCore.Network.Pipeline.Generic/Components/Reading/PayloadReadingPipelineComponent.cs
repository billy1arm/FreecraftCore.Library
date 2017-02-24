using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	public sealed class PayloadReadingPipelineComponent<TDerivedGamePayloadDeserializableType, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType> : INetworkInputPipelineListenerAsync<IWireStreamReaderStrategyAsync, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType>
		where TDerivedGamePayloadDeserializableType : TPayloadType
		where TNetworkContextBuilderType : INetworkMessageContextBuilder<TOperationCodeType, THeaderType, TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <summary>
		/// The serializer service.
		/// </summary>
		[NotNull]
		private ISerializationServiceAsync Serializer { get; }

		public PayloadReadingPipelineComponent([NotNull] ISerializationServiceAsync serializer)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			Serializer = serializer;
		}

		public async Task<IWireStreamReaderStrategyAsync> RecievePipelineMessage(IWireStreamReaderStrategyAsync input, [NotNull] TNetworkContextBuilderType currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (currentState == null) throw new ArgumentNullException(nameof(currentState));

			//Deserialize a payload from the steam
			currentState.GamePayload = await Serializer.DeserializeAsync<TDerivedGamePayloadDeserializableType>(input);

			//We don't need to do anything to the reader
			//In fact it is probably empty at this point
			return input;
		}
	}
}
