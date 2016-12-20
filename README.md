# FreecraftCore.Packet

FreecraftCore is an open-source C#-based 3.3.5 World of Warcraft emulation project. It is derived from the work of Mangos and Trinitycore.

FreecraftCore.Packet is a collection of libraries that lay out the format and design for packets and payloads for FreecraftCore. It depends on common libraries and the [FreecraftCore.Serializer](https://github.com/FreecraftCore/FreecraftCore.Serializer) which is a metadata based serializer based on Blizzard's JAM.

## Builds

Available on a Nuget Feed: https://www.myget.org/F/freecraftcore/api/v2 [![freecraftcore MyGet Build Status](https://www.myget.org/BuildSource/Badge/freecraftcore?identifier=c8b700be-7ec4-4a5b-87a0-f663ab446ad0)](https://www.myget.org/)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/FreecraftCore/FreecraftCore.Packet.svg?branch=master)](https://travis-ci.org/FreecraftCore/FreecraftCore.Packet) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/FreecraftCore/FreecraftCore.Packet.svg?branch=dev)](https://travis-ci.org/FreecraftCore/FreecraftCore.Packet)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the GNU AFFERO GENERAL PUBLIC LICENSE so follow it or I will report you to the software [gods](https://www.gnu.org/licenses/gpl-violation.en.html).
