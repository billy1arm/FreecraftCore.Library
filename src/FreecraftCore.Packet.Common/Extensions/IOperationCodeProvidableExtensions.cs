using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	//TODO: Document
	public static class IOperationCodeProvidableExtensions
	{
		/// <summary>
		/// Used to syncronize the reading of the map that maps types to opcodes.
		/// </summary>
		private static readonly ReaderWriterLockSlim LockObject = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

		private static Dictionary<Type, AuthOperationCode> AuthenticationCodeMap { get; } = new Dictionary<Type, AuthOperationCode>((int)Enum.GetValues(typeof(AuthOperationCode)).Cast<AuthOperationCode>().Last() + 1);

		private static Dictionary<Type, NetworkOperationCode> NetworkOperationCodeMap { get; } = new Dictionary<Type, NetworkOperationCode>((int)Enum.GetValues(typeof(NetworkOperationCode)).Cast<NetworkOperationCode>().Last() + 1);

		public static NetworkOperationCode GetOperationCode(this IOperationCodeProvidable<NetworkOperationCode> provider)
		{
			//We enter a regular read lock because we assume we have the cached operation code
			LockObject.EnterReadLock();
			try
			{
				if (NetworkOperationCodeMap.ContainsKey(provider.GetType()))
					return NetworkOperationCodeMap[provider.GetType()];
			}
			finally
			{
				LockObject.ExitReadLock();
			}

			//It's mostly not going to reach this point more than once ever.

			//If we get to this point then we don't know the operation code
			//must write lock to write it
			LockObject.EnterWriteLock();
			try
			{
				//Recheck otherwise there is a race condition
				if (!NetworkOperationCodeMap.ContainsKey(provider.GetType()))
					NetworkOperationCodeMap[provider.GetType()] = ReflectForGameOperationCode(provider.GetType());
			}
			finally
			{
				LockObject.ExitWriteLock();
			}

			return GetOperationCode(provider);
		}

		public static AuthOperationCode GetOperationCode(this IOperationCodeProvidable<AuthOperationCode> provider)
		{
			//We enter a regular read lock because we assume we have the cached operation code
			LockObject.EnterReadLock();
			try
			{
				if (AuthenticationCodeMap.ContainsKey(provider.GetType()))
					return AuthenticationCodeMap[provider.GetType()];
			}
			finally
			{
				LockObject.ExitReadLock();
			}

			//It's mostly not going to reach this point more than once ever.

			//If we get to this point then we don't know the operation code
			//must write lock to write it
			LockObject.EnterWriteLock();
			try
			{
				//Recheck otherwise there is a race condition
				if (!AuthenticationCodeMap.ContainsKey(provider.GetType()))
					AuthenticationCodeMap[provider.GetType()] = ReflectForAuthenticationOperationCode(provider.GetType());
			}
			finally
			{
				LockObject.ExitWriteLock();
			}

			return GetOperationCode(provider);
		}

		private static AuthOperationCode ReflectForAuthenticationOperationCode(Type t)
		{
			if (t == null) throw new ArgumentNullException(nameof(t));

			AuthenticationPayloadAttribute attribute = t.GetTypeInfo().GetCustomAttribute<AuthenticationPayloadAttribute>();

			if (attribute == null)
				throw new InvalidOperationException($"{t.FullName} does not have a {nameof(AuthenticationPayloadAttribute)} that defines its {nameof(AuthOperationCode)}.");

			return attribute.OperationCode; throw new NotImplementedException();
		}

		private static NetworkOperationCode ReflectForGameOperationCode([NotNull] Type t)
		{
			if (t == null) throw new ArgumentNullException(nameof(t));

			GamePayloadOperationCodeAttribute attribute = t.GetTypeInfo().GetCustomAttribute<GamePayloadOperationCodeAttribute>();

			if (attribute == null)
				throw new InvalidOperationException($"{t.FullName} does not have a {nameof(GamePayloadOperationCodeAttribute)} that defines its {nameof(NetworkOperationCode)}.");

			return attribute.OperationCode;
		}
	}
}
