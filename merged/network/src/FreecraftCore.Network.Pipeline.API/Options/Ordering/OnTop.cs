using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Strategy for adding a pipeline to the top of the pipe.
	/// </summary>
	public class OnTop : IPipelineOrderingStrategy
	{
		/// <inheritdoc />
		public void RegisterVisitor<TPipelineType>([NotNull] IList<TPipelineType> pipelines, [NotNull] TPipelineType pipeline)
		{
			if (pipelines == null) throw new ArgumentNullException(nameof(pipelines));
			if (pipeline == null) throw new ArgumentNullException(nameof(pipeline));

			//TODO: Should we catch and throw a more meaning exception?
			pipelines.Insert(0, pipeline);
		}
	}
}
