using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Pipeline component that writes the payload into the writer.
	/// </summary>
	public class AuthenticationPayloadWritingComponent : AuthenticationWireWriterPipelineComponent
	{
		[NotNull]
		private ISerializerService Serializer { get; }

		public AuthenticationPayloadWritingComponent([NotNull] ISerializerService serializer)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			Serializer = serializer;
		}

		/// <inheritdoc />
		public override Task<IWireStreamWriterStrategyAsync> RecievePipelineMessage(IWireStreamWriterStrategyAsync input, AuthenticationPayload currentState)
		{
			//Just serialize the payload into the provided writer.
			Serializer.Serialize(currentState, input);

			return Task.FromResult(input);
		}
	}
}
