using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto.RC4
{
	public class RC4DropNCryptoServiceProvider : IRC4CryptoServiceProvider
	{
		/// <summary>
		/// Decorated <see cref="IRC4CryptoServiceProvider"/>.
		/// </summary>
		[NotNull]
		private IRC4CryptoServiceProvider internalRC4Engine { get; }

		/// <summary>
		/// Creates a new RC4 service with the provided <see cref="key"/> and follows
		/// RC4-Drop[n].
		/// </summary>
		/// <param name="key">The RC4 <see cref="BigInteger"/> key.</param>
		/// <param name="n">The n value to be used in RC4-Drop[n]</param>
		public RC4DropNCryptoServiceProvider(BigInteger key, int n, bool forEncryption)
			: this(key.ToCleanByteArray(), n, forEncryption)
		{

		}

		/// <summary>
		/// Creates a new RC4 service with the provided <see cref="key"/> and follows
		/// RC4-Drop[n].
		/// </summary>
		/// <param name="key">The RC4 key.</param>
		/// <param name="n">The n value to be used in RC4-Drop[n]</param>
		public RC4DropNCryptoServiceProvider([NotNull] byte[] key, int n, bool forEncryption)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			internalRC4Engine = new RC4CryptoServiceProvider(key, forEncryption);

			//TODO: Create a shared n buffer to be used. Only matters for server. Client only makes one usually.
			byte[] dropNByteArray = new byte[n];

			//Once initialized we need to drop the first n bytes.
			//This prevents a common RC4 exploit that can determine key values otherwise.
			internalRC4Engine.ProcessBytes(dropNByteArray, 0, n, dropNByteArray, 0);
		}

		public void Dispose()
		{
			internalRC4Engine.Dispose();
		}

		/// <inheritdoc />
		public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			//We can save some perf letting the engine we delegate to check parameters.
			internalRC4Engine.ProcessBytes(input, inOff, length, output, outOff);
		}

		/// <inheritdoc />
		public byte ReturnByte(byte input)
		{
			return internalRC4Engine.ReturnByte(input);
		}
	}
}
