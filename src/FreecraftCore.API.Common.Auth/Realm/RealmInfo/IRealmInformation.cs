using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Base contract of information a realm definition contains.
	/// </summary>
	[DefaultChild(typeof(DefaultRealmInformation))]
	[WireDataContractBaseTypeByFlags((int)RealmFlags.SpecifyBuild, typeof(RealmCompleteInformation))]
	[WireDataContract(WireDataContractAttribute.KeyType.Byte, InformationHandlingFlags.DontConsumeRead)] //AuthServer sents byte flags that can be used to determine type information
	public interface IRealmInformation
	{
		/// <summary>
		/// Packed information about the realm.
		/// </summary>
		RealmFlags Flags { get; }

		/// <summary>
		/// The string the realm should display on the realmlist tab.
		/// This might not be only the name. It could include build information.
		/// </summary>
		string RealmString { get; }

		/// <summary>
		/// Endpoint information for the realm.
		/// </summary>
		RealmEndpoint RealmAddress { get; }

		//TOOD: Research Mangos and Ember to find out why this is a float.
		//Odd that this is a float.
		float PopulationLevel { get; }

		/// <summary>
		/// Indicates how many character's the account of the client has
		/// on this realm.
		/// </summary>
		byte CharacterCount { get; }

		//TODO: Ok, which time zone maps to which byte?
		byte RealmTimeZone { get; }

		//2.x and 3.x clients expect the realm index.
		//1.12.1 just expect a byte-sized 0
		/// <summary>
		/// Indicates the ID of the realm.
		/// </summary>
		byte RealmId { get; }

		//Optional data may also be sent but implementers of this interface should deal with it.
	}
}
