using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using GladNet;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Similar to JAM route config where it routes messages based on protocol.
	/// </summary>
	/// <typeparam name="TOperationCode">The operation code of the message.</typeparam>
	/// <typeparam name="THeaderType">The header type of the message.</typeparam>
	/// <typeparam name="TPayloadType">The type of the payload.</typeparam>
	public class RouteConfigurationService<TOperationCode, THeaderType, TPayloadType> : IMessageRouteRegister<NetworkIncomingMessage<TPayloadType>>, IMessageRoutingStrategy<NetworkIncomingMessage<TPayloadType>> //it's also technically a message routing strategy
		where TOperationCode : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCode>
		where TPayloadType : class, IMessageVerifyable, IProtocolGroupable //also add contraint that the payload be routegroupable
	{
		[NotNull]
		private Dictionary<ProtocolCode, IMessageRoutingStrategy<NetworkIncomingMessage<TPayloadType>>> RoutingMap { get; }

		/// <summary>
		/// Creates a new route configuring service.
		/// </summary>
		public RouteConfigurationService()
		{
			//We need this route map to help map incoming protocols to their routing strategy.
			RoutingMap = new Dictionary<ProtocolCode, IMessageRoutingStrategy<NetworkIncomingMessage<TPayloadType>>>();
		}

		//TODO: Thread safety
		/// <inheritdoc />
		public void RegisterRoute([NotNull] IMessageRoutingStrategy<NetworkIncomingMessage<TPayloadType>> router, ProtocolCode code)
		{
			if (router == null) throw new ArgumentNullException(nameof(router));

			if (!Enum.IsDefined(typeof(ProtocolCode), code))
				throw new ArgumentOutOfRangeException(nameof(code), "Value should be defined in the ProtocolCode enum.");
			
			//Just add the route to the map
			RoutingMap.Add(code, router);
		}

		//TODO: Thread safety
		/// <inheritdoc />
		public async Task RouteMessage(NetworkIncomingMessage<TPayloadType> message)
		{
			//Check the protocol coming in on the message
			ProtocolCode code = message.Payload.GetProtocol();
			
			if(!RoutingMap.ContainsKey(code))
				throw new InvalidOperationException($"{GetType().FullName} does not contain a registered routing strategy for Protocol: {code.ToString()}.");

			await RoutingMap[code].RouteMessage(message);
		}
	}
}
