using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Provides a crypto service implementation of the
	/// wire reader strategy. Processes the stream once, all at once, for the designated size.
	/// This yields better performance than the realtime which does chunk by chunk.
	/// This reader may not immediately read the stream. Will read it as soon as it is needed.
	/// </summary>
	public class OneTimeCryptoStreamReader<TStreamType> : CryptoStreamReader<TStreamType>
		where TStreamType : class, IWireStreamReaderStrategy
	{
		/// <summary>
		/// Creates a realtime crypto stream reader.
		/// </summary>
		/// <param name="source">The <see cref="IWireStreamReaderStrategy"/> to decorate.</param>
		/// <param name="sessionCrypto"></param>
		/// <param name="count">The amount of bytes to be read immediately.</param>
		public OneTimeCryptoStreamReader([NotNull] TStreamType source, [NotNull] ISessionPacketCryptoService sessionCrypto, int count)
			: base(new DefaultStreamReaderStrategy(new LazyCryptoStreamReader<TStreamType>(source, sessionCrypto).ReadBytes(count)) as TStreamType, sessionCrypto) //Reads count many bytes and decrypts and creates a default reader around it
		{
			if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
		}

		/// <inheritdoc />
		public override void Dispose()
		{
			//Call base
			base.Dispose();

			//We own this stream so we should dipose it
			Source.Dispose();
		}
	}
}
