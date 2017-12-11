using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Contract for types that can build contexts.
	/// </summary>
	/// <typeparam name="TMessageType">The context type.</typeparam>
	public interface IMessageContextBuilder<out TMessageType> //We don't constrain here to make this least derived type more flexible
	{
		/// <summary>
		/// Indicates if the <typeparamref name="TMessageType"/> has been constructed.
		/// </summary>
		bool isBuilt { get; }

		/// <summary>
		/// Builds the <typeparamref name="TMessageType"/> object.
		/// </summary>
		/// <returns>Returns a non-null representation of the <typeparam name="TMessageType"> based on the builder's contained data.</typeparam></returns>
		[NotNull]
		TMessageType Build();
	}
}
