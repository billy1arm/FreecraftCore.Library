using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Provides a realtime crypto service implementation of the
	/// wire reader strategy. Processes the stream chunks at request as opposed to all at once.
	/// </summary>
	public class LazyCryptoStreamReader<TReaderType> : CryptoStreamReader<TReaderType>
		where TReaderType : IWireStreamReaderStrategy
	{
		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="source">The <see cref="IWireStreamReaderStrategy"/> to decorate.</param>
		/// <param name="sessionCrypto"></param>
		public LazyCryptoStreamReader([NotNull] TReaderType source, [NotNull] ISessionPacketCryptoService sessionCrypto)
			: base(source, sessionCrypto)
		{

		}

		public override byte[] ReadAllBytes()
		{
			return SessionCryptoService.ProcessBytes(Source.ReadAllBytes(), 0);
		}

		public override byte[] ReadBytes(int count)
		{
			return SessionCryptoService.ProcessBytes(Source.ReadBytes(count), 0);
		}

		public override byte[] PeekBytes(int count)
		{
			return SessionCryptoService.ProcessBytes(Source.PeekBytes(count), 0);
		}
	}
}
