using FreecraftCore.API.Common;
using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	[WireDataContract]
	public class KnownNameQueryResult : NameQueryResult
	{
		/// <inheritdoc />
		public override bool Successful { get; } = true;

		[WireMember(1)]
		private readonly string _name;

		/// <summary>
		/// The name of the result.
		/// </summary>
		public override string Name => _name;

		/// <summary>
		/// The name of the realm they're on.
		/// (used for cross-realm)
		/// </summary>
		[WireMember(2)]
		public string RealmName { get; private set; }

		/// <summary>
		/// Indicates the race associated with the guid.
		/// </summary>
		[WireMember(3)]
		public CharacterRace Race { get; private set; }

		/// <summary>
		/// Indicates the gender associated with the guid.
		/// </summary>
		[WireMember(4)]
		public CharacterGender Gender { get; private set; }

		/// <summary>
		/// Indicates the class associated with the guid.
		/// </summary>
		[WireMember(5)]
		public CharacterClass Class { get; private set; }

		//TODO: Implement Declined name

		protected KnownNameQueryResult()
		{
			//serializer ctor
		}
	}
}