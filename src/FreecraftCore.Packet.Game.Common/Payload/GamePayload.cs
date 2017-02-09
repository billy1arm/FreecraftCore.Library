using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	[DefaultChild(typeof(UnknownGamePayload))] //if we encounter something not handled we'll be able to produce an unknown payload
	[WireDataContract(WireDataContractAttribute.KeyType.UShort, InformationHandlingFlags.DontConsumeRead)]
	public abstract class GamePayload : IGamePacketPayload
	{
		/// <inheritdoc />
		[DontWrite] //don't write since this is just a leftover from type information
		[WireMember(1)]
		public NetworkOperationCode OperationCode { get; private set; }

		protected GamePayload()
		{
				
		}
	}
}
