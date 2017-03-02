using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public class CharacterScreenPetInfo
	{
		//TODO: Figure out what any of this means
		[WireMember(1)]
		public uint PetInformationId { get; private set; }

		[WireMember(2)]
		public uint PetLevel { get; private set; }

		[WireMember(3)]
		public uint PetFamilyId { get; private set; }

		protected CharacterScreenPetInfo()
		{
			//serializer ctor
		}
	}
}
