using System;
using System.Collections.Generic;
using System.Text;

namespace MicoServer.Packets {
    public class PacketBuilder {
        public PacketType pType = (PacketType)(-1);
        public int dataSize = 0;
        byte[] buffer = new byte[0];
        int dataRead = 0;

        public bool Feed(byte[] data) {
            if (data.Length == 0) {
                return dataSize == dataRead - Packet.DATA_START;
            }

            int i = 0;
            if ((int)pType == -1) {
                pType = (PacketType)data[0];
                i = dataRead = 1;
            }

            if (dataRead < Packet.DATA_START) {
                int j;
                for (j = 0; dataRead + i < Packet.DATA_START && i+j < data.Length; j++, dataRead++) {
                    dataSize |= (data[i+j] << (8 * (dataRead - 1)));
                }

                i += j;
            }

            if (dataRead == Packet.DATA_START) {
                buffer = new byte[dataSize];
            }

            if (data.Length - i > 0) {
                for (; i < data.Length && dataRead < dataSize + Packet.DATA_START; i++, dataRead++) {
                    buffer[dataRead - Packet.DATA_START] = data[i];
                }
            }

            return dataSize == dataRead - Packet.DATA_START;
        }

        public T BuildPacket<T>() where T : Packet {
            throw new NotImplementedException();
        }
    }
}
