using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Represents a network message context builder.
	/// </summary>
	/// <typeparam name="TNetworkOperationCodeType">The network operation code type.</typeparam>
	/// <typeparam name="THeaderType">The header type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type.</typeparam>
	public interface INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType> : IMessageContextBuilder<INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType>>
		where TNetworkOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		THeaderType GameHeader { get; set; }

		TPayloadType GamePayload { get; set; }
	}
}
