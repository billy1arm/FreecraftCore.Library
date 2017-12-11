using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of all update types that can come in an update packet.
	/// See Jackpoz's ObjectUpdateType or dig into Trinitycore to see. 
	/// </summary>
	[WireDataContract]
	public enum ObjectUpdateType : byte
	{
		/// <summary>
		/// Indicates that the type of update is a dictionary of update values.
		/// </summary>
		UPDATETYPE_VALUES = 0,

		/// <summary>
		/// Indicates that the type of update is a movement realted update.
		/// </summary>
		UPDATETYPE_MOVEMENT = 1,

		/// <summary>
		/// Indicates that the type of update is the creation of an object.
		/// </summary>
		UPDATETYPE_CREATE_OBJECT = 2,
		
		//TODO: Find out what this is
		/// <summary>
		/// Don't know.
		/// </summary>
		UPDATETYPE_CREATE_OBJECT2 = 3,

		/// <summary>
		/// Indicates that an object has gone out of range.
		/// </summary>
		UPDATETYPE_OUT_OF_RANGE_OBJECTS = 4,

		//TODO: Find out what this is
		/// <summary>
		/// Don't know
		/// </summary>
		UPDATETYPE_NEAR_OBJECTS = 5
	};
}
