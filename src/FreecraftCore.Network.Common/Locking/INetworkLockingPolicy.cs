using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// The service contract for a locking policy for message handlers.
	/// To understand the need for such a mechanism review: https://youtu.be/hCsEHYwjqVE?t=2109
	/// where JAM developer talks about the ability of JAM and its handlers determining a lock policy
	/// for a specific Protocol. Protocols are a collection of JAM message definitions. We have no concept of a protocol.
	/// </summary>
	public interface INetworkLockingPolicyStrategy : IDisposable
	{
		/// <summary>
		/// Attempts to aquire an async lock based on the implemented locking policy.
		/// </summary>
		/// <param name="operationCode">The operation code of the incoming message.</param>
		/// <returns>An awaitable to wait for the lock aquisition.</returns>
		Task LockAsync(NetworkOperationCode operationCode);

		/// <summary>
		/// Attempts to aquire a lock based on the implemented locking policy.
		/// </summary>
		/// <param name="operationCode">The operation code of the incoming message.</param>
		void Lock(NetworkOperationCode operationCode);

		/// <summary>
		/// Releases a lock held based on the implemented locking policy.
		/// </summary>
		/// <param name="operationCode">The operation code of the incoming message.</param>
		void Unlock(NetworkOperationCode operationCode);
	}
}
