using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FreecraftCore.API.Common;
using FreecraftCore.Network;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	public class AuthenticationLogonProofResponseMessageHandler : AuthenticationMessageHandler<AuthLogonProofResponse>
	{
		/// <summary>
		/// Publisher object that can publish the authentication result.
		/// </summary>
		private IObserver<AuthenticationResult> OnAuthenticationResultRecieved { get; }

		/// <summary>
		/// The service that is able to send messages back to the connection that sent this one.
		/// </summary>
		[NotNull]
		private INetworkMessageSendingService<AuthOperationCode> SendService { get; }

		public AuthenticationLogonProofResponseMessageHandler([NotNull] INetworkMessageSendingService<AuthOperationCode> sendService, [NotNull] IObserver<AuthenticationResult> onAuthenticationResultRecieved)
		{
			if (sendService == null) throw new ArgumentNullException(nameof(sendService));
			if (onAuthenticationResultRecieved == null) throw new ArgumentNullException(nameof(onAuthenticationResultRecieved));

			SendService = sendService;
			OnAuthenticationResultRecieved = onAuthenticationResultRecieved;
		}

		//The parameters are promised to never be null
		/// <inheritdoc />
		protected override NetworkMessageContextState RecieveMessage(AuthenticationNetworkMessageContext context, AuthLogonProofResponse stronglyTypedPayload)
		{
			OnAuthenticationResultRecieved.OnNext(stronglyTypedPayload.AuthResult);

			//TODO: How should the result be pushed out into UI or other space?
			if (stronglyTypedPayload.AuthResult == AuthenticationResult.Success)
				SendService.SendMessage(new AuthRealmListRequest()); //Send the realm list request too.

			return NetworkMessageContextState.Handled;
		}
	}
}
