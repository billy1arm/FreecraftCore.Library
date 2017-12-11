using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Base contract for all pipeline types.
	/// </summary>
	/// <typeparam name="TResultType">Result type to yield at the end of recieve.</typeparam>
	/// <typeparam name="TSourceType">The source type to recieve.</typeparam>
	/// <typeparam name="TStateType">The state type to manage.</typeparam>
	public interface IPipelineListenerAsync<TResultType, in TSourceType, in TStateType> : IPipelineListener<Task<TResultType>, TSourceType, TStateType>
	{

	}
}
