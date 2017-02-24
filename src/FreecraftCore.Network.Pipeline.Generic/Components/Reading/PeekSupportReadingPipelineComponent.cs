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
	/// Generic pipeline component that adds peeking functionality to an unpeekable stream.
	/// </summary>
	/// <typeparam name="TNetworkContextBuilderType"></typeparam>
	/// <typeparam name="TOperationCodeType"></typeparam>
	/// <typeparam name="THeaderType"></typeparam>
	/// <typeparam name="TPayloadType"></typeparam>
	public class PeekSupportReadingPipelineComponent<TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType> : INetworkInputPipelineListenerAsync<IWireStreamReaderStrategyAsync, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType>
		where TNetworkContextBuilderType : INetworkMessageContextBuilder<TOperationCodeType, THeaderType, TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		public Task<IWireStreamReaderStrategyAsync> RecievePipelineMessage(IWireStreamReaderStrategyAsync input, [NotNull] TNetworkContextBuilderType currentState)
		{
			//We need to add stream peaking functionality
			//The streams we're dealing with won't support it by default.
			return Task.FromResult(input.PeekWithBufferingAsync());
		}
	}
}
