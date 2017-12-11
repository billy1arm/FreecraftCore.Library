using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	//TODO: This will be handled in a special serializer
	[WireDataContract]
	public class PackedGuid : BaseGuid
	{
		/// <summary>
		/// Represents an Empty or uninitialized <see cref="PackedGuid"/>.
		/// </summary>
		public static PackedGuid Empty { get; } = new PackedGuid(0);

		/// <inheritdoc />
		public override ulong RawGuidValue { get; }

		/// <summary>
		/// Creates a new value-type wrapped for the uint64 raw GUID value.
		/// </summary>
		/// <param name="guidValue">Raw GUID value.</param>
		public PackedGuid(ulong guidValue)
		{
			RawGuidValue = guidValue;
		}

		protected PackedGuid()
		{
			//Serializer ctor
		}

		/// <summary>
		/// Implict cast to ulong (uint64 TC/C++)
		/// </summary>
		/// <param name="guid"></param>
		public static implicit operator ulong(PackedGuid guid)
		{
			return guid.RawGuidValue;
		}
	}
}
