using FreecraftCore.Serializer;

namespace FreecraftCore.Packet
{
	/// <summary>
	/// Represents a failed name query result.
	/// </summary>
	[WireDataContract]
	public class UnknownNameQueryResult : NameQueryResult
	{
		/// <inheritdoc />
		public override bool Successful { get; } = false;

		/// <inheritdoc />
		public override string Name { get; }

		protected UnknownNameQueryResult()
		{
			//serializer ctor
		}
	}
}