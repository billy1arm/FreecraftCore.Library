using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Enumeration of application types in the network.
	/// </summary>
	public enum NetworkApplicationType : int
	{
		/// <summary>
		/// Indicates an unknown application type.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Indicates that it represents a connection to a client.
		/// </summary>
		Client = 1,

		/// <summary>
		/// Generally indicates a "server" from the perspective of a client.
		/// Actual serverside or services have a more specific denotation.
		/// </summary>
		Server = 2

		//TODO: List all the other services. WoW says they have like 40.
	}
}
