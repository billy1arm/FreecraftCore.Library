using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of entity types. See Trinitycore's ObjectGuid.h
	/// </summary>
	public enum EntityTypeId : byte
	{
		TYPEID_OBJECT = 0,
		TYPEID_ITEM = 1,
		TYPEID_CONTAINER = 2,
		TYPEID_UNIT = 3,
		TYPEID_PLAYER = 4,
		TYPEID_GAMEOBJECT = 5,
		TYPEID_DYNAMICOBJECT = 6,
		TYPEID_CORPSE = 7
	};
}
