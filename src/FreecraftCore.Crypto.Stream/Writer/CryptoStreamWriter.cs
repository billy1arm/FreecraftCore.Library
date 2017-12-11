using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	public abstract class CryptoStreamWriter : CryptoStreamService, IWireStreamWriterStrategy
	{
		/// <summary>
		/// Decorated <see cref="IWireStreamReaderStrategy"/>.
		/// </summary>
		[NotNull]
		protected IWireStreamWriterStrategy Dest { get; }

		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="dest">The <see cref="IWireStreamWriterStrategy"/> to decorate.</param>
		/// <param name="sessionCrypto"></param>
		protected CryptoStreamWriter([NotNull] IWireStreamWriterStrategy dest, [NotNull] ISessionPacketCryptoService sessionCrypto)
			: base(sessionCrypto)
		{
			//We don't implement the writing ourselves because it could be a default behavior
			//our we may need network stream reading, which may block, or anything under the sun
			if (dest == null) throw new ArgumentNullException(nameof(dest));

			Dest = dest;
		}

		public virtual void Dispose()
		{
			//Do NOT dispose the source
			//We do NOT own it
		}

		//See doc for IWireStreamReaderStrategy
		public virtual void Write(byte[] data)
		{
			Dest.Write(data);
		}

		public virtual void Write(byte[] data, int offset, int count)
		{
			Dest.Write(data, offset, count);
		}

		public virtual void Write(byte data)
		{
			Dest.Write(data);
		}

		public virtual byte[] GetBytes()
		{
			return Dest.GetBytes();
		}
	}
}
