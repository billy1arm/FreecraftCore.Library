using FreecraftCore.Packet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Metadata market for an authentication payload.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)] //sometimes multiple operations will send the same payload
	public class AuthenticationPayloadAttribute : Attribute
	{
		/// <summary>
		/// Represents the authentication operation (or Auth Command in Mangos/Trinitycore)
		/// </summary>
		public AuthOperationCode OperationCode { get; }
 
		public AuthenticationPayloadAttribute(AuthOperationCode opCode)
		{
			OperationCode = opCode;
		}
	}
}
