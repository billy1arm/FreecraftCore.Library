using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore.Serializer;


namespace FreecraftCore.API.Common
{
	//value-type wrapper for the object GUID from Trinitycore
	[WireDataContract]
	public class ObjectGuid : BaseGuid//using a class reduces GC pressure but this object should be treated as a value-type and is immutable
	{
		/// <summary>
		/// Represents an Empty or uninitialized <see cref="ObjectGuid"/>.
		/// </summary>
		public static ObjectGuid Empty { get; } = new ObjectGuid(0);

		/// <summary>
		/// GUID value.
		/// </summary>
		[WireMember(1)]
		public override ulong RawGuidValue { get; }

		/// <summary>
		/// Creates a new value-type wrapped for the uint64 raw GUID value.
		/// </summary>
		/// <param name="guidValue">Raw GUID value.</param>
		public ObjectGuid(ulong guidValue)
		{
			RawGuidValue = guidValue;
		}

		protected ObjectGuid()
		{
			//Serializer ctor
		}

		/// <summary>
		/// Implict cast to ulong (uint64 TC/C++)
		/// </summary>
		/// <param name="guid"></param>
		public static implicit operator ulong(ObjectGuid guid)
		{
			return guid.RawGuidValue;
		}

		/// <summary>
		/// Implict cast to ulong (uint64 TC/C++)
		/// </summary>
		/// <param name="guid"></param>
		public static implicit operator ObjectGuid(ulong guid)
		{
			return new ObjectGuid(guid);
		}
	}
}
