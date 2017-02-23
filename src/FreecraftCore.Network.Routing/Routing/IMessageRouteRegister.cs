using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Contract for services that are able to register a routing strategy protocol pairing.
	/// </summary>
	/// <typeparam name="TMessageType">The message type being routed.</typeparam>
	public interface IMessageRouteRegister<out TMessageType>
	{
		/// <summary>
		/// Registers a route for the provide <see cref="ProtocolCode"/> using the
		/// <see cref="router"/> routing strategy provided.
		/// </summary>
		/// <param name="router">The routing strategy to use.</param>
		/// <param name="code">The protocol code to map to.</param>
		void RegisterRoute([NotNull] IMessageRoutingStrategy<TMessageType> router, ProtocolCode code);
	}
}
