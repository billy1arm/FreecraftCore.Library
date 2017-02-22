using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Locking policy that syncronizes locking globally within the protocol it's being used in.
	/// </summary>
	public class ProtocolLockingPolicy : INetworkLockingPolicyStrategy
	{
		/// <summary>
		/// Async-capable semaphore (1) that acts as a mutex.
		/// </summary>
		private SemaphoreSlim LockingObject { get; } = new SemaphoreSlim(1,1);

		/// <inheritdoc />
		public async Task LockAsync(NetworkOperationCode operationCode)
		{
			//No matter the opcode we should lock
			await LockingObject.WaitAsync();
		}

		/// <inheritdoc />
		public void Lock(NetworkOperationCode operationCode)
		{
			//No matter the opcode we should lock
			LockingObject.Wait();
		}

		/// <inheritdoc />
		public void Unlock(NetworkOperationCode operationCode)
		{
			//No matter the opcode we should release back
			LockingObject.Release(1);
		}

		//Locking policies must be new() able.
		public ProtocolLockingPolicy()
		{

		}

		/// <inheritdoc />
		public void Dispose()
		{
			LockingObject.Dispose();
		}
	}
}
