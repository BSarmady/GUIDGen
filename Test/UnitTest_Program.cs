using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Test {
    [TestClass]
    public class UnitTest_Program {

        [TestMethod]
        public void Test_Program() {
            // Dummy test for 100% Coverage, LOL!
            try {
                Program.Main(new string[] { });
                Guid guid = Guid.Parse(Clipboard.GetText());
            } catch {
                Assert.Fail();
            }
        }
    }
}
