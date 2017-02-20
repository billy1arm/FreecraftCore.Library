using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Internal default pipeline decorator.
	/// </summary>
	internal class DefaultFlagsPipelineTypeStrategy : IPipelineTypeFlagsStrategy
	{
		[NotNull]
		private IPipelineTypeFlagsStrategy DecoratedFlagsStrategy { get; }

		public DefaultFlagsPipelineTypeStrategy([NotNull] IPipelineTypeFlagsStrategy decoratedFlagsStrategy)
		{
			if (decoratedFlagsStrategy == null) throw new ArgumentNullException(nameof(decoratedFlagsStrategy));

			DecoratedFlagsStrategy = decoratedFlagsStrategy;
		}

		/// <inheritdoc />
		public NetworkPipelineTypes TransformTypeFlags(NetworkPipelineTypes types)
		{
			return types | DecoratedFlagsStrategy.TransformTypeFlags(NetworkPipelineTypes.Default);
		}
	}
}
