using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore.Packet.Auth;


namespace FreecraftCore.Packet.Auth
{
	//We cannot use basetype polymorphic serialization. The serializer supports only 1 to 1 for keys.
	//In authentication's case there is a response and a request associated with the command/opcode. Therefore it cannot be used.
	//[WireDataContract]
	//[WireDataContractBaseType(1, typeof(AuthLogonChallengeRequest))]
	public interface IAuthenticationPayload : IMessageVerifyable //this is sorta like a Java metadata interface marker. We use it for polymorphic serialization
	{
		//Can't do OpCode prop because concrete classes may be involved in multiple op codes

		//TODO: Add a verify
	}
}
