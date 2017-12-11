using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// A network message context that contains a <see cref="INetworkPacket{TOperationCodeType,THeaderType,TPayloadType}"/>.
	/// </summary>
	/// <typeparam name="TOperationCodeType">Operation code type of the context.</typeparam>
	/// <typeparam name="THeaderType">The header type of the context.</typeparam>
	/// <typeparam name="TPayloadType">The payload type of the context.</typeparam>
	public interface INetworkMessageContext<TOperationCodeType, out THeaderType, out TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <summary>
		/// Represents the network packet for the context.
		/// </summary>
		[NotNull]
		INetworkPacket<TOperationCodeType, THeaderType, TPayloadType> NetworkMessage { get; }
	}
}
