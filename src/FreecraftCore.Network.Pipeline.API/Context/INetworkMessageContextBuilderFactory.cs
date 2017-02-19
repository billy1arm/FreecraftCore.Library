using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	//This can be used as a vector for context recycling
	//Yes, this sound very enterprisey. It makes sense though. It produces builders which can be akward to create.
	/// <summary>
	/// Service that can create message context builders.
	/// </summary>
	public interface INetworkMessageContextBuilderFactory<out TContextBuilderType, TOperationCodeType, THeaderType, TPayloadType>
		where TContextBuilderType : INetworkMessageContextBuilder<TOperationCodeType, THeaderType, TPayloadType>
		where TPayloadType : IMessageVerifyable 
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType> 
		where TOperationCodeType : struct
	{
		/// <summary>
		/// Produces a new context builder.
		/// </summary>
		/// <returns></returns>
		[NotNull]
		TContextBuilderType CreateNew();
	}
}
