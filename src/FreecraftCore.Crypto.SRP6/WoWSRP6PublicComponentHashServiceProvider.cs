using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace FreecraftCore.Crypto.SRP6
{
	/// <summary>
	/// Provides SRP6 public component hashing service.
	/// Review: u on http://srp.stanford.edu/design.html
	/// Or this diagram: https://www.codeproject.com/articles/1082676/communication-using-secure-remote-password-protoco
	/// </summary>
	public class WoWSRP6PublicComponentHashServiceProvider : IDisposable
	{
		/// <summary>
		/// SRP6 u. Random scrambling parameter.
		/// See: http://srp.stanford.edu/design.html
		/// </summary>
		public BigInteger u { get; private set; }

		public WoWSRP6PublicComponentHashServiceProvider(BigInteger publicComponentA, BigInteger PublicComponentB)
		{
			if (publicComponentA == null)
				throw new ArgumentNullException(nameof(publicComponentA), $"Must provide a public key component. Cannot preform hash without two non-null public components.");

			if (PublicComponentB == null)
				throw new ArgumentNullException(nameof(PublicComponentB), $"Must provide a public key component. Cannot preform hash without two non-null public components.");

			//WoW expects non-secure SHA1 hashing. SRP6 is deprecated too. We need to do it anyway
			using (SHA1 shaProvider = SHA1.Create())
			{
				//See Jackpoz's Combine function
				u = new BigInteger(shaProvider.ComputeHash(Enumerable.Concat(publicComponentA.ToCleanByteArray(), PublicComponentB.ToCleanByteArray()).ToArray()));
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
