using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Metadata marker for message handlers that are for authentication payloads.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class AuthenticationMessageHandlerAttribute : Attribute, IOperationIdentifable<AuthOperationCode>
	{
		/// <summary>
		/// The operation code of the handlers.
		/// </summary>
		public AuthOperationCode OperationCode { get; }

		public AuthenticationMessageHandlerAttribute(AuthOperationCode operationCode)
		{
			if (!Enum.IsDefined(typeof(AuthOperationCode), operationCode))
				throw new ArgumentOutOfRangeException(nameof(operationCode), "Value should be defined in the AuthOperationCode enum.");

			OperationCode = operationCode;
		}
	}
}
