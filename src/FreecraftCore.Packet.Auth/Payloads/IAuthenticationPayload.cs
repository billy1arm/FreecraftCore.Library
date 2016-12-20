using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreecraftCore.Packet.Auth
{
	[WireMessage]
	[WireMessageBaseType(1, typeof(AuthLogonChallengeRequest))]
	public interface IAuthenticationPayload //this is sorta like a Java metadata interface marker. We use it for polymorphic serialization
	{

	}
}
