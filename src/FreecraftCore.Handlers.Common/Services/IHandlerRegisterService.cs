using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using System.Reflection;

namespace FreecraftCore.Handlers
{
	/// <summary>
	/// Service contract for registering handlers.
	/// </summary>
	public interface IHandlerRegisterService<in THandlerType, in TOperationCodeType>
		where TOperationCodeType : struct
	{
		/// <summary>
		/// Tries to register a provided handler.
		/// </summary>
		/// <param name="handler">The handler to register.</param>
		/// <param name="code">The operation code.</param>
		/// <returns>True if it was able to register the provided handler.</returns>
		bool TryRegister([NotNull] THandlerType handler, TOperationCodeType code);

		/// <summary>
		/// Tries to register a default handler for handling when
		/// no other handlers is found.
		/// </summary>
		/// <param name="handler">The default handler.</param>
		/// <returns>True if it was able to register the provided handler.</returns>
		bool TryRegisterDefault(THandlerType handler);
	}
}
