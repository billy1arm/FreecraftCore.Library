using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.API;
using FreecraftCore.API.Client;
using FreecraftCore.Crypto;
using FreecraftCore.Network;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Handler that handles the <see cref="AuthLogonChallengeResponse"/>.
	/// </summary>
	public class AuthenticationLogonChallengeResponseMessageHandler : AuthenticationMessageHandler<AuthLogonChallengeResponse>
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

		public AuthenticationLogonChallengeResponseMessageHandler([NotNull] IAuthenticationDetailsProvider detailsProvider, [NotNull] ISessionKeyStore sessionKeyStorage)
		{
			if (detailsProvider == null) throw new ArgumentNullException(nameof(detailsProvider));
			if (sessionKeyStorage == null) throw new ArgumentNullException(nameof(sessionKeyStorage));

			DetailsProvider = detailsProvider;
			SessionKeyStorage = sessionKeyStorage;
		}

		//The parameters are promised to never be null
		/// <inheritdoc />
		protected override NetworkMessageContextState RecieveMessage(AuthenticationNetworkMessageContext context, AuthLogonChallengeResponse stronglyTypedPayload)
		{
			//Check that we have a valid account and password
			if(DetailsProvider.Details == null)
				throw new InvalidOperationException($"The {nameof(DetailsProvider)} did not contain any details.");

			if(String.IsNullOrWhiteSpace(DetailsProvider.Details.AccountName))
				throw new InvalidOperationException($"There was no {nameof(DetailsProvider.Details.AccountName)} available.");

			if (String.IsNullOrWhiteSpace(DetailsProvider.Details.PlainTextPassword))
				throw new InvalidOperationException($"There was no {nameof(DetailsProvider.Details.PlainTextPassword)} available.");

			AuthLogonProofRequest proof = null;

			using (WoWSRP6CryptoServiceProvider srpProvider = new WoWSRP6CryptoServiceProvider(stronglyTypedPayload.Challenge.B.ToBigInteger(), stronglyTypedPayload.Challenge.N.ToBigInteger(), stronglyTypedPayload.Challenge.g.ToBigInteger()))
			{
				using (WoWSRP6PublicComponentHashServiceProvider hashingService = new WoWSRP6PublicComponentHashServiceProvider())
				{
					//Set the session key in the store for usage
					SessionKeyStorage.SessionKey = srpProvider.ComputeSessionKey(DetailsProvider.Details.AccountName.ToUpper(), DetailsProvider.Details.PlainTextPassword, stronglyTypedPayload.Challenge.salt);

					proof = new AuthLogonProofRequest(srpProvider.A.ToCleanByteArray(), hashingService.ComputeSRP6M1(srpProvider.g, srpProvider.N, DetailsProvider.Details.AccountName.ToUpper(), stronglyTypedPayload.Challenge.salt, srpProvider.A, srpProvider.B, SessionKeyStorage.SessionKey));
				}
			}

			//SendRequest.Create(AuthOperationCode.AUTH_LOGON_PROOF, proof, networkStream, serializer).Send();

			return NetworkMessageContextState.Handled;
		}
	}
}
