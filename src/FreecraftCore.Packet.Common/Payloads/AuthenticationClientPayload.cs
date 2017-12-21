using FreecraftCore.Packet.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Authentication payload base type that is used to wire children for serialization purposes.
	/// This version of the Authentication Payload is for payloads sent by the server.
	/// </summary>
	[ProtocolGrouping(ProtocolCode.Authentication)] //we can put this on the base type because all auth packets have the same protocol.
	[WireDataContract(WireDataContractAttribute.KeyType.Byte, true)] //expect runtime linking
	public abstract class AuthenticationClientPayload : IAuthenticationPayload
	{
		/// <inheritdoc />
		public abstract bool isValid { get; }
	}
}