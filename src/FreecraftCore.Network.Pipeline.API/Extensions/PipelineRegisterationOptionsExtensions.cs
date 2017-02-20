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
		public static PipelineRegisterationOptionsWithOrder<TOrderStrategyType> WithOrder<TOrderStrategyType>(this UnorderedPipelineRegisterationOptions options)
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
		public static AggregateOptionsWithOrderStrategyDecorated WithOrder(this UnorderedPipelineRegisterationOptions options, IPipelineOrderingStrategy orderingStrategy)
		{
			//Aggregate the options
			return new AggregateOptionsWithOrderStrategyDecorated(orderingStrategy, options.PipelineFlags);
		}

		/// <summary>
		/// Lets you specificy pipelines this pipeline should bind itself to.
		/// </summary>
		/// <typeparam name="TRegisterationType">The extended object type.</typeparam>
		/// <param name="options"></param>
		/// <param name="pipelineTypeFlagsStrategy"></param>
		/// <returns></returns>
		public static TRegisterationType ForType<TRegisterationType>(this TRegisterationType options, IPipelineTypeFlagsStrategy pipelineTypeFlagsStrategy)
			where TRegisterationType : IPipelineTypeFlagsMutatable
		{
			//Preforms the transformation of the flags based on the provided strategy.
			options.PipelineFlags = pipelineTypeFlagsStrategy.TransformTypeFlags(options.PipelineFlags);

			//transform the option's flags
			return options;
		}
	}
}
