using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace ARC4.ThirdParty.Tests
{
	public class Program
	{
		static void Main(string[] args)
		{
			byte[] key = ComputeTestKey();
			byte[] testData = NewTestData();
			byte[] jackpozData = testData.ToArray();
			byte[] outputBufferBouncy = new byte[50000];
			byte[] outputBufferJackpoz = new byte[50000];

			JackpozARC4 arc4 = new JackpozARC4(key);
			RC4Engine bouncyRC4 = new RC4Engine();

			bouncyRC4.Init(true, new KeyParameter(key));

			bouncyRC4.ProcessBytes(testData, 0, testData.Length, outputBufferBouncy, 0);
			arc4.Process(jackpozData, outputBufferJackpoz, 0, jackpozData.Length);

			for (int i = 0; i < jackpozData.Length; i++)
			{
				if (outputBufferJackpoz[i] != outputBufferBouncy[i])
				{
					Console.WriteLine($"Failed at index: {i} with Value Jackpoz: {outputBufferJackpoz[i]} vs Bouncy: {outputBufferBouncy[i]}");
					Console.ReadKey();
				}
			}

			Console.WriteLine("Finished parity test.");

			Stopwatch bouncyWatch = new Stopwatch();

			bouncyWatch.Start();
			for(int i = 0; i < 1000000; i++)
				bouncyRC4.ProcessBytes(testData, 0, testData.Length, outputBufferBouncy, 0);
			bouncyWatch.Stop();
			GC.Collect();

			Console.WriteLine($"Bouncy perf: {bouncyWatch.ElapsedMilliseconds}");

			Stopwatch jackpozWatch = new Stopwatch();

			jackpozWatch.Start();
			for (int i = 0; i < 1000000; i++)
				arc4.Process(jackpozData, outputBufferJackpoz, 0, jackpozData.Length);
			jackpozWatch.Stop();
			GC.Collect();

			Console.WriteLine($"Jackpoz perf: {jackpozWatch.ElapsedMilliseconds}");

			Console.ReadKey();
		}

		public static byte[] ComputeTestKey()
		{
			Random r = new Random(DateTime.Now.Millisecond);

			byte[] bytes = new byte[256];
			r.NextBytes(bytes);

			return bytes;
		}

		public static byte[] NewTestData()
		{
			Random r = new Random(DateTime.Now.Millisecond + 5);

			byte[] bytes = new byte[1024];
			r.NextBytes(bytes);

			return bytes;
		}
	}
}
