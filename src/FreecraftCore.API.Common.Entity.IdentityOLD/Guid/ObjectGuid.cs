using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreecraftCore.API.Common
{
	//value-type wrapper for the object GUID from Trinitycore
	public class ObjectGuid //using a class reduces GC pressure but this object should be treated as a value-type and is immutable
	{
		/// <summary>
		/// Represents an Empty or uninitialized <see cref="ObjectGuid"/>.
		/// </summary>
		public static ObjectGuid Empty { get; } = new ObjectGuid(0);

		/// <summary>
		/// GUID value.
		/// </summary>
		public ulong RawGuidValue { get; }

		/// <summary>
		/// Indicates the object Type that the <see cref="GUID"/> is associated with.
		/// </summary>
		EntityGuidMask ObjectType { get { return (EntityGuidMask)((RawGuidValue >> 48) & 0x0000FFFF); } }

		/// <summary>
		/// Indiciates if the GUID is an empty or unitialized GUID.
		/// </summary>
		/// <returns></returns>
		public bool isEmpty()
		{
			return RawGuidValue == 0;
		}

		/// <summary>
		/// Indiciates the current GUID of the object. This is the last chunk represents the id that the world server assigned to the object. (The rest is just maskable flags about the object)
		/// </summary>
		public int CurrentObjectGuid { get { return (int)(RawGuidValue & 0x0000000000FFFFFF); } }

		/// <summary>
		/// Indicates if the <see cref="ObjectGuid"/> is associated with an Object Type <paramref name="guidType"/>.
		/// </summary>
		/// <param name="guidType">Type of GUID.</param>
		/// <returns>True if <see cref="ObjectType"/> is the same as <paramref name="guidType"/>.</returns>
		public bool isType(EntityGuidMask guidType)
		{
			return guidType == ObjectType;
		}

		/// <summary>
		/// Creates a new value-type wrapped for the uint64 raw GUID value.
		/// </summary>
		/// <param name="guidValue">Raw GUID value.</param>
		public ObjectGuid(ulong guidValue)
		{
			RawGuidValue = guidValue;
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
