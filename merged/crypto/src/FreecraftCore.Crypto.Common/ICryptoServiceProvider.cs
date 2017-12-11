using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Provides encryption/decryption services.
	/// (Based on Bouncy Castle's ICipherStream)
	/// </summary>
	public interface ICryptoServiceProvider
	{
		//Based on Bouncy Castle's IStreamCipher API
		/// <summary>
		/// Processes a block of bytes through an RC4 cipher.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="inOff">The offset to start at in the input buffer.</param>
		/// <param name="length">The length of the input buffer.</param>
		/// <param name="output">The output buffer.</param>
		/// <param name="outOff">Output buffer offset.</param>
		/// <exception cref="ArgumentNullException">Throws if any buffer is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Throws if any offset or length is less than 0.</exception>
		[Pure]
		void ProcessBytes([NotNull] byte[] input, int inOff, int length, [NotNull] byte[] output, int outOff);

		/// <summary>
		/// Runs a single byte through the RC4 cipher.
		/// </summary>
		/// <param name="input">The byte.</param>
		/// <returns>A byte run through the RC4 cipher.</returns>
		[Pure]
		byte ReturnByte(byte input);
	}
}
