using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Contract for any pipeline component that can listen to network contexts.
	/// </summary>
	/// <typeparam name="TNetworkMessageContextType">The message context type.</typeparam>
	/// <typeparam name="TNetworkOperationCodeType">The operation code type.</typeparam>
	/// <typeparam name="THeaderType">The header type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type.</typeparam>
	public interface INetworkMessagePipelineListenerAsync<in TNetworkMessageContextType, TNetworkOperationCodeType, THeaderType, TPayloadType> : IPipelineListenerAsync<NetworkMessageContextState, TNetworkMessageContextType, NetworkMessageContextState>
		where TNetworkMessageContextType : INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType>
		where TNetworkOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{

	}
}
