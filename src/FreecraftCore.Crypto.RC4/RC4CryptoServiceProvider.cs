using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace FreecraftCore.Crypto.RC4
{
	/// <summary>
	/// Provides basic RC4 cipher services.
	/// </summary>
	public class RC4CryptoServiceProvider : IRC4CryptoServiceProvider
	{
		/// <summary>
		/// Managed Bouncy Castle RC4 engine being adapted.
		/// </summary>
		[NotNull]
		private RC4Engine internalRC4Engine { get; }

		/// <summary>
		/// Creates a new RC4 service with the provided <see cref="key"/>.
		/// </summary>
		/// <param name="key">The RC4 <see cref="BigInteger"/> key.</param>
		public RC4CryptoServiceProvider(BigInteger key, bool forEncryption)
			: this(key.ToCleanByteArray(), forEncryption)
		{

		}

		/// <summary>
		/// Creates a new RC4 service with the provided <see cref="key"/>.
		/// </summary>
		/// <param name="key">The RC4 key.</param>
		public RC4CryptoServiceProvider([NotNull] byte[] key, bool forEncryption)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			//Bouncy requires key initialization
			internalRC4Engine = new RC4Engine();
			internalRC4Engine.Init(forEncryption, new KeyParameter(key));
		}

		/// <inheritdoc />
		public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			//Let bouncy handle throwing in release
#if DEBUG || DEBUGBUILD
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (output == null) throw new ArgumentNullException(nameof(output));

			if (inOff < 0) throw new ArgumentOutOfRangeException(nameof(inOff));
			if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
			if (outOff < 0) throw new ArgumentOutOfRangeException(nameof(outOff));
#endif
			//Just delegate RC4 to bouncy
			internalRC4Engine.ProcessBytes(input, inOff, length, output, outOff);
		}

		/// <inheritdoc />
		public byte ReturnByte(byte input)
		{
			return internalRC4Engine.ReturnByte(input);
		}

		public void Dispose()
		{
			//Nothing is unmanaged but this may changed in the future depending on what RC4 implemnentation we use
		}
	}
}
