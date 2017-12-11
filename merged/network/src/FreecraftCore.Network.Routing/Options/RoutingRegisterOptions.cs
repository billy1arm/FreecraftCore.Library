using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	public sealed class RoutingRegisterOptions<TMessageType>
	{
		[NotNull]
		private IMessageRouteRegister<TMessageType> Register { get; }

		public RoutingRegisterOptions([NotNull] IMessageRouteRegister<TMessageType> register)
		{
			if (register == null) throw new ArgumentNullException(nameof(register));

			Register = register;
		}

		public RoutingRegisterOptions<TMessageType> WithRoute<TProtocolType, TRoutingStrategy>()
			where TProtocolType : IMessageProtocolStrategy, new()
			where TRoutingStrategy : IMessageRoutingStrategy<TMessageType>, new()
		{
			//Register using the strategies provided
			Register.RegisterRoute(new TRoutingStrategy(), new TProtocolType().MessageProtocol);

			//for fluent building
			return this;
		}

		public RoutingRegisterOptions<TMessageType> WithRoute<TProtocolType>(IMessageRoutingStrategy<TMessageType> routingStrategy)
			where TProtocolType : IMessageProtocolStrategy, new()
		{
			//Register using the strategies provided
			Register.RegisterRoute(routingStrategy, new TProtocolType().MessageProtocol);

			//for fluent building
			return this;
		}

		public RoutingRegisterOptions<TMessageType> WithRoute(IMessageRoutingStrategy<TMessageType> routingStrategy, ProtocolCode code)
		{
			//Register using the strategies provided
			Register.RegisterRoute(routingStrategy, code);

			//for fluent building
			return this;
		}
	}
}
