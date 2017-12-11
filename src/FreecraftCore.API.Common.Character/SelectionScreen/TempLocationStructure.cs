using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	//TODO: Replace with something real when we need to actually use this stuff
	/// <summary>
	/// Temporary structure that represents the location of an object
	/// </summary>
	[WireDataContract]
	public sealed class TempLocationStructure
	{
		//TODO: Encapsulate this in a location object when we make it
		[WireMember(1)]
		public uint ZoneId { get; }

		//TODO: Encapsulate this in a location object when we make it
		[WireMember(2)]
		public uint MapId { get; }

		//TODO: Encapsulate this in the Vector3 in System.Numerics with SIMD or create a Vector3 class
		[WireMember(3)]
		public float XPosition { get; }

		[WireMember(4)]
		public float YPosition { get; }

		[WireMember(5)]
		public float ZPosition { get; }

		//TODO: Create a real ctor if we ever make a server
		protected TempLocationStructure()
		{
			//serializer ctor
		}
	}
}
