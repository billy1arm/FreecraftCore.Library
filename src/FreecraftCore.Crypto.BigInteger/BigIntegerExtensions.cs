#if NET35
using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Numerics
{
	public static class BigIntegerExtensions
	{
		public static BigInteger ModPow(this BigInteger number, BigInteger exp, BigInteger n)
		{
			return BigInteger.ModPow(number, exp, n);
		}
	}
}
#endif
