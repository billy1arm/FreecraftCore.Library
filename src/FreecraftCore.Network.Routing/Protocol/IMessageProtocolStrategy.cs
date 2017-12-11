using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Contract for a strongly typed strategy that provides a <see cref="ProtocolCode"/>.
	/// </summary>
	public interface IMessageProtocolStrategy
	{
		ProtocolCode MessageProtocol { get; }
	}
}
