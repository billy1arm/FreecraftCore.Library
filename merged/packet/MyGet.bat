%NUGET% restore FreecraftCore.Packet.sln -NoCache -NonInteractive -ConfigFile Nuget.config
msbuild FreecraftCore.Packet.sln /p:Configuration=Release