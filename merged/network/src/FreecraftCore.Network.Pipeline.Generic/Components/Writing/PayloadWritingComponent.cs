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
	/// Generic pipeline component that writes a payload.
	/// </summary>
	/// <typeparam name="TPayloadType"></typeparam>
	/// <typeparam name="TOperationCodeType"></typeparam>
	/// <typeparam name="THeaderType"></typeparam>
	public class PayloadWritingComponent<TPayloadType, TOperationCodeType, THeaderType> : WireWriterPipelineComponent<TPayloadType, TOperationCodeType, THeaderType, TPayloadType> 
		where TOperationCodeType : struct 
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType> 
		where TPayloadType : IMessageVerifyable
	{
		[NotNull]
		private ISerializerService Serializer { get; }

		public PayloadWritingComponent([NotNull] ISerializerService serializer)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			Serializer = serializer;
		}

		/// <inheritdoc />
		public override Task<IWireStreamWriterStrategyAsync> RecievePipelineMessage(IWireStreamWriterStrategyAsync input, TPayloadType currentState)
		{
			//Just serialize the payload into the provided writer.
			Serializer.Serialize(currentState, input);

			return Task.FromResult(input);
		}
	}
}
