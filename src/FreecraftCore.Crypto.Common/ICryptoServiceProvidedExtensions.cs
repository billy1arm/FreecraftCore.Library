using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	public static class ICryptoServiceProvidedExtensions
	{
		/// <summary>
		/// Processes a block of bytes through an RC4 cipher in place.
		/// </summary>
		/// <param name="provider">The crypto provider.</param>
		/// <param name="input">The input buffer.</param>
		/// <param name="inOff">The offset to start at in the input buffer.</param>
		/// <param name="length">The length of the input buffer.</param>
		/// <exception cref="ArgumentNullException">Throws if any buffer is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Throws if any offset or length is less than 0.</exception>
		public static byte[] ProcessBytes(this ICryptoServiceProvider provider, [NotNull] byte[] input, int inOff, int length)
		{
			provider.ProcessBytes(input, inOff, length, input, inOff);

			return input;
		}

		/// <summary>
		/// Processes a block of bytes through an RC4 cipher in place.
		/// </summary>
		/// <param name="provider">The crypto provider.</param>
		/// <param name="input">The input buffer.</param>
		/// <param name="inOff">The offset to start at in the input buffer.</param>
		/// <exception cref="ArgumentNullException">Throws if any buffer is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Throws if any offset or length is less than 0.</exception>
		public static byte[] ProcessBytes(this ICryptoServiceProvider provider, [NotNull] byte[] input, int inOff)
		{
			provider.ProcessBytes(input, inOff, input.Length, input, inOff);

			return input;
		}

		//TODO: These may not be setup properly
		/// <summary>
		/// Processes a block of bytes through an RC4 cipher in place.
		/// </summary>
		/// <param name="provider">The crypto provider.</param>
		/// <param name="input">The input buffer.</param>
		/// <param name="inOff">The offset to start at in the input buffer.</param>
		/// <param name="length">The length of the input buffer.</param>
		/// <exception cref="ArgumentNullException">Throws if any buffer is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Throws if any offset or length is less than 0.</exception>
		public static byte[] ProcessBytesToNewBuffer(this ICryptoServiceProvider provider, [NotNull] byte[] input, int inOff, int length)
		{
			byte[] outputBuffer = new byte[length];

			provider.ProcessBytes(input, inOff, length, outputBuffer, inOff);

			return outputBuffer;
		}

		/// <summary>
		/// Processes a block of bytes through an RC4 cipher in place.
		/// </summary>
		/// <param name="provider">The crypto provider.</param>
		/// <param name="input">The input buffer.</param>
		/// <param name="inOff">The offset to start at in the input buffer.</param>
		/// <exception cref="ArgumentNullException">Throws if any buffer is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Throws if any offset or length is less than 0.</exception>
		public static byte[] ProcessBytesToNewBuffer(this ICryptoServiceProvider provider, [NotNull] byte[] input, int inOff)
		{
			byte[] outputBuffer = new byte[input.Length - inOff];

			provider.ProcessBytes(input, inOff, input.Length - inOff, outputBuffer, inOff);

			return outputBuffer;
		}
	}
}
