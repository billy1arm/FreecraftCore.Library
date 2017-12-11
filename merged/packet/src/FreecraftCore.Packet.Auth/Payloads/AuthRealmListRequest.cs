using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Payload for requesting the realm list.
	/// </summary>
	[WireDataContract]
	[AuthenticationPayload(Common.AuthOperationCode.REALM_LIST, AuthOperationDestinationCode.Server)] //TODO: Figure out how to support linking with the limited information.
	public class AuthRealmListRequest : AuthenticationPayload
	{
		//TODO: Implement
		public override bool isValid { get; } = true;

		//For some reason this is 5 bytes long (remove one for byte sized op code)

		//I can only guess what this is for.
		//This is what EmberEmu had to say: https://github.com/EmberEmu/Ember/blob/spark-new/src/login/grunt/client/RequestRealmList.h
		[WireMember(1)]
		private readonly int unknownValue = 0; // hardcoded to zero in public client
		
		public AuthRealmListRequest()
		{
			//Don't need to send anything.
		}
	}
}
