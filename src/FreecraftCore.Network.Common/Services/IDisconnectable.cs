using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Contract for types that are disconnectable.
	/// </summary>
	public interface IDisconnectable
	{
		//TODO: Implement a reason code system.
		void Disconnect();
	}
}
