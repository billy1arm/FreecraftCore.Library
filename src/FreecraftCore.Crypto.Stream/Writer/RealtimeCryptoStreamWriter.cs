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
	/// wire writer strategy. Processes the stream chunks at request as opposed to all at once.
	/// </summary>
	public class RealtimeCryptoStreamWriter : IWireStreamWriterStrategy, IDisposable
	{
		/// <summary>
		/// Decorated <see cref="IWireStreamReaderStrategy"/>.
		/// </summary>
		[NotNull]
		private IWireStreamWriterStrategy Dest { get; }

		/// <summary>
		/// Provided crypto service.
		/// </summary>
		[NotNull]
		private ISessionPacketCryptoService SessionCryptoService { get; }

		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="dest">The <see cref="IWireStreamWriterStrategy"/> to decorate.</param>
		/// <param name="sessionCrypto"></param>
		public RealtimeCryptoStreamWriter([NotNull] IWireStreamWriterStrategy dest, [NotNull] ISessionPacketCryptoService sessionCrypto)
		{
			//We don't implement the reading ourselves because it could be a default behavior
			//our we may need network stream reading, which may block, or anything under the sun
			if (dest == null) throw new ArgumentNullException(nameof(dest));
			if (SessionCryptoService == null) throw new ArgumentNullException(nameof(SessionCryptoService));

			Dest = dest;
			SessionCryptoService = sessionCrypto;
		}

		public void Dispose()
		{
			//Do not dispose the crypto service
			//We don't own it
			Dest.Dispose();
		}

		public void Write(byte[] data)
		{
			Dest.Write(SessionCryptoService.ProcessBytes(data, 0));
		}

		public void Write(byte[] data, int offset, int count)
		{
			
			Dest.Write(SessionCryptoService.ProcessBytes(data, offset, count), offset, count);
		}

		public void Write(byte data)
		{
			Dest.Write(SessionCryptoService.ReturnByte(data));
		}

		public byte[] GetBytes()
		{
			return Dest.GetBytes();
		}
	}
}
