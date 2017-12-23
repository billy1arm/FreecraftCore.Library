using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using FreecraftCore.API;
using FreecraftCore.API.Common;
using FreecraftCore.Crypto;
using FreecraftCore.Handlers;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using GladNet;
using JetBrains.Annotations;
using BigInteger = FreecraftCore.Crypto.BigInteger;

namespace FreecraftCore.Handlers
{
	//TODO: Do we even want a handler? We might just want to intercept the response.
	//TODO: Add dependencies for auth details and such.
	/// <summary>
	/// Handler that handles the <see cref="AuthLogonChallengeResponse"/>.
	/// </summary>
	public class AuthenticationLogonChallengeResponseMessageHandler : IPeerPayloadSpecificMessageHandler<AuthLogonChallengeResponse, AuthenticationClientPayload>
	{
		public AuthenticationLogonChallengeResponseMessageHandler()
		{

		}

		/// <inheritdoc />
		public async Task HandleMessage(IPeerMessageContext<AuthenticationClientPayload> context, AuthLogonChallengeResponse payload)
		{
			AuthLogonProofRequest proof = null;

			if(payload.Result != AuthenticationResult.Success)
				throw new InvalidOperationException($"The auth challenge failed. Returned: {payload.Result}.");

			using(WoWSRP6CryptoServiceProvider srpProvider = new WoWSRP6CryptoServiceProvider(payload.Challenge.B.ToBigInteger(), payload.Challenge.N.ToBigInteger(), payload.Challenge.g.ToBigInteger()))
			{
				using(WoWSRP6PublicComponentHashServiceProvider hashingService = new WoWSRP6PublicComponentHashServiceProvider())
				{
					//TODO: Remove hardcoded name/pass
					//Set the session key in the store for usage
					BigInteger unhashedKey = srpProvider.ComputeSessionKey("Glader".ToUpper(), "test", payload.Challenge.salt);

					proof = new AuthLogonProofRequest(srpProvider.A.ToCleanByteArray(), hashingService.ComputeSRP6M1(srpProvider.g, srpProvider.N, "Glader".ToUpper(), payload.Challenge.salt, srpProvider.A, srpProvider.B, unhashedKey));

					//Set the session key as a hashed session key
					//SessionKeyStorage.SessionKey = hashingService.HashSessionKey(unhashedKey);
				}
			}

			await context.PayloadSendService.SendMessage(proof);
		}
	}
}
