using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;

namespace FreecraftCore
{
	public interface IAuthenticationPayload : IMessageVerifyable, IProtocolGroupable, IOperationCodeProvidable<AuthOperationCode>
	{
		
	}
}