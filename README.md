# FreecraftCore.Network

FreecraftCore is an open-source C#/.NET World of Warcraft emulation project. It is derived from the reverse engineering work of Mangos and Trinitycore.

FreecraftCore.Network is a collection of libraries that implements the core networking components for a functioning network client and serverside for World of Warcraft emulation.

# Functionality

### [Async Augmentable Pipeline System](https://github.com/FreecraftCore/FreecraftCore.Network/blob/dev/docs/PipelineSystem.md)

The pipeline system is the core component responsible for building the incoming networking handling logic. Additionally this pipeline system is extended and used in the message handling API as well. Refer to the [documentation](https://github.com/FreecraftCore/FreecraftCore.Network/blob/dev/docs/PipelineSystem.md) for more information on how this works.

## Builds

Available on a Nuget Feed: https://www.myget.org/F/freecraftcore/api/v2 [![freecraftcore MyGet Build Status](https://www.myget.org/BuildSource/Badge/freecraftcore?identifier=c8b700be-7ec4-4a5b-87a0-f663ab446ad0)](https://www.myget.org/)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/FreecraftCore/FreecraftCore.Network.svg?branch=master)](https://travis-ci.org/FreecraftCore/FreecraftCore.Network) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/FreecraftCore/FreecraftCore.Network.svg?branch=dev)](https://travis-ci.org/FreecraftCore/FreecraftCore.Network)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the GNU AFFERO GENERAL PUBLIC LICENSE so follow it or I will report you to the software [gods](https://www.gnu.org/licenses/gpl-violation.en.html).
