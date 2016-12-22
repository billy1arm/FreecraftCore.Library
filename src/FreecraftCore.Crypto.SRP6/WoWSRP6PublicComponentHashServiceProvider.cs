using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Provides SRP6 public component hashing service.
	/// Review: u on http://srp.stanford.edu/design.html
	/// Or this diagram: https://www.codeproject.com/articles/1082676/communication-using-secure-remote-password-protoco
	/// </summary>
	public class WoWSRP6PublicComponentHashServiceProvider : IDisposable
	{
		public byte[] Hash(BigInteger componentOne, BigInteger componentTwo)
		{
			return Hash(componentOne.ToCleanByteArray(), componentTwo.ToCleanByteArray());
		}

		public byte[] Hash(byte[] componentOne, byte[] componentTwo)
		{
			//WoW expects non-secure SHA1 hashing. SRP6 is deprecated too. We need to do it anyway
			using (SHA1 shaProvider = SHA1.Create())
			{
				//See Jackpoz's Combine function
				return shaProvider.ComputeHash(Enumerable.Concat(componentOne, componentTwo).ToArray());
			}
		}

		public byte[] Hash(byte[] bytes)
		{
			//WoW expects non-secure SHA1 hashing. SRP6 is deprecated too. We need to do it anyway
			using (SHA1 shaProvider = SHA1.Create())
			{
				return shaProvider.ComputeHash(bytes);
			}
		}

		public BigInteger HashSessionKey(BigInteger key)
		{
			//See: https://github.com/vermie/MangosClient/blob/master/Client/Authentication/Network/AuthSocket.cs for information
			//I don't know what hashing algorithm this is but SRP6 calls for
			//hashing the session key with a one-way hashing algorithm
			//Maybe this is a custom Blizzrd hashing algorithm?

			byte[] keyHash;
			byte[] sData = key.ToCleanByteArray();
			if (sData.Length < 32)
			{
				var tmpBuffer = new byte[32];
				Buffer.BlockCopy(sData, 0, tmpBuffer, 32 - sData.Length, sData.Length);
				sData = tmpBuffer;
			}
			byte[] keyData = new byte[40];
			byte[] temp = new byte[16];

			// take every even indices byte, hash, store in even indices
			for (int i = 0; i < 16; ++i)
				temp[i] = sData[i * 2];

			using (SHA1 shaProvider = SHA1.Create())
			{
				keyHash = shaProvider.ComputeHash(temp);
			}

			for (int i = 0; i < 20; ++i)
				keyData[i * 2] = keyHash[i];

			// do the same for odd indices
			for (int i = 0; i < 16; ++i)
				temp[i] = sData[i * 2 + 1];

			using (SHA1 shaProvider = SHA1.Create())
			{
				keyHash = shaProvider.ComputeHash(temp);
			}

			for (int i = 0; i < 20; ++i)
				keyData[i * 2 + 1] = keyHash[i];

			return new BigInteger(keyData);
		}

		/// <summary>
		/// Computes M/M1 for the SRP6 protocol.
		/// M/M1 is defined as H(H(N) xor H(g), H(I), s, A, B, K)
		/// </summary>
		/// <param name="g">Generator.</param>
		/// <param name="N">Modulus</param>
		/// <param name="userName">Unhashed username string.</param>
		/// <param name="salt">Provided challenge salt.</param>
		/// <param name="A">First public component.</param>
		/// <param name="B">Second public component.</param>
		/// <param name="unhashedSessionKey">Unhashed session key (S) which is used to build K = H(S)</param>
		/// <returns>M/M1 byte array.</returns>
		public byte[] ComputeSRP6M1(BigInteger g, BigInteger N, string userName, byte[] salt, BigInteger A, BigInteger B, BigInteger unhashedSessionKey)
		{
			//Ok, so the client is technically the host in this situation
			//The SRP6 protocol suggests that we provide a proof such as
			//M = H(H(N) xor H(g), H(I), s, A, B, K) to prove we know K, I think.

			//From Mangos client: https://github.com/vermie/MangosClient/blob/master/Client/Authentication/Network/AuthSocket.cs

			byte[] NHash = GenerateHashFor(N);
			byte[] gHash = GenerateHashFor(g);

			//Hash the Username provided
			byte[] hashedUserName = GenerateHashFor(Encoding.ASCII.GetBytes(userName));

			//TODO: Why do we use this hashing algorithm? Is this custom Blizzard hash?
			//Hash session key (K)
			//K = H(S)
			BigInteger hashedSessionKey = HashSessionKey(unhashedSessionKey);

			//XOR the NHash by with the gHash
			for (int i = 0; i < NHash.Length; i++)
				NHash[i] ^= gHash[i];

			//SRP6 suggests M be the hash of NHash xor'd, hash of identity, salt, public components and hashed session key
			//H(H(N) xor H(g), H(I), s, A, B, H(S))
			return GenerateHashFor(NHash.Concat(hashedUserName)
				.Concat(salt)
				.Concat(A.ToCleanByteArray()) 
				.Concat(B.ToCleanByteArray())
				.Concat(hashedSessionKey.ToCleanByteArray())
				.ToArray());
		}

		private byte[] GenerateHashFor(BigInteger bigInt)
		{
			return GenerateHashFor(bigInt.ToCleanByteArray());
		}

		private byte[] GenerateHashFor(byte[] bytes)
		{
			using (SHA1 shaProvider = SHA1.Create())
			{
				//Compute SHA1 hash of the provide bigInt
				return shaProvider.ComputeHash(bytes);
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~WoWSRP6PublicComponentHashServiceProvider() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion

	}
}
