using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Numerics
{
	public static class BigIntegerExtensions
	{
		//From JackPoz's 3.3.5 bot: https://github.com/jackpoz/BotFarm
		/// <summary>
		/// Returns <see cref="BigInteger"/> in byteform but truncates
		/// the final field if it's 0.
		/// (The MSB)
		/// </summary>
		/// <param name="bigInt"></param>
		/// <returns></returns>
		public static byte[] ToCleanByteArray(this BigInteger bigInt)
		{
			byte[] array = bigInt.ToByteArray();
			if (array[array.Length - 1] != 0)
				return array;

			byte[] temp = new byte[array.Length - 1];
			Array.Copy(array, temp, temp.Length);
			return temp;
		}

		public static BigInteger ModPow(this BigInteger number, BigInteger exp, BigInteger modulus)
		{
			return BigInteger.ModPow(number, exp, modulus);
		}

		//From Jackpoz's 3.3.5 bot
		/// <summary>
		/// places a non-negative value (0) at the MSB, then converts to a BigInteger.
		/// This ensures a non-negative value without changing the binary representation.
		/// </summary>
		public static BigInteger ToBigInteger(this byte[] array)
		{
			byte[] temp;
			if ((array[array.Length - 1] & 0x80) == 0x80)
			{
				temp = new byte[array.Length + 1];
				temp[array.Length] = 0;
			}
			else
				temp = new byte[array.Length];

			Array.Copy(array, temp, array.Length);
			return new BigInteger(temp);
		}
	}
}
