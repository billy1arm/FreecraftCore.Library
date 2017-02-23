using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Extensions for the <see cref="RouteConfigurationService"/> class.
	/// </summary>
	public static class RouteConfigurationServiceExtensions
	{
		/// <summary>
		/// Registers the provided configured options with the <see cref="IMessageRouteRegister{TMessageType}"/>.
		/// </summary>
		/// <typeparam name="TMessageType">The message type.</typeparam>
		/// <param name="register">The registeration service.</param>
		/// <param name="options">The configuration options.</param>
		/// <returns>Fluent register.</returns>
		public static IMessageRouteRegister<TMessageType> WithOptions<TMessageType>(this IMessageRouteRegister<TMessageType> register, [NotNull] Action<RoutingRegisterOptions<TMessageType>> options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));

			//Provide the register to the options
			options(new RoutingRegisterOptions<TMessageType>(register));

			return register;
		}
	}
}
