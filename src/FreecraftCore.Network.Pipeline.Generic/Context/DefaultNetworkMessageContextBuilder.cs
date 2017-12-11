using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Packet;
using FreecraftCore.Packet.Common;
using JetBrains.Annotations;

namespace FreecraftCore.Network
{
	/// <summary>
	/// The default context builder.
	/// </summary>
	/// <typeparam name="TNetworkOperationCodeType"></typeparam>
	/// <typeparam name="THeaderType"></typeparam>
	/// <typeparam name="TPayloadType"></typeparam>
	public class DefaultNetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType> : INetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType> 
		where TNetworkOperationCodeType : struct 
		where THeaderType : IMessageVerifyable, IOperationIdentifable<TNetworkOperationCodeType> 
		where TPayloadType : IMessageVerifyable
	{
		/// <inheritdoc />
		public bool isBuilt => CachedBuiltContext.IsValueCreated;

		/// <inheritdoc />
		public THeaderType GameHeader { get; set; }

		/// <inheritdoc />
		public TPayloadType GamePayload { get; set; }

		/// <summary>
		/// Lazily built context that will serve as the built context when accessed.
		/// </summary>
		[NotNull]
		private Lazy<INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType>> CachedBuiltContext { get; }

		/// <summary>
		/// Provide a delegate that can create a new <see cref="INetworkMessageContext{TNetworkOperationCodeType, THeaderType, TPayloadType}"/>
		/// </summary>
		/// <param name="contextCreationFunc">Creation func.</param>
		public DefaultNetworkMessageContextBuilder([NotNull] Func<DefaultNetworkMessageContextBuilder<TNetworkOperationCodeType, THeaderType, TPayloadType>, INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType>> contextCreationFunc)
		{
			if (contextCreationFunc == null) throw new ArgumentNullException(nameof(contextCreationFunc));

			//Use the provided creation func; It's a little confusing but we need to capture this and provide it to the func so it can access the props in this
			//class to initialize a context
			CachedBuiltContext = new Lazy<INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType>>(() => contextCreationFunc(this), true);
		}

		/// <inheritdoc />
		public INetworkMessageContext<TNetworkOperationCodeType, THeaderType, TPayloadType> Build()
		{
			return CachedBuiltContext.Value;
		}
	}
}
