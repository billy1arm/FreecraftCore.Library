using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Org.BouncyCastle.Math;

namespace FreecraftCore.Crypto
{
	public static class BigIntegerExtensions
	{
		/// <summary>
		/// Header for Array types.
		/// </summary>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct ArrayHeader
		{
			public UIntPtr type;
			public UIntPtr length;
		}

		//From JackPoz's 3.3.5 bot: https://github.com/jackpoz/BotFarm
		/// <summary>
		/// Returns <see cref="BigInteger"/> in byteform but truncates
		/// the final field if it's 0.
		/// (The MSB)
		/// </summary>
		/// <param name="bigInt"></param>
		/// <returns></returns>
		public static unsafe byte[] ToCleanByteArray(this BigInteger bigInt)
		{
			return bigInt.ToByteArrayUnsigned();
		}
		
		//Bouncy has this implemented
		/*public static BigInteger ModPow(this BigInteger number, BigInteger exp, BigInteger modulus)
		{
			return BigInteger.ModPow(number, exp, modulus);
		}*/

		//From Jackpoz's 3.3.5 bot
		/// <summary>
		/// places a non-negative value (0) at the MSB, then converts to a BigInteger.
		/// This ensures a non-negative value without changing the binary representation.
		/// </summary>
		public static BigInteger ToBigInteger(this byte[] array, BigIntegerSign sign = BigIntegerSign.Positive) //WoW expects positive BigInteger
		{
			//Unlike with the System.Numberics or net35 BigInteger the sign byte is the first byte
			//It also trims all leading 0s. Because of this we do not need to manually remove
			//the sign byte.

			//Properly signs based on the provided optional
			//WoW expects mostly unsigned but Bouncy will default to including negatives
			return sign == BigIntegerSign.Default ? new BigInteger(array) : new BigInteger((int) sign, array);
		}
	}
}
