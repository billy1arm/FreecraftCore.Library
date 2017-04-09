using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public class RealmInfo
	{
		[WireMember(1)]
		public RealmType RealmType { get; }

		// <summary>
		/// Indidicates if the realm is open.
		/// (Only in 2.x and 3.x according to Trinitycore)
		/// </summary>
		[WireMember(2)]
		public bool isLocked { get; }

		[WireMember(3)]
		public IRealmInformation Information { get; }

		public RealmInfo()
		{

		}
	}
}
