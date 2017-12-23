# FreecraftCore.Library

FreecraftCore is an open-source C#-based 3.3.5 and 1.12.1 World of Warcraft emulation project. It is derived from the work of Mangos and Trinitycore.

This is a repository of shared .NET/C# libraries between client/server emulation for old World of Warcraft versions. All the old FreecraftCore repositories. All previous repositories, other than the [FreecraftCore.Serializer](https://github.com/FreecraftCore/FreecraftCore.Serializer) repository, is considered deprecated.

## How?

FreecraftCore is built on top of freely available reverse engineering information from the [Trinitycore](http://github.com/trinityCore) project and the [Mangos](https://github.com/Mangos) project. It is a groundup reimplementation of the specifications and protocols outlined in those projects. The focus of FreecraftCore when compared to those projects is the emphasis on shared libraries between client/server for the ultimate creation of an emulated and fully Trinitycore/Mangos compatible client.

**Serialization** in FreecraftCore takes on a next-generation approach partially outlined by Blizzard themselves, and implemented by mainstream serialization libraries in .NET such as [Protobuf-Net](https://github.com/mgravell/protobuf-net) and [JSON.NET](https://github.com/JamesNK/Newtonsoft.Json). The [FreecraftCore.Serializer](https://github.com/FreecraftCore/FreecraftCore.Serializer) leverages metadata/attributes/annotations for controlling serialization behavior. To learn or read more about this implementation see the [FreecraftCore.Serializer](https://github.com/FreecraftCore/FreecraftCore.Serializer) repository. 

**Networking** for emulation has often been a N:N problem where you need to create *N* networking clients/libraries for *N* many projects. This is a significant problem for productivity in emulation. To solve these significant problems, and to take advantage of these problems for the benefit of FreecraftCore, a library called [GladNet3](https://github.com/HelloKitty/GladNet3) is utilized. This library offers a pipeline API for controling the send and recieve of network messages and abstracting the transportation mechanism as well as providing a simple message handler API. This helps consolidate several projects under one universal networking client/library and API.

TODO: Fill in rest of readme
