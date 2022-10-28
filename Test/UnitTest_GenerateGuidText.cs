using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test {
    [TestClass]
    public class UnitTest_GenerateGuidText {

        Guid GuidFull = new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff");


        [TestMethod("Mode 1: String Mode")]
        public void Test_Mode1() {
            Guid guid = Guid.NewGuid();

            string guidText = Program.GenerateGuidText(guid, 1);
            Assert.AreEqual(guid.ToString(), guidText);

            guidText = Program.GenerateGuidText(Guid.Empty, 1);
            Assert.AreEqual(Guid.Empty.ToString(), guidText);


            Assert.AreEqual(GuidFull.ToString(), Program.GenerateGuidText(GuidFull, 1));
        }

        [TestMethod("Mode 2: Array of 16 hex bytes")]
        public void Test_Mode2() {

            string guidText = Program.GenerateGuidText(Guid.Empty, 2);
            Assert.AreEqual(guidText, "new byte[]{ 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }");

            guidText = Program.GenerateGuidText(GuidFull, 2);
            Assert.AreEqual(guidText, "new byte[]{ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }");

            Guid guid = Guid.NewGuid();
            guidText = Program.GenerateGuidText(guid, 2);
            string[] guidchunks = guidText.Replace("new byte[]{ ", "").Replace("}", "").Split(',');
            byte[] guidarr = guid.ToByteArray();

            for (int i = 0;i < guidarr.Length;i++) {
                Assert.AreEqual(guidchunks[i].Replace("0x", "").Trim(' '), guidarr[i].ToString("X2"));
            }
        }

        [TestMethod("Mode 3: 16 bytes array is base 10 numbers")]
        public void Test_Mode3() {
            string guidText = Program.GenerateGuidText(Guid.Empty, 3);
            Assert.AreEqual(guidText, "new byte[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }");

            guidText = Program.GenerateGuidText(GuidFull, 3);
            Assert.AreEqual(guidText, "new byte[]{ 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 }");

            Guid guid = Guid.NewGuid();
            guidText = Program.GenerateGuidText(guid, 3);
            string[] guidchunks = guidText.Replace("new byte[]{", "").Replace("}", "").Split(',');
            byte[] guidarr = guid.ToByteArray();

            for (int i = 0;i < guidarr.Length;i++) {
                Assert.AreEqual(guidchunks[i].Trim(' '), guidarr[i].ToString());
            }
        }

        [TestMethod("Mode 4: int, short , short, byte[8]")]
        public void Test_Mode4() {
            string guidText = Program.GenerateGuidText(Guid.Empty, 4);
            Assert.AreEqual(guidText, "0, 0, 0, new byte[]{ 0, 0, 0, 0, 0, 0, 0, 0 }");

            guidText = Program.GenerateGuidText(GuidFull, 4);
            Assert.AreEqual(guidText, "-1, -1, -1, new byte[]{ 255, 255, 255, 255, 255, 255, 255, 255 }");

            Guid guid = Guid.NewGuid();

            guidText = Program.GenerateGuidText(guid, 4);

            string[] guidchunks = guidText.Replace("new byte[]{", "").Replace("}", "").Split(',');
            byte[] guidarr = guid.ToByteArray();
            int part_int = BitConverter.ToInt32(guidarr, 0);
            short part_short1 = BitConverter.ToInt16(guidarr, 4);
            short part_short2 = BitConverter.ToInt16(guidarr, 6);

            Assert.AreEqual(guidchunks[0].Trim(' '), part_int.ToString(), "Int part is not equal");
            Assert.AreEqual(guidchunks[1].Trim(' '), part_short1.ToString(), "Short part 1 is not equal");
            Assert.AreEqual(guidchunks[2].Trim(' '), part_short2.ToString(), "Short part 2 is not equal");

            for (int i = 3;i < guidchunks.Length;i++) {
                Assert.AreEqual(guidchunks[i].Trim(' '), guidarr[i + 5].ToString(), "byte " + i + " is not equal");
            }
        }

        [TestMethod("Mode 5: int, short, short, byte, byte, byte, byte, byte, byte, byte, byte")]
        public void Test_Mode5() {
            string guidText = Program.GenerateGuidText(Guid.Empty, 5);
            Assert.AreEqual(guidText, "0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0");

            guidText = Program.GenerateGuidText(GuidFull, 5);
            Assert.AreEqual(guidText, "-1, -1, -1, 255, 255, 255, 255, 255, 255, 255, 255");

            Guid guid = Guid.NewGuid();

            guidText = Program.GenerateGuidText(guid, 5);

            string[] guidchunks = guidText.Replace("new byte[]{", "").Replace("}", "").Split(',');
            byte[] guidarr = guid.ToByteArray();
            int part_int = BitConverter.ToInt32(guidarr, 0);
            short part_short1 = BitConverter.ToInt16(guidarr, 4);
            short part_short2 = BitConverter.ToInt16(guidarr, 6);

            Assert.AreEqual(guidchunks[0].Trim(' '), part_int.ToString(), "Int part is not equal");
            Assert.AreEqual(guidchunks[1].Trim(' '), part_short1.ToString(), "Short part 1 is not equal");
            Assert.AreEqual(guidchunks[2].Trim(' '), part_short2.ToString(), "Short part 2 is not equal");

            for (int i = 3;i < guidchunks.Length;i++) {
                Assert.AreEqual(guidchunks[i].Trim(' '), guidarr[i + 5].ToString(), "byte " + i + " is not equal");
            }
        }

        [TestMethod("Mode 6: uint, ushort, ushort, byte, byte, byte, byte, byte, byte, byte, byte")]
        public void Test_Mode6() {
            string guidText = Program.GenerateGuidText(Guid.Empty, 6);
            Assert.AreEqual(guidText, "0x00000000, 0x0000, 0x0000, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00");

            guidText = Program.GenerateGuidText(GuidFull, 6);
            Assert.AreEqual(guidText, "0xFFFFFFFF, 0xFFFF, 0xFFFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF");

            Guid guid = Guid.NewGuid();

            guidText = Program.GenerateGuidText(guid, 6);

            string[] guidchunks = guidText.Replace("new byte[]{", "").Replace("}", "").Split(',');
            byte[] guidarr = guid.ToByteArray();
            uint part_int = BitConverter.ToUInt32(guidarr, 0);
            ushort part_short1 = BitConverter.ToUInt16(guidarr, 4);
            ushort part_short2 = BitConverter.ToUInt16(guidarr, 6);

            Assert.AreEqual(guidchunks[0].Replace("0x","").Trim(' '), part_int.ToString("X8"), "Int part is not equal");
            Assert.AreEqual(guidchunks[1].Replace("0x","").Trim(' '), part_short1.ToString("X4"), "Short part 1 is not equal");
            Assert.AreEqual(guidchunks[2].Replace("0x","").Trim(' '), part_short2.ToString("X4"), "Short part 2 is not equal");

            for (int i = 3;i < guidchunks.Length;i++) {
                Assert.AreEqual(guidchunks[i].Replace("0x","").Trim(' '), guidarr[i + 5].ToString("X2"), "byte " + i + " is not equal");
            }
        }
    }
}
