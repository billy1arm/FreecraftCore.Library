using FreecraftCore.Packet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore.Serializer;


namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Metadata marker for an authentication payload.
	/// Inherits from RuntimeLinkAttribute which allows types to link themselves at runtime.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)] //sometimes multiple operations will send the same payload though this is not yet supported
	public class AuthenticationPayloadAttribute : WireDataContractBaseTypeRuntimeLinkAttribute
	{
		/// <summary>
		/// Represents the authentication operation (or Auth Command in Mangos/Trinitycore)
		/// </summary>
		public AuthOperationCode OperationCode { get; }
 
		public AuthenticationPayloadAttribute(AuthOperationCode operationCode)
			: base((int)operationCode)
		{
			if (!Enum.IsDefined(typeof(AuthOperationCode), operationCode))
				throw new ArgumentOutOfRangeException(nameof(operationCode), "Value should be defined in the AuthOperationCode enum.");

			OperationCode = operationCode;
		}
	}
}
