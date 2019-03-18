using MicoServer.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicoServerTests {
    [TestClass]
    public class GenericPacketTest {
        [TestMethod]
        [TestCategory("Deserializing")]
        [DataRow(PacketType.Generic, new byte[] { 0, 0, 0, 0, 0 })]
        [DataRow((PacketType)1, new byte[] { 1, 0, 0, 0, 0 })]
        [DataRow((PacketType)128, new byte[] { 128, 0, 0, 0, 0 })]
        [DataRow((PacketType)255, new byte[] { 255, 0, 0, 0, 0 })]
        public void Test_Packet_Type_Parsing(PacketType type, byte[] data) {
            Packet packet = new Packet(data);
            Assert.AreEqual(type, packet.Type);
        }

        [TestMethod]
        [TestCategory("Deserializing")]
        [ExpectedException(typeof(FormatException))]
        [DataRow(new byte[0])]
        [DataRow(new byte[] { 0, 0 })]
        [DataRow(new byte[] { 1, 2, 3, 4 })]
        public void Test_Invalid_Packet_Length(byte[] data) {
            Packet packet = new Packet(data);
        }

        [TestMethod]
        [TestCategory("Deserializing")]
        [DataRow(0, new byte[] { 0, 0, 0, 0, 0 })]
        [DataRow(1, new byte[] { 0, 1, 0, 0, 0, 0 })]
        [DataRow(256, new byte[] { 0, 0, 1, 0, 0 })]
        [DataRow(65536, new byte[] { 0, 0, 0, 1, 0 })]
        [DataRow(16777216, new byte[] { 0, 0, 0, 0, 1 })]
        [DataRow(16843009, new byte[] { 0, 1, 1, 1, 1, 0 })]
        public void Test_Correct_Data_Length(int len, byte[] data) {
            Packet packet = new Packet(data);
            Assert.AreEqual(len, packet.DataSize);
        }

        [TestMethod]
        [TestCategory("Deserializing")]
        [DataRow(new byte[] { 0, 0, 0, 0, 0 }, new byte[0])]
        [DataRow(new byte[] { 1, 5, 0, 0, 0, 1, 2, 3, 4, 5 }, new byte[] { 1, 2, 3, 4, 5 })]
        public void Test_Correct_Copy_Of_Data(byte[] raw, byte[] data) {
            Packet packet = new Packet(raw);
            CollectionAssert.AreEqual(data, packet.Data);
        }
    }
}
