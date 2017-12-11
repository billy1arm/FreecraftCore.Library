using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public class RealmCompleteInformation : IRealmInformation
	{
		//So, the idea here is to avoid code duplicate we can decorate around the default
		//The beauty of this is it can still deserialize like this. The flags information will
		//still be able to be read when deserializing the default info. This was an unexpected beauty
		//of this implementation
		[WireMember(1)]
		public DefaultRealmInformation DefaultInformation { get; }

		/// <summary>
		/// Contains the build and version information for the realm.
		/// </summary>
		[WireMember(2)]
		public RealmBuildInformation BuildInfo { get; }

		#region Auto Implemented Decoration

		public RealmFlags Flags => ((IRealmInformation)DefaultInformation).Flags;

		public string RealmString => ((IRealmInformation)DefaultInformation).RealmString;

		public RealmEndpoint RealmAddress => ((IRealmInformation)DefaultInformation).RealmAddress;

		public float PopulationLevel => ((IRealmInformation)DefaultInformation).PopulationLevel;

		public byte CharacterCount => ((IRealmInformation)DefaultInformation).CharacterCount;

		public byte RealmTimeZone => ((IRealmInformation)DefaultInformation).RealmTimeZone;

		public byte RealmId => ((IRealmInformation)DefaultInformation).RealmId;

		#endregion
	}
}
