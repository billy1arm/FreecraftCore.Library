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
	/// Generic header reading pipeline component.
	/// </summary>
	/// <typeparam name="TDerivedDeserializableHeaderType"></typeparam>
	/// <typeparam name="TNetworkContextBuilderType"></typeparam>
	/// <typeparam name="TOperationCodeType"></typeparam>
	/// <typeparam name="THeaderType"></typeparam>
	/// <typeparam name="TPayloadType"></typeparam>
	public sealed class HeaderReadingPipelineComponent<TDerivedDeserializableHeaderType, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType> : INetworkInputPipelineListenerAsync<IWireStreamReaderStrategyAsync, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType>
		where TDerivedDeserializableHeaderType : THeaderType
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

		public HeaderReadingPipelineComponent([NotNull] ISerializationServiceAsync serializer)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			Serializer = serializer;
		}

		public async Task<IWireStreamReaderStrategyAsync> RecievePipelineMessage(IWireStreamReaderStrategyAsync input, [NotNull] TNetworkContextBuilderType currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (currentState == null) throw new ArgumentNullException(nameof(currentState));

			//Deserialize a header from the incoming stream
			currentState.GameHeader = await Serializer.DeserializeAsync<TDerivedDeserializableHeaderType>(input);

			return input;
		}
	}
}
