using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Serializer;

namespace FreecraftCore.Network
{
	/// <summary>
	/// Factory service for constructing network messaage contexts.
	/// </summary>
	/// <typeparam name="TReaderType">The type of reader to read from.</typeparam>
	/// <typeparam name="TOperationCodeType">The operation code type.</typeparam>
	/// <typeparam name="THeaderType">The header type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type.</typeparam>
	public interface INetworkMessageContextFactory<in TReaderType, TOperationCodeType, THeaderType, TPayloadType>
		where TReaderType : IWireStreamReaderStrategy
		where TOperationCodeType : struct
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TOperationCodeType>
		where TPayloadType : IMessageVerifyable
	{
		/// <summary>
		/// Constructs a <see cref="INetworkMessageContext{TOperationCodeType, THeaderType, TPayloadType}"/> from the provided
		/// <typeparam name="TReaderType"></typeparam> object.
		/// </summary>
		/// <param name="reader">The reader to read from.</param>
		/// <returns></returns>
		INetworkMessageContext<TOperationCodeType, THeaderType, TPayloadType> ConstructNetworkContext(TReaderType reader);

		/// <summary>
		/// Constructs a <see cref="INetworkMessageContext{TOperationCodeType, THeaderType, TPayloadType}"/> from the provided
		/// <typeparam name="TReaderType"></typeparam> object.
		/// </summary>
		/// <param name="reader">The reader to read from.</param>
		/// <returns></returns>
		Task<INetworkMessageContext<TOperationCodeType, THeaderType, TPayloadType>> ConstructNetworkContextAsync(TReaderType reader);
	}
}
