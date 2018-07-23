using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl;
using System.IO;

namespace IoControl.Tests
{
    [TestClass]
    public class IOControlCodeExtensionsTest
    {
        [TestMethod]
        public void CtrlCodeTest()
        {
            Assert.AreEqual(IOControlCodeExtensions.CtrlCode(FileDevice.Controller, 0x040bu, Method.Buffered, FileAccess.ReadWrite), IOControlCode.AtaPassThrough);
        }
        [TestMethod]
        public void GetDeviceTypeTest()
        {
            Assert.AreEqual(IOControlCode.AtaPassThrough.GetDeviceType(), FileDevice.Controller);
        }
        [TestMethod]
        public void GetFunctionTest()
        {
            Assert.AreEqual(IOControlCode.AtaPassThrough.GetFunction(), 0x040bu);
        }
        [TestMethod]
        public void GetMethodTest()
        {
            Assert.AreEqual(IOControlCode.AtaPassThrough.GetMethod(), Method.Buffered);
        }
        public void GetAccessTest()
        {
            Assert.AreEqual(IOControlCode.AtaPassThrough.GetAccess(), FileAccess.ReadWrite);
        }
        [TestMethod]
        public void DeconstructTest()
        {
            var (DeviceType, Function, Method, Access) = IOControlCode.AtaPassThrough;
            Assert.AreEqual(DeviceType, FileDevice.Controller);
            Assert.AreEqual(Function, 0x040bu);
            Assert.AreEqual(Method, Method.Buffered);
            Assert.AreEqual(Access, FileAccess.ReadWrite);
        }
    }
}
