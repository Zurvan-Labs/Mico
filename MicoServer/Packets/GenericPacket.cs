using System;
using System.Collections.Generic;
using System.Text;

namespace MicoServer.Packets {
    public class GenericPacket : Packet {
        public GenericPacket(PacketType type, byte[] data) : base(type, data) {
        }
    }
}
