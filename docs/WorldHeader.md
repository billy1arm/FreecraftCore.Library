## Packet Header for Incoming Client Messages

[a][b...][u1...]

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
    
###u1

A variable length chunk of unknown use.
  1. Small packet header
  
    chunk-length: 2
    
  2. Large packet header
  
    chunk-length: 3
