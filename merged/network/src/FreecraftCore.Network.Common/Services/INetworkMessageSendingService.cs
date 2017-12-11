using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Network service that can send a network message.
	/// </summary>
	/// <typeparam name="TOperationCodeType">The operation code of the message.</typeparam>
	public interface INetworkMessageSendingService<TOperationCodeType>
	{
		//TODO: Change API. maybe
		/// <summary>
		/// Send the provided <see cref="message"/>
		/// </summary>
		/// <typeparam name="TMessageType">A non-null message that </typeparam>
		/// <param name="message"></param>
		Task SendMessage<TMessageType>(TMessageType message)
			where TMessageType : IOperationCodeProvidable<TOperationCodeType>, IMessageVerifyable;
	}
}
