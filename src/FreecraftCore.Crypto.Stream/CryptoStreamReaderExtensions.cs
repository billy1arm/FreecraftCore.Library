using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto.Stream
{
	public static class CryptoStreamReaderExtensions
	{
		/// <summary>
		/// Decorates the stream with async peek only functionality.
		/// This forces the decorated reader to peek for every peek or read.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="cryptoService"></param>
		/// <returns></returns>
		public static IWireStreamReaderStrategy WithLazyCrypto<TReaderType>(this TReaderType reader, [NotNull] ISessionPacketCryptoService cryptoService)
			where TReaderType : IWireStreamReaderStrategy
		{
			if (cryptoService == null) throw new ArgumentNullException(nameof(cryptoService));

			return new LazyCryptoStreamReader<TReaderType>(reader, cryptoService);
		}

		/// <summary>
		/// Decorates the stream with async peek only functionality.
		/// This forces the decorated reader to peek for every peek or read.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="cryptoService"></param>
		/// <returns></returns>
		public static IWireStreamReaderStrategyAsync WithLazyCryptoAsync<TReaderType>(this TReaderType reader, [NotNull] ISessionPacketCryptoService cryptoService)
			where TReaderType : IWireStreamReaderStrategy, IWireStreamReaderStrategyAsync
		{
			if (cryptoService == null) throw new ArgumentNullException(nameof(cryptoService));

			return new LazyCryptoStreamReaderAsync(reader, cryptoService);
		}
	}
}
