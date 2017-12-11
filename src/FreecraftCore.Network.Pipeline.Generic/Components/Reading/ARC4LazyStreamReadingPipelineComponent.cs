using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Crypto;
using FreecraftCore.Crypto.Stream;
using FreecraftCore.Packet;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	//This can be generic even though only headers are ARC4 encrypted.
	//So, there isn't any reason for this to be generic but it can be so we should
	/// <summary>
	/// Generic pipeline component that will decorate the inpute stream/reader with a lazy
	/// RC4 crypto reader.
	/// (Only needed for headers but was made generic)
	/// </summary>
	/// <typeparam name="TNetworkContextBuilderType">The type of the context builder.</typeparam>
	/// <typeparam name="TOperationCodeType">The type of the operation code.</typeparam>
	/// <typeparam name="THeaderType">The type of the header.</typeparam>
	/// <typeparam name="TPayloadType">The type of the payload.</typeparam>
	public class ARC4StreamReadingPipelineComponent<TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType> : INetworkInputPipelineListenerAsync<IWireStreamReaderStrategyAsync, TNetworkContextBuilderType, TOperationCodeType, THeaderType, TPayloadType>
		where TNetworkContextBuilderType : INetworkMessageContextBuilder<TOperationCodeType, THeaderType, TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <summary>
		/// The service that can be used to encrypt the any bytes.
		/// </summary>
		[NotNull]
		private ISessionPacketCryptoService HeaderCryptoService { get; }

		public ARC4StreamReadingPipelineComponent([NotNull] ISessionPacketCryptoService headerCryptoService)
		{
			if (headerCryptoService == null) throw new ArgumentNullException(nameof(headerCryptoService));

			HeaderCryptoService = headerCryptoService;
		}

		/// <inheritdoc />
		public Task<IWireStreamReaderStrategyAsync> RecievePipelineMessage(IWireStreamReaderStrategyAsync input, TNetworkContextBuilderType currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (currentState == null) throw new ArgumentNullException(nameof(currentState));

			//Just decorate the reader with the crypto reader decroator.
			return Task.FromResult(input.WithLazyCryptoAsync(HeaderCryptoService));
		}
	}
}
