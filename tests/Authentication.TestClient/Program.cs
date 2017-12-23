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
using FreecraftCore.Handlers;
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

			//The auth server is encryptionless and headerless
			IManagedNetworkClient<AuthenticationClientPayload, AuthenticationServerPayload> client = new DotNetTcpClientNetworkClient()
				.AddHeaderlessNetworkMessageReading(new FreecraftCoreGladNetSerializerAdapter(serializer))
				.For<AuthenticationServerPayload, AuthenticationClientPayload, IAuthenticationPayload>()
				.Build()
				.AsManaged(new ConsoleOutLogger("ConsoleLogger", LogLevel.All, true, false, false, null));

			if(!await client.ConnectAsync("127.0.0.1", 3724))
				Console.WriteLine("Failed to connect");

			await client.SendMessage(new AuthLogonChallengeRequest(ProtocolVersion.ProtocolVersionTwo, GameType.WoW, ExpansionType.WrathOfTheLichKing, 3, 5,
				ClientBuild.Wotlk_3_3_5a, PlatformType.x86, OperatingSystemType.Win, LocaleType.enUS,
				IPAddress.Parse("127.0.0.1"), "Glader"));

			while(true)
			{
				var response = (await client.ReadMessageAsync()).Payload;

				Console.WriteLine("Recieved payload");

				AuthenticationLogonChallengeResponseMessageHandler handler = new AuthenticationLogonChallengeResponseMessageHandler();

				if(response is AuthLogonChallengeResponse challengeResponse)
				{
					Console.WriteLine($"Response: Valid: {challengeResponse.isValid} Result: {challengeResponse.Result} SRP: {challengeResponse.Challenge}");

					await handler.HandleMessage(new DefaultPeerMessageContext<AuthenticationClientPayload>(client, client, new PayloadInterceptMessageSendService<AuthenticationClientPayload>(client, client)), challengeResponse);
				}
				else
					Console.WriteLine($"Recieved Payload of Type: {response.GetType().Name}");
			}
		}
	}
}
