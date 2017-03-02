using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Represents an item worn on the character screen.
	/// Mostly unknown or unhandled values right now.
	/// </summary>
	[WireDataContract]
	public class CharacterScreenItem
	{
		//TODO: Find out what this means

		[WireMember(1)]
		public uint DisplayId { get; private set; }

		//TODO: Find out what this means

		[WireMember(2)]
		public byte InventoryType { get; private set; }

		//TODO: Find out what this means

		[WireMember(3)]
		private uint UnknownOne { get; set; }

		public CharacterScreenItem(uint displayId, byte inventoryType, uint unknownOne)
		{
			DisplayId = displayId;
			InventoryType = inventoryType;
			UnknownOne = unknownOne;
		}

		protected CharacterScreenItem()
		{
			//serializer ctor
		}
	}
}
