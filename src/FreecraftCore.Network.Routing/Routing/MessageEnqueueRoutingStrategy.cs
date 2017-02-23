using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Routing strategy that enqueues a message in a linked externally managed queue object.
	/// </summary>
	/// <typeparam name="TMessageType">The message type being routed.</typeparam>
	public class MessageEnqueueRoutingStrategy<TMessageType> : IMessageRoutingStrategy<TMessageType>
	{
		[NotNull]
		private ITargetBlock<TMessageType> LinkedMessageQueue { get; }

		public MessageEnqueueRoutingStrategy([NotNull] ITargetBlock<TMessageType> linkedMessageQueue)
		{
			if (linkedMessageQueue == null) throw new ArgumentNullException(nameof(linkedMessageQueue));

			LinkedMessageQueue = linkedMessageQueue;
		}

		/// <inheritdoc />
		public void RouteMessage(TMessageType message)
		{
			if(!LinkedMessageQueue.Post(message))
				throw new InvalidOperationException($"The buffer {nameof(LinkedMessageQueue)} in {GetType().FullName} did not route to queue.");
		}
	}
}
