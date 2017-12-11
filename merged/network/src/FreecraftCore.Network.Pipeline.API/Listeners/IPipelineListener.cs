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
	public interface IPipelineListener<out TResultType, in TSourceType, in TStateType>
	{
		/// <summary>
		/// Pipeline listener that recieves a <typeparam name="TSourceType"></typeparam> and a <typeparam name="TStateType"></typeparam>
		/// context and yields a <see cref="TResultType"/>.
		/// </summary>
		/// <param name="input">The input value.</param>
		/// <param name="currentState">The current state value.</param>
		/// <returns></returns>
		[Pure]
		[NotNull]
		TResultType RecievePipelineMessage([NotNull] TSourceType input, [NotNull] TStateType currentState);
	}
}
