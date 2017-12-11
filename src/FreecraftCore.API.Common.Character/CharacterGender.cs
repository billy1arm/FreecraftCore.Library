using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of all gender states
	/// </summary>
	[WireDataContract]
	public enum CharacterGender : byte
	{
		Male = 0,
		Female = 1,
		Genderless = 2
	}
}
