using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Packet.Auth;
using NUnit.Framework;

namespace FreecraftCore.Tests
{
	[TestFixture]
	public class AuthenticationServerPayloadTests : AutomatedReflectionTests<AuthenticationServerPayload, AuthLogonChallengeResponse>
	{

	}

	[TestFixture]
	public class AuthenticationClientPayloadTests : AutomatedReflectionTests<AuthenticationClientPayload, AuthLogonChallengeRequest>
	{

	}
}
