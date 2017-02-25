using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Strategy for routing a message.
	/// </summary>
	/// <typeparam name="TMessageType"></typeparam>
	public interface IMessageRoutingStrategy<in TMessageType>
	{
		/// <summary>
		/// Routes a message using the implemented strategy.
		/// </summary>
		/// <param name="message">The message to route.</param>
		Task RouteMessage([NotNull] TMessageType message); //callback usually used to unlock the locking policy that enforces no concurrent handling of messages from the same connection
	}
}
