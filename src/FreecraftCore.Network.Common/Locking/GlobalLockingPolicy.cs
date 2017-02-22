using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Locking policy that syncronizes locking globally.
	/// </summary>
	public class GlobalLockingPolicy : INetworkLockingPolicyStrategy
	{
		/// <summary>
		/// Async-capable semaphore (1) that acts as a mutex.
		/// (static mutex for global locking)
		/// </summary>
		private static SemaphoreSlim LockingObject { get; } = new SemaphoreSlim(1,1);

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
		public GlobalLockingPolicy()
		{

		}

		/// <inheritdoc />
		public void Dispose()
		{
			//Do not dispose the static mutex
			//we do not own it
		}
	}
}
