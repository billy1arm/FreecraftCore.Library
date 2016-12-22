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
	}
}
