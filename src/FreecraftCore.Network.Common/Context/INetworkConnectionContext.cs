using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Based on Blizzard's JAMLink concept where
	/// an object provides access to abstraction over a connected
	/// service or client.
	/// </summary>
	public interface INetworkConnectionContext : IDisconnectable
	{
		//JAMLink is suppose to contain information about the type of program
		/// <summary>
		/// Indicates the connection context's applicaiton type.
		/// </summary>
		NetworkApplicationType ApplicationType { get; }

		//We don't need to list accepted protocols for now. We use JAM (FreecraftCore.Serializer) and probably HTTP

		//TODO: Implement disconnection possibility like talked about in: https://www.youtube.com/watch?v=hCsEHYwjqVE&t=1830s
	}
}
