using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl;
using System.IO;

namespace IoControlTests
{
    [TestClass]
    public class IOControlCodeExtensionsTest
    {
        [TestMethod]
        public void DeconstructTest()
        {
            var (DeviceType, Function, Method, Access) = IOControlCode.AtaPassThrough;
            Assert.AreEqual(DeviceType, FileDevice.Controller);
            Assert.AreEqual(Function, (uint)0x040b);
            Assert.AreEqual(Method, Method.Buffered);
            Assert.AreEqual(Access, FileAccess.ReadWrite);
        }
    }
}
