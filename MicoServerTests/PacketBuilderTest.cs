using MicoServer.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicoServerTests {
    [TestClass]
    public class PacketBuilderTest {
        [TestMethod]
        public void Test_Feed_Returns_Correct_Value() {
            PacketBuilder builder = new PacketBuilder();
            
            bool stage1 = builder.Feed(new byte[] { 0 });
            bool stage2 = builder.Feed(new byte[] { 1, 0 });
            bool stage3 = builder.Feed(new byte[] { 0, 0 });
            bool stage4 = builder.Feed(new byte[] { 42 });
            bool stage5 = builder.Feed(new byte[] { 1, 2, 3, 4, 5 } );

            Assert.AreEqual(stage1, false);
            Assert.AreEqual(stage2, false);
            Assert.AreEqual(stage3, false);
            Assert.AreEqual(stage4, true);
            Assert.AreEqual(stage5, true);
        }

        [TestMethod]
        public void Test_Feed_Returns_Correct_Value_From_Whole_Array() {
            PacketBuilder builder = new PacketBuilder();

            bool result = builder.Feed(new byte[] { 0, 1, 0, 0, 0, 42 });

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Test_Returns_Correct_Parsed_Generic_Packet_type() {
            PacketBuilder builder = new PacketBuilder();

            builder.Feed(new byte[] { 0, 1, 0, 0, 0, 42  });
            GenericPacket packet = builder.BuildPacket<GenericPacket>();

            Assert.AreEqual(PacketType.Generic, packet.Type);
            Assert.AreEqual(1, packet.DataSize);
            CollectionAssert.AreEqual(new byte[] { 42 }, packet.Data);
        }
    }
}
