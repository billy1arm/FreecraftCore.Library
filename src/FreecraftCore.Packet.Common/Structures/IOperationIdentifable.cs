using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Contract for types that are identifyable by an operation code.
	/// </summary>
	/// <typeparam name="TOperationCode">The operation code type.</typeparam>
	public interface IOperationIdentifable<TOperationCode>
		where TOperationCode : struct //can't constrain to enum but this is close.
	{
		/// <summary>
		/// The code identity for the object.
		/// </summary>
		TOperationCode OperationCode { get; }
	}
}
