using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace FreecraftCore.Crypto
{
	public static class BigIntegerExtensions
	{
		/// <summary>
		/// Returns <see cref="BigInteger"/> in byteform but truncates
		/// the final field if it's 0.
		/// (The MSB)
		/// </summary>
		/// <param name="bigInt"></param>
		/// <returns></returns>
		public static unsafe byte[] ToCleanByteArray(this BigInteger bigInt)
		{
			//We removed the array header memory hack
			//due to it likely causes the runtime crashes
			byte[] array = bigInt.ToByteArray();

			if (array.Length == 0 || array[array.Length - 1] != 0)
				return array;

			byte[] temp = new byte[array.Length - 1];
			Array.Copy(array, temp, temp.Length);

			return temp;
		}

		//Bouncy has this implemented
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
			//This can't be hacked like with ToCleanArray
			byte[] temp;
			if ((array[array.Length - 1] & 0x80) == 0x80)
			{
				temp = new byte[array.Length + 1];
				temp[array.Length] = 0;

				//Copies the contents of the array into temp
				//There is no way to memory hack this
				Array.Copy(array, temp, array.Length);
			}
			else
				temp = array;

			return new BigInteger(temp);
		}
	}
}
