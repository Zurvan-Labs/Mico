using System;
using System.Collections.Generic;
using System.Text;

namespace MicoServer.Packets {
    public class Packet {
        public PacketType Type;
        public int DataSize;
        public byte[] Data;

        public Packet() {}

        public Packet(PacketType type, byte[] data) {
            Type = type;
            Data = data;
            DataSize = data.Length;
        }

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

        public byte[] Serialize() {
            byte[] data = new byte[DataSize + 5];

            data[0] = (byte)Type;
            data[1] = (byte)(DataSize & 0xF);
            data[2] = (byte)((DataSize >> 8) & 0xF);
            data[3] = (byte)((DataSize >> 16) & 0xF);
            data[4] = (byte)((DataSize >> 24) & 0xF);
            Array.Copy(Data, 0, data, 5, DataSize);

            return data;
        }
    }
}
