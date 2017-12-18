using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Auth;
using NUnit.Framework;

namespace FreecraftCore.Tests
{
	[TestFixture]
	public class CharacterScreenGamePayloadTests : AutomatedReflectionTests<GamePacketPayload, CharacterListRequest>
	{

	}

	[TestFixture]
	public class ChatGamePayloadTests : AutomatedReflectionTests<GamePacketPayload, ChatMessageEvent>
	{

	}

	[TestFixture]
	public class CoreGamePayloadTests : AutomatedReflectionTests<GamePacketPayload, NameQueryRequest>
	{

	}

	[TestFixture]
	public class WardenPayloadTests : AutomatedReflectionTests<GamePacketPayload, WardenServerPayload>
	{

	}
}
