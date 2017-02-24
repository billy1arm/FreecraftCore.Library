using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Service contract for types that can register network input pipelines.
	/// </summary>
	/// <typeparam name="TNetworkInputType">The input type for the pipeline.</typeparam>
	/// <typeparam name="TContextBuilderType"></typeparam>
	/// <typeparam name="TNetworkOperationCode"></typeparam>
	/// <typeparam name="THeaderType"></typeparam>
	/// <typeparam name="TPayloadType"></typeparam>
	public interface INetworkInputPipelineRegister<TNetworkInputType, out TContextBuilderType, TNetworkOperationCode, THeaderType, TPayloadType> //do not remove these type parameters. They are needed for the extension API
		where TNetworkOperationCode : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCode>
		where TPayloadType : IMessageVerifyable
	{
		//TODO: Support ordering and location
		/// <summary>
		/// Tries to register a pipeline component with the specified <see cref="NetworkPipelineTypes"/>.
		/// </summary>
		/// <param name="pipelineComponent">The pipeline component to register.</param>
		/// <param name="pipelineType">What this pipeline should register itself as.</param>
		/// <returns>True if the pipeline was successfully handled.</returns>
		bool TryRegisterPipeline(IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TContextBuilderType> pipelineComponent, NetworkPipelineTypes pipelineType);

		/// <summary>
		/// Tries to register a pipeline component with the specified <see cref="NetworkPipelineTypes"/>.
		/// </summary>
		/// <param name="pipelineComponent">The pipeline component to register.</param>
		/// <returns>True if the pipeline was successfully handled.</returns>
		bool TryRegisterPipeline(IPipelineListenerAsync<TNetworkInputType, TNetworkInputType, TContextBuilderType> pipelineComponent, ICompleteOptionsReadable options);
	}
}
