using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FreecraftCore.API.Common
{
	//From Trinitycore AuthCodes.h
	/// <summary>
	/// Enumeration of all possible results for the authentication
	/// process.
	/// </summary>
	public enum AuthenticationResult : byte //uint8 in payloads
	{
		//What are most of these? They seem ridiculous
		//TODO: Bring in Mangos documentation for these codes
		//TODO: Cross ref with Ember and other cores
		Success = 0x00,
		FailBanned = 0x03,
		FailUnknownAccount = 0x04,
		FailIncorrectPassword = 0x05,
		FailAlreadyOnline = 0x06,
		FailNoGameTime = 0x07,
		FailDatabaseBusy = 0x08,
		FailVersionInvalid = 0x09,
		FailVersionUpdate = 0x0A,
		FailInvalidServer = 0x0B,
		FailSuspended = 0x0C,
		FailNoAccess = 0x0D,
		Success_Survey = 0x0E,
		FailParentalControl = 0x0F,
		FailLockedEnforced = 0x10,
		FailTrialEnded = 0x11,
		FailUseBattlenet = 0x12,
		FailAntiIndulgence = 0x13,
		FailExpired = 0x14,
		FailNoGameAccount = 0x15,
		FailChargeback = 0x16,
		FailInternetGameRoomWithoutBNet = 0x17,
		FailGameAccountLocked = 0x18,
		FailUnlockableLock = 0x19,
		FailConversionRequired = 0x20,
		FailDisconnected = 0xFF
	};
}
