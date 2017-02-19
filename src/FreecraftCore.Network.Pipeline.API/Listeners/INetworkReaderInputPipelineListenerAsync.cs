using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Pipeline component that can listen to network inputs.
	/// </summary>
	/// <typeparam name="TNetworkInputType">The network input type.</typeparam>
	/// <typeparam name="TNetworkMessageContextBuilderType">The network context builder.</typeparam>
	/// <typeparam name="TNetworkOperationCodeType">The operation code type.</typeparam>
	/// <typeparam name="THeaderType">The header type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type.</typeparam>
	public interface INetworkReaderInputPipelineListenerAsync<TNetworkInputType, in TNetworkMessageContextBuilderType, TNetworkOperationCodeType, THeaderType, TPayloadType> : IPipelineAsyncListener<TNetworkInputType, TNetworkInputType, TNetworkMessageContextBuilderType>
		where TNetworkMessageContextBuilderType : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>
		where TNetworkOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{

	}
}
