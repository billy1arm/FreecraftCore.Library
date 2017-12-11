using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	public interface INetworkMessageWriterHandler<in TMessageType, TWriterType> : IPipelineListenerAsync<TWriterType, TMessageType, TWriterType>
	{

	}
}
