using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Metadata marker that fulfills the <see cref="WireDataContractBaseLinkAttribute"/> metadata role
	/// with a stronger typed ctor parameter for a <see cref="NetworkOperationCode"/>.
	/// </summary>
	public class GamePayloadOperationCodeAttribute : WireDataContractBaseLinkAttribute
	{
		/// <summary>
		/// The <see cref="NetworkOperationCode"/> of the packet.
		/// </summary>
		public NetworkOperationCode OperationCode => (NetworkOperationCode) Index;

		/// <summary>
		/// Creates a metadata marker that defines the <see cref="NetworkOperationCode"/>
		/// for this packet.
		/// </summary>
		/// <param name="operationCode">The operation code.</param>
		public GamePayloadOperationCodeAttribute(NetworkOperationCode operationCode) 
			: base((int)operationCode, typeof(GamePacketPayload))
		{
			if (!Enum.IsDefined(typeof(NetworkOperationCode), operationCode)) 
				throw new ArgumentOutOfRangeException(nameof(operationCode), "Value should be defined in the NetworkOperationCode enum.");
		}

		/// <summary>
		/// Reserved only for testing.
		/// DO NOT CALL.
		/// </summary>
		/// <param name="i"></param>
		internal GamePayloadOperationCodeAttribute(int i)
			: base(i, typeof(GamePacketPayload))
		{
			
		}
	}
}
