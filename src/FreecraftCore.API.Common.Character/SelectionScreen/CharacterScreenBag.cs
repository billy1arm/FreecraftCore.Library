using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Represents the bags on the character screen.
	/// Mostly unknown values.
	/// </summary>
	[WireDataContract]
	public class CharacterScreenBag
	{
		//TODO: Figure out what these values are or do
		[WireMember(1)]
		private uint UnknownOne { get; set; }

		[WireMember(2)]
		private byte UnknownTwo { get; set; }

		[WireMember(3)]
		private uint UnknownThree { get; set; }

		protected CharacterScreenBag()
		{
			//serializer ctor
		}
	}
}
