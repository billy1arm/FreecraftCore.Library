using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Base type for crypto streams services.
	/// </summary>
	public abstract class CryptoStreamService
	{
		/// <summary>
		/// Provided crypto service.
		/// </summary>
		[NotNull]
		protected ISessionPacketCryptoService SessionCryptoService { get; }

		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="sessionCryptoService"></param>
		protected CryptoStreamService([NotNull] ISessionPacketCryptoService sessionCryptoService)
		{
			if (sessionCryptoService == null) throw new ArgumentNullException(nameof(sessionCryptoService));

			SessionCryptoService = sessionCryptoService;
		}
	}
}
