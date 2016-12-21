using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreecraftCore.API.Common.Auth
{
	/// <summary>
	/// SRP token based on Trinitycore/Mangos/WoW authentication protocol.
	/// Review http://srp.stanford.edu/design.html for more information
	/// </summary>
	public class SRPToken 
	{
		/// <summary>
		/// Half of the ephemerial public key.
		/// </summary>
		[KnownSize(32)] //The size is not sent. It is known by both clients
		[WireMember(1)]
		public byte[] B { get; private set; }

		/// <summary>
		/// A generator modulo N.
		/// (I don't really know what that means)
		/// </summary>
		[SendSize(SendSizeAttribute.SizeType.Byte)] //Size is sent as a uint8. Trinitycore always sends 1
		[WireMember(2)]
		public byte[] g { get; private set; }

		/// <summary>
		/// A large safe prime (N = 2q+1, where q is prime)
		/// </summary>
		[SendSize(SendSizeAttribute.SizeType.Byte)] //Trinitycore always send 32
		[WireMember(3)]
		public byte[] N { get; private set; }

		//SRP_6_S
		/// <summary>
		/// User salt.
		/// </summary>
		[KnownSize(32)] //trinitycore sends 32 byte salt
		[WireMember(4)]
		public byte[] salt { get; private set; }

		//TODO: Find out what this is. Check Mangos or Ember. Trinitycore doesn't know.
		[KnownSize(16)] //Trinitycore does know it's 16 bytes though
		[WireMember(5)]
		public byte[] unk3 { get; private set; }
	}
}
