using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Metadata marker for which protocol group a targeted message class is apart of.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)] //only allow on classes and allow it to be inherited; only one is valid
	public sealed class ProtocolGroupingAttribute : Attribute
	{
		/// <summary>
		/// Indicates the protocol this message is apart of.
		/// </summary>
		public ProtocolCode Protocol { get; }

		/// <summary>
		/// Create a new protocol metadata marker for the targeted class.
		/// </summary>
		/// <param name="protocol">The protocol to group this message in.</param>
		public ProtocolGroupingAttribute(ProtocolCode protocol)
		{
			if (!Enum.IsDefined(typeof(ProtocolCode), protocol))
				throw new ArgumentOutOfRangeException(nameof(protocol), "Value should be defined in the ProtocolCode enum.");

			Protocol = protocol;
		}
	}
}
