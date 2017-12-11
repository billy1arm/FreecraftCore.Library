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
	/// Pipeline component that builds and writes an authentication packet header into the writer.
	/// </summary>
	public class AuthenticationHeaderWritingPipelineComponent : AuthenticationWireWriterPipelineComponent
	{
		[NotNull]
		private ISerializerService Serializer { get; }

		public AuthenticationHeaderWritingPipelineComponent([NotNull] ISerializerService serializer)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			Serializer = serializer;
		}

		/// <inheritdoc />
		public override Task<IWireStreamWriterStrategyAsync> RecievePipelineMessage(IWireStreamWriterStrategyAsync input, AuthenticationPayload currentState)
		{
			//Write the packet header with the payload opcode
			Serializer.Serialize(new AuthenticationPacketHeader(currentState.GetOperationCode()), input);

			return Task.FromResult(input);
		}
	}
}
