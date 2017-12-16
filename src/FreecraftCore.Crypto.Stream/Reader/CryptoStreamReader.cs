using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	public abstract class CryptoStreamReader<TStreamType> : CryptoStreamService, IWireStreamReaderStrategy
		where TStreamType : IWireStreamReaderStrategy
	{
		/// <summary>
		/// Decorated <see cref="IWireStreamReaderStrategy"/>.
		/// </summary>
		[NotNull]
		protected TStreamType Source { get; }

		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="source">The <see cref="TStreamType"/> to decorate.</param>
		/// <param name="sessionCrypto"></param>
		protected CryptoStreamReader([NotNull] TStreamType source, [NotNull] ISessionPacketCryptoService sessionCrypto)
			: base(sessionCrypto)
		{
			//We don't implement the reading ourselves because it could be a default behavior
			//our we may need network stream reading, which may block, or anything under the sun
			if (source == null) throw new ArgumentNullException(nameof(source));

			Source = source;
		}

		public virtual void Dispose()
		{
			//Do NOT dispose the source
			//We do NOT own it
		}

		//See doc for IWireStreamReaderStrategy
		public virtual byte ReadByte()
		{
			return ReadBytes(1)[0];
		}

		public virtual byte PeekByte()
		{
			return PeekBytes(1)[0];
		}

		public virtual byte[] ReadAllBytes()
		{
			return Source.ReadAllBytes();
		}

		public virtual byte[] ReadBytes(int count)
		{
			return Source.ReadBytes(count);
		}

		public virtual byte[] PeekBytes(int count)
		{
			return Source.PeekBytes(count);
		}
	}
}
