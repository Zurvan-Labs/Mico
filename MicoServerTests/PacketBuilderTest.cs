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
    }
}
