using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common.Warden
{
	//TODO: Documentation
	//From Trinitycore Warden.h
	/// <summary>
	/// Enumeration of all sub-operation codes sent by the client for Warden.
	/// These are suboperations that only the client can send.
	/// </summary>
	[WireDataContract]
	public enum WardenSubOperationCodeClient : byte
	{
		WARDEN_CMSG_MODULE_MISSING = 0,
		WARDEN_CMSG_MODULE_OK = 1,
		WARDEN_CMSG_CHEAT_CHECKS_RESULT = 2,

		/// <summary>
		/// only sent if MEM_CHECK bytes doesn't match
		/// </summary>
		WARDEN_CMSG_MEM_CHECKS_RESULT = 3,
		WARDEN_CMSG_HASH_RESULT = 4,

		/// <summary>
		/// this is sent when client failed to load uploaded module due to cache fail
		/// </summary>
		WARDEN_CMSG_MODULE_FAILED = 5,
	}
}
