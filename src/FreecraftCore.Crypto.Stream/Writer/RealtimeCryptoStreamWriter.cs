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
	public class RealtimeCryptoStreamWriter : CryptoStreamWriter
	{
		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="dest">The <see cref="IWireStreamWriterStrategy"/> to decorate.</param>
		/// <param name="sessionCrypto"></param>
		public RealtimeCryptoStreamWriter([NotNull] IWireStreamWriterStrategy dest, [NotNull] ISessionPacketCryptoService sessionCrypto)
			: base(dest, sessionCrypto)
		{

		}

		public override void Write(byte[] data)
		{
			Dest.Write(SessionCryptoService.ProcessBytes(data, 0));
		}

		public override void Write(byte[] data, int offset, int count)
		{
			Dest.Write(SessionCryptoService.ProcessBytes(data, offset, count), offset, count);
		}

		public override void Write(byte data)
		{
			Dest.Write(SessionCryptoService.ReturnByte(data));
		}

		public override byte[] GetBytes()
		{
			return Dest.GetBytes();
		}
	}
}
