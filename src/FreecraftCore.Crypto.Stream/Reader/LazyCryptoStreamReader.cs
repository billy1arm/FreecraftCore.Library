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
	public class LazyCryptoStreamReader : CryptoStreamReader
	{
		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="source">The <see cref="IWireStreamReaderStrategy"/> to decorate.</param>
		/// <param name="sessionCrypto"></param>
		public LazyCryptoStreamReader([NotNull] IWireStreamReaderStrategy source, [NotNull] ISessionPacketCryptoService sessionCrypto)
			: base(source, sessionCrypto)
		{

		}

		public override byte ReadByte()
		{
			return SessionCryptoService.ReturnByte(base.ReadByte());
		}

		public override byte PeekByte()
		{
			return SessionCryptoService.ReturnByte(base.PeekByte());
		}

		public override byte[] ReadAllBytes()
		{
			return SessionCryptoService.ProcessBytes(base.ReadAllBytes(), 0);
		}

		public override byte[] ReadBytes(int count)
		{
			return SessionCryptoService.ProcessBytes(base.ReadBytes(count), 0);
		}

		public override byte[] PeakBytes(int count)
		{
			return SessionCryptoService.ProcessBytes(base.PeakBytes(count), 0);
		}
	}
}
