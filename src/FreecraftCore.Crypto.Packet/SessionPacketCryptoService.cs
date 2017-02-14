using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using FreecraftCore.Crypto.RC4;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Handles session-based crypto services for packets.
	/// </summary>
	public class SessionPacketCryptoService : ISessionPacketCryptoService
	{
		//TODO: Maybe not hard code this for different server or client versions
		/// <summary>
		/// Reverse engineered from the client.
		/// (Found in Jackpoz's bot)
		/// </summary>
		private static readonly byte[] encryptionKey = new byte[]
		{
			0xC2, 0xB3, 0x72, 0x3C, 0xC6, 0xAE, 0xD9, 0xB5,
			0x34, 0x3C, 0x53, 0xEE, 0x2F, 0x43, 0x67, 0xCE
		};

		/// <summary>
		/// Reverse engineered from the client.
		/// (Found in Jackpoz's bot)
		/// </summary>
		private static readonly byte[] decryptionKey = new byte[]
		{
			0xCC, 0x98, 0xAE, 0x04, 0xE8, 0x97, 0xEA, 0xCA,
			0x12, 0xDD, 0xC0, 0x93, 0x42, 0x91, 0x53, 0x57
		};

		/// <summary>
		/// Managed RC4 cipher service.
		/// </summary>
		[NotNull]
		protected IRC4CryptoServiceProvider rc4CryptoService { get; }

		/// <summary>
		/// Creates a new session packet crypto service.
		/// </summary>
		/// <param name="sessionKey">The key to be used in the cipher.</param>
		/// <param name="isForEncryption">If true the the service encrypts. Otherwise it decrypts.</param>
		public SessionPacketCryptoService(BigInteger sessionKey, bool isForEncryption)
			: this(sessionKey.ToCleanByteArray(), isForEncryption)
		{

		}

		/// <summary>
		/// Creates a new session packet crypto service.
		/// </summary>
		/// <param name="sessionKey">The key to be used in the cipher.</param>
		/// <param name="isForEncryption">If true the the service encrypts. Otherwise it decrypts.</param>
		public SessionPacketCryptoService([NotNull] byte[] sessionKey, bool isForEncryption)
			: this(sessionKey, isForEncryption, isForEncryption ? encryptionKey : decryptionKey)
		{

		}

		/// <summary>
		/// Creates a new session paket crypto service.
		/// </summary>
		/// <param name="sessionKey"></param>
		/// <param name="isForEncryption"></param>
		/// <param name="hmacKey"></param>
		public SessionPacketCryptoService([NotNull] byte[] sessionKey, bool isForEncryption, [NotNull] byte[] hmacKey)
		{
			if (sessionKey == null) throw new ArgumentNullException(nameof(sessionKey));
			if (hmacKey == null) throw new ArgumentNullException(nameof(hmacKey));

			//RC4-Drop[n] for security
			//Build an RC4 cipher service for the provided session key.
			using (HMACSHA1 hmacsha = new HMACSHA1(hmacKey))
				rc4CryptoService = new RC4DropNCryptoServiceProvider(hmacsha.ComputeHash(sessionKey), 1024, isForEncryption);
		}

		/// <inheritdoc />
		public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			//defer parameter validation to the decorated crypto service
			rc4CryptoService.ProcessBytes(input, inOff, length, output, outOff);
		}

		/// <inheritdoc />
		public byte ReturnByte(byte input)
		{
			//defer parameter validation to the decorated crypto service
			return rc4CryptoService.ReturnByte(input);
		}
	}
}
