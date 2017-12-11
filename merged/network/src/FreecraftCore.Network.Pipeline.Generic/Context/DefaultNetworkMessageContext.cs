using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Default generic network message context.
	/// </summary>
	/// <typeparam name="TOperationCodeType"></typeparam>
	/// <typeparam name="THeaderType"></typeparam>
	/// <typeparam name="TPayloadType"></typeparam>
	public class DefaultNetworkMessageContext<TOperationCodeType, THeaderType, TPayloadType> : INetworkMessageContext<TOperationCodeType, THeaderType, TPayloadType> 
		where TOperationCodeType : struct 
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType> 
		where TPayloadType : IMessageVerifyable
	{
		/// <inheritdoc />
		[NotNull]
		public INetworkPacket<TOperationCodeType, THeaderType, TPayloadType> NetworkMessage { get; }

		public DefaultNetworkMessageContext([NotNull] INetworkPacket<TOperationCodeType, THeaderType, TPayloadType> networkMessage)
		{
			if (networkMessage == null) throw new ArgumentNullException(nameof(networkMessage));

			NetworkMessage = networkMessage;
		}
	}
}
