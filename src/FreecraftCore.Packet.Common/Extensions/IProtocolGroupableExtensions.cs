using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Packet
{
	public static class IProtocolGroupableExtensions
	{
		/// <summary>
		/// Used to syncronize the reading of the map that maps types to protocolcodes.
		/// </summary>
		private static readonly ReaderWriterLockSlim LockObject = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

		/// <summary>
		/// Map containing the mappings from <see cref="Type"/> to the <see cref="ProtocolCode"/> that should be marked as metadata on the type.
		/// </summary>
		private static Dictionary<Type, ProtocolCode> TypeToProtocolMap { get; } = new Dictionary<Type, ProtocolCode>(ComputeProtocolMapSize());
		
		public static ProtocolCode GetProtocol(this IProtocolGroupable protocolGroupableObject)
		{
			//We enter a regular read lock because we assume we have the cached protocol type.
			LockObject.EnterReadLock();
			try
			{
				if (TypeToProtocolMap.ContainsKey(protocolGroupableObject.GetType()))
					return TypeToProtocolMap[protocolGroupableObject.GetType()];
			}
			finally
			{
				LockObject.ExitReadLock();
			}

			//It's mostly not going to reach this point more than once ever.

			//If we get to this point then we don't know the protocol
			//must write lock to write it
			LockObject.EnterWriteLock();
			try
			{
				//Recheck otherwise there is a race condition
				if (!TypeToProtocolMap.ContainsKey(protocolGroupableObject.GetType()))
					TypeToProtocolMap[protocolGroupableObject.GetType()] = ReflectForProtocolCode(protocolGroupableObject.GetType());
			}
			finally
			{
				LockObject.ExitWriteLock();
			}

			return GetProtocol(protocolGroupableObject);
		}

		/// <summary>
		/// Computes the max capacity the protocol map needs to support.
		/// </summary>
		/// <returns>About 1350.</returns>
		private static int ComputeProtocolMapSize()
		{
			int networkOpCodeSize = (int) Enum.GetValues(typeof(NetworkOperationCode)).Cast<NetworkOperationCode>().Last() + 1;
			int authOpCodeSize = (int) Enum.GetValues(typeof(AuthOperationCode)).Cast<AuthOperationCode>().Last() + 1;

			return networkOpCodeSize + authOpCodeSize;
		}

		private static ProtocolCode ReflectForProtocolCode(Type t)
		{
			ProtocolGroupingAttribute protocolAttribute = t.GetTypeInfo().GetCustomAttribute<ProtocolGroupingAttribute>();

			//If it's not marked with an attribute we should group it with none.
			if(protocolAttribute == null)
				return ProtocolCode.None;

			return protocolAttribute.Protocol;
		}
	}
}
