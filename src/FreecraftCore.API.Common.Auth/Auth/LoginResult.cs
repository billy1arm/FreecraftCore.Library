using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FreecraftCore.API.Common
{
	/// <summary>
	/// Enumeration of all possible login results.
	/// </summary>
	public enum LoginResult //TODO: Find out type in packet
	{
		//TODO: Find documentation on these codes (Mangos or Ember)
		Ok = 0x00,
		Failed = 0x01,
		Failed2 = 0x02, //TODO: What is this?
		Banned = 0x03,
		UnknownAccount = 0x04,
		UnknownAccount3 = 0x05, //TODO: What is this?
		AlreadyOnline = 0x06,
		NoGameTime = 0x07,
		DatabaseBusy = 0x08,
		BadVersion = 0x09,
		DownloadFile = 0x0A, //TODO: What is this?
		Failed3 = 0x0B, //TODO: What is this?
		Suspended = 0x0C,
		Failed4 = 0x0D, //TODO: What is this?
		Connected = 0x0E,
		ParentalControlBlocked = 0x0F, //I think this is block
		LockedEnforced = 0x10
	};
}
