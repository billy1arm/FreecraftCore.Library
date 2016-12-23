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
	[AuthenticationPayload(Common.AuthOperationCode.REALM_LIST)]
    public class AuthRealmListRequest : IAuthenticationPayload
    {
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
