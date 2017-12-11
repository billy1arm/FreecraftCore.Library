using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Crypto service that produces session authentication hashes.
	/// </summary>
	public class WoWSessionDigestCryptoProvider : IDisposable
	{
		/// <summary>
		/// Produces a hash of the provided parameters as well as the required components for
		/// session authentication.
		/// </summary>
		/// <param name="userName">Username of the account.</param>
		/// <param name="ourSeed">The random seed we generated.</param>
		/// <param name="authenticationSeed">The authentication seed for the session.</param>
		/// <param name="sessionKey">The SRP6 session key.</param>
		/// <returns>The SHA1 session authentication hash.</returns>
		public byte[] ProduceAuthenticationHash([NotNull] string userName, BigInteger ourSeed, uint authenticationSeed, BigInteger sessionKey)
		{
			if (string.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", nameof(userName));

			using (SHA1 hasher = SHA1.Create())
			{
				//Based on Jackpoz's bot implementation
				//Creates a hash for our random, the username we've logged in as 
				//and the SRP6 session key.
				return hasher.ComputeHash(Encoding.ASCII.GetBytes(userName.ToUpper())
					.Concat(BitConverter.GetBytes(default(uint)))
					.Concat(BitConverter.GetBytes((uint) ourSeed))
					.Concat(BitConverter.GetBytes(authenticationSeed))
					.Concat(sessionKey.ToCleanByteArray())
					.ToArray());
			}
		}

		/// <summary>
		/// Generates the Authentication seed for session authentication.
		/// </summary>
		/// <returns>An authentication seed.</returns>
		public BigInteger GenerateAuthenticationSeed()
		{
			byte[] bytes = new byte[4];

			RandomNumberGenerator.Create().GetBytes(bytes);
			return bytes.ToBigInteger();
		}

		/// <inheritdoc />
		public void Dispose()
		{
			//Don't need to do anything.
		}
	}
}
