using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Factory that can create handlers.
	/// </summary>
	public interface IMessageHandlerFactory
	{
		/// <summary>
		/// Creates the handler provided as the type argument.
		/// </summary>
		/// <typeparam name="THandlerType">The handlers type.</typeparam>
		/// <returns>A non-null handler instance.</returns>
		[Pure]
		[NotNull]
		THandlerType Create<THandlerType>();

		/// <summary>
		/// Creates the handler provided as the type argument.
		/// </summary>
		/// <returns>A non-null handler instance.</returns>
		[Pure]
		[NotNull]
		object Create([NotNull] Type handlerType);
	}
}
