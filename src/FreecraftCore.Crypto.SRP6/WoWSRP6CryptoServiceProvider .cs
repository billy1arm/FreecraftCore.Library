using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Secure Remote Password 6 service provider.
	/// Based on the work https://github.com/vermie/MangosClient/blob/master/Client/Authentication/Network/AuthSocket.cs
	/// and the specification: http://srp.stanford.edu/design.html
	/// </summary>
	public class WoWSRP6CryptoServiceProvider : IDisposable
	{
		//TODO: Implement >net4 Lazy loaded version.
		private RNGCryptoServiceProvider randomProvider { get; } = new RNGCryptoServiceProvider();

		/// <summary>
		/// First public key component for SRP.
		/// </summary>
		public BigInteger A { get; private set; }

		/// <summary>
		/// Second public key component for SRP.
		/// </summary>
		public BigInteger B { get; private set; }

		/// <summary>
		/// SRP N. A large safe prime (N = 2q+1, where q is prime)
		/// 2q + 1 primes are Sophie Germain primes https://en.wikipedia.org/wiki/Sophie_Germain_prime
		/// </summary>
		public BigInteger N { get; private set; }

		/// <summary>
		/// SRP g. A generator modulo N (might be B generator modulo N in this setup?)
		/// </summary>
		public BigInteger g { get; private set; }

		/// <summary>
		/// SRP a. A secrete ephemeral value.
		/// </summary>
		private BigInteger privateKeyComponent_a { get; }

		public WoWSRP6CryptoServiceProvider(BigInteger providedB, BigInteger providedN, BigInteger providedg)
		{
			B = providedB;
			N = providedN;
			g = providedg;

			byte[] randBytes = new byte[19];

			//Initialize A

			//The host will abort if it detects that A == 0(mod N)
			//We try again. Not abort. This shouldn't happen though?
			do
			{
				//Fill array with crypto secure bytes
				randomProvider.GetNonZeroBytes(randBytes);

				//Build a private component
				privateKeyComponent_a = randBytes.ToBigInteger(); //should be secure and valid

				//Based on SRP6 spec: https://www.codeproject.com/articles/1082676/communication-using-secure-remote-password-protoco
				A = g.ModPow(privateKeyComponent_a, N);

			} while (A.ModPow(1, N) == 0);
		}

		public BigInteger ComputeSessionKey(string userName, string password, byte[] challengeSalt)
		{

			//Attribution to both Jackpoz's 3.3.5 bot and Mangons Client
			//https://github.com/jackpoz/BotFarm
			//https://github.com/vermie/MangosClient/blob/master/Client/Authentication/Network/AuthSocket.cs

			//TODO: Is this the correct k value? SRP6 spec shows k = hash(N, g)
			//hashedTokenWithUser = SRP6 u
			//hashedSaltPassword = SRP6 x
			//compute session key
			using (WoWSRP6PublicComponentHashServiceProvider hashProvider = new WoWSRP6PublicComponentHashServiceProvider())
			{
				//Compute password hash salted with provided salt
				BigInteger x = hashProvider.Hash(challengeSalt, hashProvider.Hash(Encoding.ASCII.GetBytes(password.ToUpper()))).ToBigInteger();

				return ((B + new BigInteger(3) * (N - g.ModPow(x, N))) % N).ModPow(privateKeyComponent_a + (hashProvider.Hash(A.ToCleanByteArray(), B.ToCleanByteArray()).ToBigInteger() * x), N);
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
		// ~SRP6CryptoServiceProvider() {
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
