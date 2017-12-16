using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Crypto
{
	/// <summary>
	/// Provides a crypto service implementation of the
	/// wire writer strategy. Preforms crypto operations only once on the data.
	/// Must call <see cref="OneTimeCryptoStreamWriter"/>.GetBytes() to encrypt the byte array.
	/// </summary>
	public class OneTimeCryptoStreamWriter : CryptoStreamWriter
	{
		public enum State
		{
			/// <summary>
			/// Indicates that the writer is currently still writing.
			/// </summary>
			Default = 0,

			/// <summary>
			/// Indicates that the writer has crypted.
			/// </summary>
			Crypted = 1,
		}

		/// <summary>
		/// Indicates the state of the writer.
		/// If the writer is <see cref="State.Crypted"/> it can no longer write.
		/// Calls to GetBytes will force a crypted state.
		/// </summary>
		public State WriterState { get; private set; }

		private Lazy<byte[]> CryptoByteRepresentation { get; }

		public OneTimeCryptoStreamWriter([NotNull] IWireStreamWriterStrategy dest, [NotNull] ISessionPacketCryptoService sessionCrypto)
			: base(dest, sessionCrypto)
		{
			//The starting state is default
			//The writer can only write when it's in default state.
			WriterState = State.Default;

			//Lazily encrypt the stream's bytes.
			//This means it can ONLY be done once.
			CryptoByteRepresentation = new Lazy<byte[]>(() =>
			{
				WriterState = State.Crypted;
				return sessionCrypto.ProcessBytes(dest.GetBytes(), 0);
			}, true);
		}

		//See doc for IWireStreamReaderStrategy
		public override void Write(byte[] data)
		{
			ThrowIfEncrypted();
			Dest.Write(data);
		}

		public override void Write(byte[] data, int offset, int count)
		{
			ThrowIfEncrypted();
			Dest.Write(data, offset, count);
		}

		public override void Write(byte data)
		{
			ThrowIfEncrypted();
			Dest.Write(data);
		}

		public override byte[] GetBytes()
		{
			//Once this returns it's crypted and this writer can no longer write
			return CryptoByteRepresentation.Value;
		}

		public byte[] Crypt()
		{
			return GetBytes();
		}

		private void ThrowIfEncrypted()
		{
			if (WriterState == State.Crypted)
				throw new InvalidOperationException($"Cannot write bytes to {nameof(OneTimeCryptoStreamWriter)} after the stream has been encrypted.");
		}
	}
}
