using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Simple;
using FreecraftCore;
using FreecraftCore.API.Common;
using FreecraftCore.Crypto;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Serializer;
using GladNet;

namespace Authentication.TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ReadKey();

			Task.Factory.StartNew(AsyncMain).Wait();
			Console.ReadKey();
		}

		private static async Task AsyncMain()
		{
			SerializerService serializer = new SerializerService();

			typeof(AuthLogonChallengeRequest).Assembly
				.GetTypes()
				.Where(t => typeof(AuthenticationServerPayload).IsAssignableFrom(t) || typeof(AuthenticationClientPayload).IsAssignableFrom(t))
				.ToList()
				.ForEach(t =>
				{
					serializer.RegisterType(t);
				});

			serializer.Compile();

			if(!serializer.isTypeRegistered<AuthLogonChallengeResponse>() || !serializer.isTypeRegistered<AuthLogonChallengeRequest>())
				Console.WriteLine("Failed to register key types.");

			AuthLogonChallengeResponse testChallengeResponse = new AuthLogonChallengeResponse();

			try
			{
				//The auth server is encryptionless and headerless
				/*IManagedNetworkClient<AuthenticationClientPayload, AuthenticationServerPayload> client = new DotNetTcpClientNetworkClient()
					.AddHeaderlessNetworkMessageReading(new FreecraftCoreGladNetSerializerAdapter(serializer))
					.For<AuthenticationServerPayload, AuthenticationClientPayload, IAuthenticationPayload>()
					.Build()
					.AsManaged(new ConsoleOutLogger("ConsoleLogger", LogLevel.All, true, false, false, null));*/

				NetworkClientHeaderlessPacketPayloadReaderWriterDecorator<DotNetTcpClientNetworkClient, AuthenticationServerPayload, AuthenticationClientPayload, IAuthenticationPayload> client = new DotNetTcpClientNetworkClient()
					.AddHeaderlessNetworkMessageReading(new FreecraftCoreGladNetSerializerAdapter(serializer))
					.For<AuthenticationServerPayload, AuthenticationClientPayload, IAuthenticationPayload>()
					.Build();

				//PayloadInterceptMessageSendService<AuthenticationClientPayload> interceptService = new PayloadInterceptMessageSendService<AuthenticationClientPayload>(client, client);

				if(!await client.ConnectAsync("127.0.0.1", 3724))
					Console.WriteLine("Failed to connect");

				await client.WriteAsync(serializer.Serialize(new AuthLogonChallengeRequest(ProtocolVersion.ProtocolVersionTwo, GameType.WoW, ExpansionType.WrathOfTheLichKing, 3, 5,
					ClientBuild.Wotlk_3_3_5a, PlatformType.x86, OperatingSystemType.Win, LocaleType.enUS,
					IPAddress.Parse("127.0.0.1"), "Glader")));

				/*SendResult sendResult = await client.SendMessage(new AuthLogonChallengeRequest(ProtocolVersion.ProtocolVersionTwo, GameType.WoW, ExpansionType.WrathOfTheLichKing, 3, 5,
					ClientBuild.Wotlk_3_3_5a, PlatformType.x86, OperatingSystemType.Win, LocaleType.enUS,
					IPAddress.Parse("127.0.0.1"), "Glader"));*/

				while(true)
				{
					//TODO: The non-async version works. Check into why the async doesn't
					var response = await serializer.DeserializeAsync<AuthenticationServerPayload>(new AsyncWireReaderBytesReadableAdapter(client));

					Console.WriteLine("Recieved payload");

					if(response is AuthLogonChallengeResponse challengeResponse)
					{
						if(challengeResponse?.Challenge.B.Length != 32)
							throw new InvalidOperationException($"B wasn't 32 bytes. Was: {challengeResponse?.Challenge.B.Length}");

						if(challengeResponse?.Challenge.g.Length != 1)
							throw new InvalidOperationException($"G wasn't 1 bytes. Was: {challengeResponse.Challenge.g.Length}");

						Console.WriteLine($"Response: Valid: {challengeResponse.isValid} Result: {challengeResponse.Result} SRP: {challengeResponse.Challenge}");
					}
					else
						Console.WriteLine($"Recieved Payload of Type: {response.GetType().Name}");

					/*var response = await client.ReadMessageAsync()
						.ConfigureAwait(false);

					Console.WriteLine("Recieved payload");

					if(response.Payload is AuthLogonChallengeResponse challengeResponse)
						Console.WriteLine($"Response: Valid: {challengeResponse.isValid} Result: {challengeResponse.Result} SRP: {challengeResponse.Challenge}");
					else
						Console.WriteLine($"Recieved Payload of Type: {response.Payload.GetType().Name}");*/
				}
			}
			catch(Exception e)
			{
				Console.WriteLine($"Exception: {e}");

				if(e is AggregateException ea)
					foreach(var ex in ea.InnerExceptions)
						Console.WriteLine($"InnerException: {ex.Message}");

				throw;
			}
		}
	}
}
