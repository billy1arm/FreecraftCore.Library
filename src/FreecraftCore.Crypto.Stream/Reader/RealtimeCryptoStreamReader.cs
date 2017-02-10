using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Provides a realtime crypto service implementation of the
	/// wire reader strategy. Processes the stream chunks at request as opposed to all at once.
	/// </summary>
	public class RealtimeCryptoStreamReader : IWireStreamReaderStrategy, IDisposable
	{
		/// <summary>
		/// Decorated <see cref="IWireStreamReaderStrategy"/>.
		/// </summary>
		[NotNull]
		private IWireStreamReaderStrategy Source { get; }

		/// <summary>
		/// Provided crypto service.
		/// </summary>
		[NotNull]
		private ISessionPacketCryptoService SessionCryptoService { get; }

		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="source">The <see cref="IWireStreamReaderStrategy"/> to decorate.</param>
		/// <param name="sessionCrypto"></param>
		public RealtimeCryptoStreamReader([NotNull] IWireStreamReaderStrategy source, [NotNull] ISessionPacketCryptoService sessionCrypto)
		{
			//We don't implement the reading ourselves because it could be a default behavior
			//our we may need network stream reading, which may block, or anything under the sun
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (SessionCryptoService == null) throw new ArgumentNullException(nameof(SessionCryptoService));

			Source = source;
			SessionCryptoService = sessionCrypto;
		}

		public byte ReadByte()
		{
			return SessionCryptoService.ReturnByte(Source.ReadByte());
		}

		public byte PeekByte()
		{
			return SessionCryptoService.ReturnByte(Source.PeekByte());
		}

		public byte[] ReadAllBytes()
		{
			return SessionCryptoService.ProcessBytes(Source.ReadAllBytes(), 0);
		}

		public byte[] ReadBytes(int count)
		{
			return SessionCryptoService.ProcessBytes(Source.ReadBytes(count), 0);
		}

		public byte[] PeakBytes(int count)
		{
			return SessionCryptoService.ProcessBytes(Source.PeakBytes(count), 0);
		}

		public void Dispose()
		{
			//Do not dispose the crypto service
			//We don't own it
			Source.Dispose();
		}
	}
}
