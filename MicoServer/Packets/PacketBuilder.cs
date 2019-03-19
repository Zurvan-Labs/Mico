using System;
using System.Collections.Generic;
using System.Text;

namespace MicoServer.Packets {
    public class PacketBuilder {
        public PacketType pType = (PacketType)(-1);
        public int dataSize;
        byte[] buffer = new byte[0];
        int dataRead;

        public bool Feed(byte[] data) {
            if (data.Length == 0) {
                return dataSize == dataRead - Packet.HEADER_SIZE;
            }

            int i = 0;
            if ((int)pType == -1) {
                pType = (PacketType)data[0];
                i = dataRead = 1;
            }

            if (dataRead < Packet.HEADER_SIZE) {
                int j;
                for (j = 0; dataRead < Packet.HEADER_SIZE && i+j < data.Length; j++, dataRead++) {
                    dataSize |= (data[i+j] << (8 * (dataRead - 1)));
                }

                i += j;
            }

            if (dataRead == Packet.HEADER_SIZE) {
                buffer = new byte[dataSize];
            }

            if (data.Length - i > 0) {
                for (; i < data.Length && dataRead < dataSize + Packet.HEADER_SIZE; i++, dataRead++) {
                    buffer[dataRead - Packet.HEADER_SIZE] = data[i];
                }
            }

            return dataSize == dataRead - Packet.HEADER_SIZE;
        }

        public T BuildPacket<T>() where T : Packet {
            return (T)Activator.CreateInstance(typeof(T), new object[] {
                pType,
                buffer
            });
        }
    }
}
