using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Enumeration of pipeline registeration locations.
	/// </summary>
	public enum NetworkPipelineRegisterationOrder
	{
		/// <summary>
		/// Indicates the order should be handled by whatever default algorithm the pipeline system uses
		/// </summary>
		Default = 0,

		/// <summary>
		/// Indicates the pipeline registeration should be inserted on top.
		/// </summary>
		Top = 1,

		/// <summary>
		/// Indicates the pipeline registeration should be inserted on the bottom.
		/// </summary>
		Bottom = 2
	}
}
