using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Metadata marker for message handler classes.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MessageHandlerAttribute : Attribute
	{
		/// <summary>
		/// Indicates the operation code that the handler is responsible for.
		/// </summary>
		public Type PayloadType { get; }

		public MessageHandlerAttribute([NotNull] Type payloadType)
		{
			if (payloadType == null) throw new ArgumentNullException(nameof(payloadType));

			PayloadType = payloadType;
		}
	}
}
