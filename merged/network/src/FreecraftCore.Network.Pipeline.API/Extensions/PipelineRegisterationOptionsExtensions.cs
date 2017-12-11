using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreecraftCore.Network
{
	public static class PipelineRegisterationOptionsExtensions
	{
		/// <summary>
		/// Denotes the order in which the pipeline will be registered.
		/// Allows you to register it in locations such as top, bottom or in the middle.
		/// </summary>
		/// <typeparam name="TOrderStrategyType"></typeparam>
		/// <param name="options"></param>
		/// <returns></returns>
		public static PipelineRegisterationOptionsWithOrder<TOrderStrategyType> WithOrder<TOrderStrategyType>(this ICompleteOptionsReadable options)
			where TOrderStrategyType : IPipelineOrderingStrategy, new()
		{
			//Produce a built options of the aggregate of the order strategy and the flags
			return new PipelineRegisterationOptionsWithOrder<TOrderStrategyType>(options.PipelineFlags);
		}

		/// <summary>
		/// Denotes the order in which the pipeline will be registered.
		/// Allows you to register it in locations such as top, bottom or in the middle.
		/// </summary>
		/// <typeparam name="TOrderStrategyType"></typeparam>
		/// <param name="options"></param>
		/// <returns></returns>
		public static AggregateOptionsWithOrderStrategyDecorated WithOrder(this ICompleteOptionsReadable options, IPipelineOrderingStrategy orderingStrategy)
		{
			//Aggregate the options
			return new AggregateOptionsWithOrderStrategyDecorated(orderingStrategy, options.PipelineFlags);
		}

		public static ICompleteOptionsReadable For<TPipelineType>(this ICompleteOptionsReadable options)
			where TPipelineType : IPipelineTypeFlagsStrategy, new()
		{
			return new AggregateOptionsWithOrderStrategyDecorated(options, new TPipelineType().TransformTypeFlags(options.PipelineFlags));
		}
	}
}
