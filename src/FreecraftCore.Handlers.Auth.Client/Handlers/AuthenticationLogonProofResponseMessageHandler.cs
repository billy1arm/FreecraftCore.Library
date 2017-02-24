using System;
using System.Collections.Generic;
using System.Linq;
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
		[NotNull]
		private IObserver<AuthenticationResult> OnResultRecieved { get; }

		/// <summary>
		/// The service that is able to send messages back to the connection that sent this one.
		/// </summary>
		[NotNull]
		private INetworkMessageSendingService<AuthOperationCode> SendService { get; }

		public AuthenticationLogonProofResponseMessageHandler([NotNull] IObserver<AuthenticationResult> onResultRecieved, [NotNull] INetworkMessageSendingService<AuthOperationCode> sendService)
		{
			if (onResultRecieved == null) throw new ArgumentNullException(nameof(onResultRecieved));
			if (sendService == null) throw new ArgumentNullException(nameof(sendService));

			OnResultRecieved = onResultRecieved;
			SendService = sendService;
		}

		//The parameters are promised to never be null
		/// <inheritdoc />
		protected override NetworkMessageContextState RecieveMessage(AuthenticationNetworkMessageContext context, AuthLogonProofResponse stronglyTypedPayload)
		{
			//TODO: How should the result be pushed out into UI or other space?
			if (stronglyTypedPayload.AuthResult != AuthenticationResult.Success)
			{
				//TODO: What do we do if we fail?
				OnResultRecieved.OnNext(stronglyTypedPayload.AuthResult);
			}
			else
			{
				OnResultRecieved.OnNext(stronglyTypedPayload.AuthResult);

				//Send the realm list request too.
				SendService.SendMessage(new AuthRealmListRequest());
			}

			return NetworkMessageContextState.Handled;
		}
	}
}
