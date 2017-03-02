using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.API;
using FreecraftCore.API.Client;
using FreecraftCore.API.Common;
using FreecraftCore.Crypto;
using FreecraftCore.Handlers;
using FreecraftCore.Network;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;
using Org.BouncyCastle.Asn1;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Handler that handles the <see cref="AuthLogonChallengeResponse"/>.
	/// </summary>
	[AuthenticationMessageHandler(AuthOperationCode.AUTH_LOGON_CHALLENGE)]
	public class AuthenticationLogonChallengeResponseMessageHandler : AuthenticationMessageHandlerAsync<AuthLogonChallengeResponse>
	{
		/// <summary>
		/// Injected details provider dependency.
		/// </summary>
		[NotNull]
		private IAuthenticationDetailsProvider DetailsProvider { get; }

		/// <summary>
		/// Mutable settable session key container.
		/// </summary>
		[NotNull]
		private ISessionKeyStore SessionKeyStorage { get; }

		/// <summary>
		/// The service that is able to send messages back to the connection that sent this one.
		/// </summary>
		[NotNull]
		private INetworkMessageSendingService<AuthOperationCode> SendService { get; }

		public AuthenticationLogonChallengeResponseMessageHandler([NotNull] IAuthenticationDetailsProvider detailsProvider, [NotNull] ISessionKeyStore sessionKeyStorage, [NotNull] INetworkMessageSendingService<AuthOperationCode> sendService)
		{
			if (detailsProvider == null) throw new ArgumentNullException(nameof(detailsProvider));
			if (sessionKeyStorage == null) throw new ArgumentNullException(nameof(sessionKeyStorage));
			if (sendService == null) throw new ArgumentNullException(nameof(sendService));

			DetailsProvider = detailsProvider;
			SessionKeyStorage = sessionKeyStorage;
			SendService = sendService;
		}

		//The parameters are promised to never be null
		/// <inheritdoc />
		protected override async Task<NetworkMessageContextState> RecieveMessage(AuthenticationNetworkMessageContext context, AuthLogonChallengeResponse stronglyTypedPayload)
		{
			//Check that we have a valid account and password
			if(DetailsProvider.Details == null)
				throw new InvalidOperationException($"The {nameof(DetailsProvider)} did not contain any details.");

			if(String.IsNullOrWhiteSpace(DetailsProvider.Details.AccountName))
				throw new InvalidOperationException($"There was no {nameof(DetailsProvider.Details.AccountName)} available.");

			if (String.IsNullOrWhiteSpace(DetailsProvider.Details.PlainTextPassword))
				throw new InvalidOperationException($"There was no {nameof(DetailsProvider.Details.PlainTextPassword)} available.");

			if(stronglyTypedPayload.Result != AuthenticationResult.Success)
				throw new InvalidOperationException($"The auth challenge failed. Returned: {stronglyTypedPayload.Result}.");

			AuthLogonProofRequest proof = null;

			using (WoWSRP6CryptoServiceProvider srpProvider = new WoWSRP6CryptoServiceProvider(stronglyTypedPayload.Challenge.B.ToBigInteger(), stronglyTypedPayload.Challenge.N.ToBigInteger(), stronglyTypedPayload.Challenge.g.ToBigInteger()))
			{
				using (WoWSRP6PublicComponentHashServiceProvider hashingService = new WoWSRP6PublicComponentHashServiceProvider())
				{
					//Set the session key in the store for usage
					BigInteger unhashedKey = srpProvider.ComputeSessionKey(DetailsProvider.Details.AccountName.ToUpper(), DetailsProvider.Details.PlainTextPassword, stronglyTypedPayload.Challenge.salt);

					proof = new AuthLogonProofRequest(srpProvider.A.ToCleanByteArray(), hashingService.ComputeSRP6M1(srpProvider.g, srpProvider.N, DetailsProvider.Details.AccountName.ToUpper(), stronglyTypedPayload.Challenge.salt, srpProvider.A, srpProvider.B, unhashedKey));

					//Set the session key as a hashed session key
					SessionKeyStorage.SessionKey = hashingService.HashSessionKey(unhashedKey);
				}
			}

			//Send the proof to the authserver
			await SendService.SendMessage(proof);

			return NetworkMessageContextState.Handled;
		}
	}
}
