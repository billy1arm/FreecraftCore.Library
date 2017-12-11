using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Network;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Base contract for all authentication message handlers that need async handling.
	/// </summary>
	/// <typeparam name="TAuthenticationPayloadType">The payload this handler is responsible for.</typeparam>
	public abstract class AuthenticationMessageHandlerAsync<TAuthenticationPayloadType> : INetworkMessagePipelineListenerAsync<AuthenticationNetworkMessageContext, AuthOperationCode, IAuthenticationPacketHeader, AuthenticationPayload>
		where TAuthenticationPayloadType : AuthenticationPayload
	{
		public async Task<NetworkMessageContextState> RecievePipelineMessage([NotNull] AuthenticationNetworkMessageContext input, NetworkMessageContextState currentState)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));

			TAuthenticationPayloadType stronglyTypedPayload = input.NetworkMessage.Payload as TAuthenticationPayloadType;
			
			//If it didn't cast or was already null then we should yield an invalid result
			if(stronglyTypedPayload == null)
				return NetworkMessageContextState.Invalid | currentState;

			//Pass to message handling method implemented in the child class
			return (await RecieveMessage(input, stronglyTypedPayload)) | currentState;
		}

		/// <summary>
		/// Message handler method. Provided context and <see cref="stronglyTypedPayload"/> and promised to never be null.
		/// The <see cref="stronglyTypedPayload"/> is merely a strongly typed version of the payload contained in the <see cref="AuthenticationNetworkMessageContext"/> for
		/// ease of consumption.
		/// </summary>
		/// <param name="context">The authentication message context.</param>
		/// <param name="stronglyTypedPayload">The strongly typed casted payload.</param>
		/// <returns></returns>
		protected abstract Task<NetworkMessageContextState> RecieveMessage([NotNull] AuthenticationNetworkMessageContext context, [NotNull] TAuthenticationPayloadType stronglyTypedPayload);
	}
}
