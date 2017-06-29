using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Metadata marker that fulfills the <see cref="WireDataContractBaseTypeRuntimeLinkAttribute"/> metadata role
	/// with a stronger typed ctor parameter for a <see cref="NetworkOperationCode"/>.
	/// </summary>
	public class GamePayloadOperationCodeAttribute : WireDataContractBaseTypeRuntimeLinkAttribute
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
			: base((int)operationCode)
		{
			if (!Enum.IsDefined(typeof(NetworkOperationCode), operationCode)) 
				throw new ArgumentOutOfRangeException(nameof(operationCode), "Value should be defined in the NetworkOperationCode enum.");
		}
	}
}
