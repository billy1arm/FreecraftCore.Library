using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Represents an unknown game payload.
	/// </summary>
	[ProtocolGrouping(ProtocolCode.None)] //Unknown has no protocol
	public class UnknownGamePayload : GamePacketPayload
	{
		public override bool isValid => true;

		public UnknownGamePayload()
		{
			//Don't need to initialize anything
			//it's unknown so just check the OpCode to see what it is.
		}
	}
}
