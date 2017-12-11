using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto.RC4
{
	/// <summary>
	/// RC4 encryption/decryption service.
	/// </summary>
	public interface IRC4CryptoServiceProvider : ICryptoServiceProvider, IDisposable
	{
		//Temporarily empty
		//Everything was moved to our version of BouncyCastle's ICipherStream.
	}
}
