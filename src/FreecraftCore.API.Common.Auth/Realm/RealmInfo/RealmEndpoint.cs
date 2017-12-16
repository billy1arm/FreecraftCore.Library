using FreecraftCore.Serializer;
using System;
using System.Linq;
using System.Net;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public class RealmEndpoint
	{
		//We use lazy because we may have like 100 realms and don't need to initialize ALL of their
		//IPAddress and ports. Just waste of CPU cycles.

		//They send IP as a string that contains both IP and the port
		[WireMember(1)]
		private string RealmEndpointInformation { get; }

		private Lazy<IPAddress> lazyIP { get; }

		/// <summary>
		/// <see cref="IPAddress"/> for the realm.
		/// </summary>
		public IPAddress RealmIP => lazyIP.Value;

		private Lazy<int> lazyPort { get; }

		/// <summary>
		/// Port for the realm.
		/// (Usually 8085)
		/// </summary>
		public int Port => lazyPort.Value;

		//TODO: If we make a server make a ctor for this

		public RealmEndpoint()
		{
			lazyIP = new Lazy<IPAddress>(BuildRealmIP, true);
			lazyPort = new Lazy<int>(BuildRealmPort, true);
		}

		//TODO: Cache split realm info
		private IPAddress BuildRealmIP()
		{
			IPAddress address = null;

			if (IPAddress.TryParse(RealmEndpointInformation.Split(':').First(), out address))
				return address;

			throw new InvalidOperationException($"Failed to generate IPAddress from {RealmEndpointInformation}.");
		}

		private int BuildRealmPort()
		{
			int port = 0;

			if (int.TryParse(RealmEndpointInformation.Split(':').Last(), out port))
				return port;

			throw new InvalidOperationException($"Failed to generate Port from {RealmEndpointInformation}.");
		}
	}
}
