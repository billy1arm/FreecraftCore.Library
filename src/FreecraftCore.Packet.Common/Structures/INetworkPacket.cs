using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Network Packet contract.
	/// </summary>
	/// <typeparam name="TOperationCodeType">The operation code of the packet.</typeparam>
	/// <typeparam name="THeaderType">The header type of the packet.</typeparam>
	/// <typeparam name="TPayloadType">The payload type of the packet.</typeparam>
	public interface INetworkPacket<TOperationCodeType, out THeaderType, out TPayloadType>
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <summary>
		/// The header for the network packet.
		/// </summary>
		[NotNull]
		THeaderType Header { get; }

		/// <summary>
		/// The payload for the network packet.
		/// </summary>
		[NotNull]
		TPayloadType Payload { get; }
	}
}
