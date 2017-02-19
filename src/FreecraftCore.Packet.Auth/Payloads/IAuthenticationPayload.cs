using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore.Packet.Auth;


namespace FreecraftCore.Packet.Auth
{
	//There is a reason the keytype is UShort. It's complex to explain but the authserver sends a byte operation code. This is not enough information
	//to know what payload to deserialize so we extend the keytype by inserting data into the stream depending on if we're on the client or the server.
	/// <summary>
	/// Authentication payload base type that is used to wire children for serialization purposes.
	/// </summary>

	[WireDataContract(WireDataContractAttribute.KeyType.UShort, InformationHandlingFlags.DontWrite, true)] //expect runtime linking
	public abstract class AuthenticationPayload : IMessageVerifyable 
	{
		/// <inheritdoc />
		public abstract bool isValid { get; }
	}
}
