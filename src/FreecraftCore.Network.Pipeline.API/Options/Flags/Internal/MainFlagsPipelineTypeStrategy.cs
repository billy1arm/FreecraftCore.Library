using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Internal main pipeline decorator.
	/// </summary>
	internal class MainFlagsPipelineTypeStrategy : IPipelineTypeFlagsStrategy
	{
		[NotNull]
		private IPipelineTypeFlagsStrategy DecoratedFlagsStrategy { get; }

		public MainFlagsPipelineTypeStrategy([NotNull] IPipelineTypeFlagsStrategy decoratedFlagsStrategy)
		{
			if (decoratedFlagsStrategy == null) throw new ArgumentNullException(nameof(decoratedFlagsStrategy));

			DecoratedFlagsStrategy = decoratedFlagsStrategy;
		}

		/// <inheritdoc />
		public NetworkPipelineTypes TransformTypeFlags(NetworkPipelineTypes types)
		{
			return types | DecoratedFlagsStrategy.TransformTypeFlags(NetworkPipelineTypes.Main);
		}
	}
}
