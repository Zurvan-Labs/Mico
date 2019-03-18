using System;
using System.Collections.Generic;
using System.Text;

namespace MicoServer.Packets {
    public class Packet {
        public const int HEADER_SIZE = 5;

        public PacketType Type;
        public int DataSize;
        public byte[] Data;

        public Packet() {}

        /// <summary>
        /// Create a new generic packet object.
        /// </summary>
        /// <param name="type">Type of the packet.</param>
        /// <param name="data">The raw data for the packet.</param>
        public Packet(PacketType type, byte[] data) {
            Type = type;
            Data = data;
            DataSize = data.Length;
        }

        /// <summary>
        /// Deserialize a set of bytes and construct a packet object.
        /// </summary>
        /// <param name="data"></param>
        public Packet(byte[] data) {
            if (data.Length < HEADER_SIZE) {
                throw new FormatException("Data must be "+HEADER_SIZE+" bytes or more.");
            }

            Type = (PacketType)data[0];
            DataSize = data[1] | (data[2] << 8) | (data[3] << 16) | (data[4] << 24);
            Data = new byte[DataSize];
            try {
                Array.Copy(data, HEADER_SIZE, Data, 0, DataSize);
            } catch (Exception) { }
        }

        /// <summary>
        /// Serializes the packet into raw bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize() {
            byte[] data = new byte[DataSize + 5];

            data[0] = (byte)Type;
            data[1] = (byte)(DataSize & 0xF);
            data[2] = (byte)((DataSize >> 8) & 0xF);
            data[3] = (byte)((DataSize >> 16) & 0xF);
            data[4] = (byte)((DataSize >> 24) & 0xF);
            Array.Copy(Data, 0, data, HEADER_SIZE, DataSize);

            return data;
        }
    }
}
