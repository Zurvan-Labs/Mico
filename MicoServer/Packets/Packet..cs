using System;
using System.Collections.Generic;
using System.Text;

namespace MicoServer.Packets {
    public class Packet {
        public PacketType Type;
        public int DataSize;
        public byte[] Data;

        public Packet(byte[] data) {
            if (data.Length < 5) { // byte + int = 5 bytes
                throw new FormatException("Data must be 5 bytes or more.");
            }

            Type = (PacketType)data[0];
            DataSize = data[1] | (data[2] << 8) | (data[3] << 16) | (data[4] << 24);
            Data = new byte[DataSize];
            try {
                Array.Copy(data, 5, Data, 0, DataSize);
            } catch (Exception) { }
        }
    }
}
