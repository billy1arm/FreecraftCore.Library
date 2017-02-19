using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Packet
{
	public static class EnumVerificationUtil
	{
		/// <summary>
		/// Verifies that a provided <typeparam name="TEnumType"></typeparam> value is defined within
		/// the enumeration. (DOES NOT WORK FOR FLAGS)
		/// </summary>
		/// <typeparam name="TEnumType">The type of the enumeration (can be infered usually)</typeparam>
		/// <param name="value">The value of the enumeration (is extended on).</param>
		/// <param name="name">The name of the value to be checked (only used for exception cases.</param>
		/// <param name="throwIfInvalid">Optional argument that can throw if invalid.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <see cref="throwIfInvalid"/> is true and the <see cref="value"/> is not in a valid Enum range.</exception>
		/// <returns>True if valid. False or throws if invalid.</returns>
		public static bool VerifyIsDefined<TEnumType>([CanBeNull] this TEnumType value, string name, bool throwIfInvalid = false)
			where TEnumType : struct, IConvertible
		{
			//This reflection may be expensive in release builds
#if DEBUG || DEBUGBUILD
			if(value.GetType().GetTypeInfo().IsEnum)
#endif
				if (Enum.IsDefined(typeof(TEnumType), value))
					return true;

			if (throwIfInvalid)
				throw new ArgumentOutOfRangeException(nameof(value), $"The enumeration {nameof(value)} of Type: {typeof(TEnumType).FullName} with Name: {name} is not valid for the enumeration range.");
			else
				return false;
		}

		/// <summary>
		/// Verifies that a provided <typeparam name="TEnumType"></typeparam> value is defined within
		/// the enumeration. (DOES NOT WORK FOR FLAGS)
		/// </summary>
		/// <typeparam name="TEnumType">The type of the enumeration (can be infered usually)</typeparam>
		/// <param name="value">The value of the enumeration (is extended on).</param>
		/// <returns>True if valid. False or throws if invalid.</returns>
		public static bool VerifyIsDefined<TEnumType>([CanBeNull] this TEnumType value)
			where TEnumType : struct, IConvertible
		{
			return value.VerifyIsDefined("Not Specified", false);
		}
	}
}
