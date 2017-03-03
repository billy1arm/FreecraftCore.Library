using System;
using System.Collections.Generic;
using System.Linq;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Represents a single character for the selection screen.
	/// </summary>
	[WireDataContract]
	public class CharacterScreenCharacter
	{
		/// <summary>
		/// The GUID of the character.
		/// </summary>
		[WireMember(1)]
		public ObjectGuid CharacterGuid { get; private set; }

		/// <summary>
		/// The character's name.
		/// </summary>
		[WireMember(2)]
		public string CharacterName { get; private set; } //null terminated

		/// <summary>
		/// The character's race.
		/// </summary>
		[WireMember(3)]
		public CharacterRace Race { get; private set; }

		/// <summary>
		/// The class of the character.
		/// </summary>
		[WireMember(4)]
		public CharacterClass Class { get; private set; }

		/// <summary>
		/// The gender setting of the character.
		/// </summary>
		[WireMember(5)]
		public CharacterGender Gender { get; private set; }

		//TODO: Implement the looks
		//This is uint8 Skin, uint8 Face, uint8 Hairstyle, uint8 hairColor, uint8 facialStyle
		[KnownSize(5)] //is sent as 5 bytes in JackPoz's setup
		[WireMember(6)]
		public byte[] UnknownBytesOne { get; private set; }

		/// <summary>
		/// The character's level.
		/// </summary>
		[WireMember(7)]
		public byte CharacterLevel { get; private set; }

		[WireMember(8)]
		public TempLocationStructure Location { get; private set; }

		[WireMember(9)]
		public uint GuildId { get; private set; }

		//TODO: See Trinitycore's HandleCharEnum to see how these flags look and implement it
		[WireMember(10)]
		public uint CharacterFlags { get; private set; }

		//TODO: See Trinitycore's HandleCharEnum to see how the selection customization optional flags are set
		[WireMember(11)]
		public uint SelectionFlags { get; private set; }

		/// <summary>
		/// Indicates if this is the first login by the account.
		/// </summary>
		[WireMember(12)]
		public bool isFirstLogin { get; private set; } //This may be used to tell it to go to character customization?

		/// <summary>
		/// The visual display pet information.
		/// </summary>
		[WireMember(13)]
		public CharacterScreenPetInfo PetInformation { get; private set; }

		/// <summary>
		/// Represents the display IDs of the various items equipped by a character so that
		/// the client can render it.
		/// </summary>
		[KnownSize(19)] //Jackpoz has this set as 20 items but reads length - 1 and Trinitycore sends 19 items. SO THERE ARE 19!!
		[WireMember(14)]
		public CharacterScreenItem[] VisualEquipmentItems { get; private set; }

		//TODO: Why is this sent?
		[KnownSize(4)] //jackpoz bot shows they send 4 bags
		[WireMember(15)]
		public CharacterScreenBag[] Bags { get; private set; }

		protected CharacterScreenCharacter()
		{
			
		}
	}
}
