using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.API.Common
{
	[WireDataContract]
	public class RealmBuildInformation
	{
		[WireMember(1)]
		public ExpansionType Expansion { get; }

		[WireMember(2)]
		public byte MajorVersion { get; }

		[WireMember(3)]
		public byte MinorVersion { get; }

		//TODO: If we ever make a server add a real ctor. Right now only the server sends this

		public RealmBuildInformation()
		{

		}
	}
}
