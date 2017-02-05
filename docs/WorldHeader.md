## Packet Header for Incoming Client Messages

Between 4 and 5 bytes.

[a][b...][cc]

###a
A one byte field that represents the packet type and contains part of the size data. There are two types.
  1. Small packet header
  2. Large packet header
  
**Small packet header**: Small packet header is the default type of packet header. This header encodes the size of the payload in the first 2 bytes of the incoming stream.


**Large packet header**: Large packet header is an optional type of packet header. This type can be deduced by **bitwise &** with 0x80. 
This header encodes the size of the payload in the first 3 bytes.

###b

A variable length chunk that is used to compute the packet size in combination with the first byte **a**.
  1. Small packet header
  
    chunk-length: 1
    
    encoding: a << 8 | b[0]
    
  2. Large packet header
  
    chunk-length: 2
    
    encoding: ((a & 0x7F) << 16) | (b[0] << 8) | b[1]
    
###cc

Two byte (ushort) chunk that represents the [NetworkOperationCode](https://github.com/FreecraftCore/FreecraftCore.Packet/blob/master/src/FreecraftCore.Packet.Common/OpCodes/NetworkOperationCode.cs). Starts after the **b** chunk.
