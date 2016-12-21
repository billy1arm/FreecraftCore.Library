using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FreecraftCore.Packet.Auth
{
	//We cannot use basetype polymorphic serialization. The serializer supports only 1 to 1 for keys.
	//In authentication's case there is a response and a request associated with the command/opcode. Therefore it cannot be used.
	//[WireMessage]
	//[WireMessageBaseType(1, typeof(AuthLogonChallengeRequest))]
	public interface IAuthenticationPayload //this is sorta like a Java metadata interface marker. We use it for polymorphic serialization
	{

	}
}
