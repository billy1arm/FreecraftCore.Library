using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common.Warden
{
	//TODO: Documentation
	//From Trinitycore Warden.h
	/// <summary>
	/// Enumeration of all sub-operation codes sent by the server for Warden.
	/// These are suboperations that only the server can send.
	/// </summary>
	[WireDataContract]
	public enum WardenSubOperationCodeServer : byte
	{
		WARDEN_SMSG_MODULE_USE = 0,
		WARDEN_SMSG_MODULE_CACHE = 1,
		WARDEN_SMSG_CHEAT_CHECKS_REQUEST = 2,
		WARDEN_SMSG_MODULE_INITIALIZE = 3,

		/// <summary>
		/// byte len; while (!EOF) { byte unk(1); byte index(++); string module(can be 0); int offset; byte len; byte[] bytes_to_compare[len]; }
		/// </summary>
		WARDEN_SMSG_MEM_CHECKS_REQUEST = 4, 
		WARDEN_SMSG_HASH_REQUEST = 5
	}
}
