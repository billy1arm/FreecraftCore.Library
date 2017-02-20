using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	public static class PipelineType<TPipelineStrategyType>
		where TPipelineStrategyType : IPipelineTypeFlagsStrategy, new()
	{
		public static IPipelineTypeFlagsStrategy Default()
		{
			return new DefaultFlagsPipelineTypeStrategy(new TPipelineStrategyType());
		}

		public static IPipelineTypeFlagsStrategy Main()
		{
			return new MainFlagsPipelineTypeStrategy(new TPipelineStrategyType());
		}

		public static IPipelineTypeFlagsStrategy Error()
		{
			return new ErrorFlagsPipelineTypeStrategy(new TPipelineStrategyType());
		}
	}
}
