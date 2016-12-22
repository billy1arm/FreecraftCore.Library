# FreecraftCore.Crypto

FreecraftCore is an open-source C#-based 3.3.5 and 1.12.1 World of Warcraft emulation project. It is derived from the work of Mangos and Trinitycore.

The FreecraftCore.Crypto is a library that implements the [SRP6 Protocol](http://srp.stanford.edu/design.html) and implements the custom Blizzard hashing algorithm for session key hashing. Additionally it implements a SHA1(A, HMAC(client_files)) (misnamed CRC or unk3 in packets) service.

# Functionality

### BigInteger for .Net3.5

Backported version of System.Numerics.BigInteger for .NET 3.5 based on the .NET Framework [source](https://referencesource.microsoft.com/#System.Numerics/System/Numerics/BigInteger.cs).

### SRP6

Implements SHA1 (unsecure) based [SRP6](http://srp.stanford.edu/design.html)

### Custom Session Key Hash

Implements custom session key hashing based on the [Mangos Client](https://github.com/vermie/MangosClient/blob/master/Client/Authentication/Network/AuthSocket.cs)'s work.

### Client HMAC

Implements hashing aglorithm used for AUTH_LOGON_PROOF's crc_hash (misnamed) using the unk3 (misnamed) seed that is sent with the challenge response.

## Builds

Available on a Nuget Feed: https://www.myget.org/F/freecraftcore/api/v2 [![freecraftcore MyGet Build Status](https://www.myget.org/BuildSource/Badge/freecraftcore?identifier=c8b700be-7ec4-4a5b-87a0-f663ab446ad0)](https://www.myget.org/)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/FreecraftCore/FreecraftCore.Crypto.svg?branch=master)](https://travis-ci.org/FreecraftCore/FreecraftCore.Crypto) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/FreecraftCore/FreecraftCore.Crypto.svg?branch=dev)](https://travis-ci.org/FreecraftCore/FreecraftCore.Crypto)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the GNU AFFERO GENERAL PUBLIC LICENSE so follow it or I will report you to the software [gods](https://www.gnu.org/licenses/gpl-violation.en.html).
