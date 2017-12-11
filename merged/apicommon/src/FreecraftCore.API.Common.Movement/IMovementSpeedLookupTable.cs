using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Contract for types that implement functionality to lookup movement speed values.
	/// </summary>
	public interface IMovementSpeedLookupTable : IEnumerable<float>
	{
		/// <summary>
		/// Indicates the size of the underlying table.
		/// On 3.3.5 this will likely be 9 but on 1.12.1 this will be 6.
		/// </summary>
		int TableSize { get; }

		/// <summary>
		/// Indicates if the type is supported.
		/// </summary>
		/// <param name="moveType"></param>
		/// <returns></returns>
		bool SupportedType(UnitMoveType moveType);

		/// <summary>
		/// O(1) lookup for the specified <see cref="moveType"/> value.
		/// </summary>
		/// <param name="moveType"></param>
		/// <returns></returns>
		float this[UnitMoveType moveType] { get; }
	}
}
