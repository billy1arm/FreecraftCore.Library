using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using FreecraftCore.API.Common;
using FreecraftCore.Network;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	[AuthenticationMessageHandler(AuthOperationCode.REALM_LIST)]
	public class RealmListResponseMessageHandler : AuthenticationMessageHandler<AuthRealmListResponse>
	{
		/// <summary>
		/// Publishing service to push the realm information to.
		/// </summary>
		private IObserver<RealmInfo> OnRealmInformationAvailable { get; }

		public RealmListResponseMessageHandler([NotNull] IObserver<RealmInfo> onRealmInformationAvailable)
		{
			if (onRealmInformationAvailable == null) throw new ArgumentNullException(nameof(onRealmInformationAvailable));

			OnRealmInformationAvailable = onRealmInformationAvailable;
		}

		/// <inheritdoc />
		protected override NetworkMessageContextState RecieveMessage(AuthenticationNetworkMessageContext context, AuthRealmListResponse stronglyTypedPayload)
		{
			//TODO: We will write a rules and verification engine. No need to check fields manually.

			//Push all the realm info
			stronglyTypedPayload.Realms.ToObservable().Subscribe(OnRealmInformationAvailable).Dispose();

			//We should disconnect after we recieve the response
			context.ConnectionLink.Disconnect();

			return NetworkMessageContextState.Handled;
		}
	}
}
