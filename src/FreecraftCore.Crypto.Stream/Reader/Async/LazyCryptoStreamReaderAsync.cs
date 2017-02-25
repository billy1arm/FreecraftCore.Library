using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	public class LazyCryptoStreamReaderAsync : LazyCryptoStreamReader<IWireStreamReaderStrategyAsync>, IWireStreamReaderStrategyAsync
	{
		/// <inheritdoc />
		public LazyCryptoStreamReaderAsync([NotNull] IWireStreamReaderStrategyAsync source, [NotNull] ISessionPacketCryptoService sessionCrypto) 
			: base(source, sessionCrypto)
		{

		}

		/// <inheritdoc />
		public async Task<byte> ReadByteAsync()
		{
			return (await ReadBytesAsync(1))[0];
		}

		/// <inheritdoc />
		public async Task<byte> PeekByteAsync()
		{
			return (await PeekBytesAsync(1))[0];
		}

		/// <inheritdoc />
		public async Task<byte[]> ReadAllBytesAsync()
		{
			return SessionCryptoService.ProcessBytes(await Source.ReadAllBytesAsync(), 0);
		}

		/// <inheritdoc />
		public async Task<byte[]> ReadBytesAsync(int count)
		{
			return SessionCryptoService.ProcessBytes(await Source.ReadBytesAsync(count), 0);
		}

		/// <inheritdoc />
		public async Task<byte[]> PeekBytesAsync(int count)
		{
			return SessionCryptoService.ProcessBytes(await Source.PeekBytesAsync(count), 0);

		}
	}
}
