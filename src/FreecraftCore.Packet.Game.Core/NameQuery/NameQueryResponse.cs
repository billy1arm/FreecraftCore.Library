using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.API.Common;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.SMSG_NAME_QUERY_RESPONSE)]
	[ProtocolGrouping(ProtocolCode.Game)] //TODO: Change this protocol to something more specific
	public class NameQueryResponse : GamePacketPayload
	{
		/// <inheritdoc />
		public override bool isValid { get; } = true;

		/// <summary>
		/// The GUID requested in the name query request.
		/// </summary>
		[WireMember(1)]
		public PackedGuid RequestedGuid { get; private set; }

		/// <summary>
		/// The name query result.
		/// </summary>
		[WireMember(2)]
		public NameQueryResult Result { get; private set; }

		protected NameQueryResponse()
		{
			//serializer ctor
		}
	}
}
