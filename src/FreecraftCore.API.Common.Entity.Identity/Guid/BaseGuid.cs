using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	public abstract class BaseGuid
	{
		/// <summary>
		/// GUID value.
		/// </summary>
		public abstract ulong RawGuidValue { get; } //setter only for serialization

		/// <summary>
		/// Indicates the object Type that the <see cref="GUID"/> is associated with.
		/// </summary>
		EntityGuidMask ObjectType => (EntityGuidMask)((RawGuidValue >> 48) & 0x0000FFFF);

		/// <summary>
		/// Indiciates the current GUID of the object. This is the last chunk represents the id that the world server assigned to the object. (The rest is just maskable flags about the object)
		/// </summary>
		public int CurrentObjectGuid => (int)(RawGuidValue & 0x0000000000FFFFFF);

		/// <summary>
		/// Indiciates if the GUID is an empty or unitialized GUID.
		/// </summary>
		/// <returns></returns>
		public bool isEmpty()
		{
			return RawGuidValue == 0;
		}

		/// <summary>
		/// Indicates if the <see cref="BaseGuid"/> is associated with an Object Type <paramref name="guidType"/>.
		/// </summary>
		/// <param name="guidType">Type of GUID.</param>
		/// <returns>True if <see cref="ObjectType"/> is the same as <paramref name="guidType"/>.</returns>
		public bool isType(EntityGuidMask guidType)
		{
			return guidType == ObjectType;
		}
	}
}
