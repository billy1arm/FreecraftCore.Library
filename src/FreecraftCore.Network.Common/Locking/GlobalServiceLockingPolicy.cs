using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Locking policy that syncronizes locking globally within the service it's being used in.
	/// </summary>
	public class GlobalServiceLockingPolicy : INetworkLockingPolicyStrategy
	{
		/// <summary>
		/// Async-capable semaphore (1) that acts as a mutex.
		/// </summary>
		private SemaphoreSlim LockingObject { get; } = new SemaphoreSlim(1,1);

		/// <inheritdoc />
		public Task LockAsync(NetworkOperationCode operationCode)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void Lock(NetworkOperationCode operationCode)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void Unlock(NetworkOperationCode operationCode)
		{
			throw new NotImplementedException();
		}

		//Locking policies must be new() able.
		/// <summary>
		/// 
		/// </summary>
		public GlobalServiceLockingPolicy()
		{

		}

		/// <inheritdoc />
		public void Dispose()
		{
			LockingObject.Dispose();
		}
	}
}
