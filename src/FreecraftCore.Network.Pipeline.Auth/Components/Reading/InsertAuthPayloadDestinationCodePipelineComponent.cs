using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Authentication pipeline component that inserts a destination code byte into the stream for deserialization
	/// to the correct payload type.
	/// </summary>
	public class InsertAuthPayloadDestinationCodePipelineComponent : AuthenticationWireReaderPipelineComponent
	{
		/// <summary>
		/// The serializer service.
		/// </summary>
		[NotNull]
		private ISerializationService Serializer { get; }

		/// <summary>
		/// Indicates the destination code to write into the stream
		/// </summary>
		public Lazy<byte> ByteRepresentationOfDestinateCode { get; }

		public InsertAuthPayloadDestinationCodePipelineComponent([NotNull] ISerializationService serializer, AuthOperationDestinationCode destinationCode)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			Serializer = serializer;

			//We're sneaky so we lazily initialize a byte field with the byte representation of the destinate code
			ByteRepresentationOfDestinateCode = new Lazy<byte>(() => serializer.Serialize(destinationCode)[0], true);
		}

		/// <inheritdoc />
		public override Task<IWireStreamReaderStrategyAsync> RecievePipelineMessage([NotNull] IWireStreamReaderStrategyAsync input, [NotNull] INetworkMessageContextBuilder<AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload> currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (currentState == null) throw new ArgumentNullException(nameof(currentState));

			//TODO: Create a PrependWithByteAsync instead of Bytes to squeeze out some perf
			//Don't need to be async
			//See: AuthenticationPayloadAttribute to get the gist of this.
			return Task.FromResult(input.PreprendWithBytesAsync(new byte[] {ByteRepresentationOfDestinateCode.Value}));
		}
	}
}
