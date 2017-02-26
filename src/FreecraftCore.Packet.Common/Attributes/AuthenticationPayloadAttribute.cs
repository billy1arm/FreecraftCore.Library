using FreecraftCore.Packet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore.Serializer;


namespace FreecraftCore.Packet
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

		/// <summary>
		/// Indicates the desttination code for the payload.
		/// </summary>
		public AuthOperationDestinationCode DestinationCode { get; }

		public AuthenticationPayloadAttribute(AuthOperationCode operationCode, AuthOperationDestinationCode destinationCode)
			: base((((int)operationCode) << 8) + (int)destinationCode) //this shifts the opcode (Ex. 0000 0001 to become 0000 0001 0000 0000) and then inserts the destination code (Ex. 0000 0001 0000 0001) which is written little endian with the real opcode being closest
		{
			if (!Enum.IsDefined(typeof(AuthOperationCode), operationCode))
				throw new ArgumentOutOfRangeException(nameof(operationCode), "Value should be defined in the AuthOperationCode enum.");

			if (!Enum.IsDefined(typeof(AuthOperationDestinationCode), destinationCode))
				throw new ArgumentOutOfRangeException(nameof(destinationCode), "Value should be defined in the AuthOperationDestinationCode enum.");

			OperationCode = operationCode;
			DestinationCode = destinationCode;
		}
	}
}
