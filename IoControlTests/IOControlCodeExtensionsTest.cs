using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace IoControl.Tests
{
    [TestClass]
    public class IOControlCodeExtensionsTest
    {
        private static IEnumerable<object[]> CtrlCodeTestData {
            get {
                yield return new object[] { IOControlCode.AtaPassThrough,  FileDevice.Controller, 0x040bu, Method.Buffered, FileAccess.ReadWrite };
            }
        }
        [TestMethod]
        [DynamicData(nameof(CtrlCodeTestData))]
        public void CtrlCodeTest(IOControlCode CtrlCode, FileDevice DeviceType, uint Function, Method Method, FileAccess Access)
            => Assert.AreEqual(CtrlCode, IOControlCodeExtensions.CtrlCode(DeviceType, Function, Method, Access));
        private static IEnumerable<object[]> GetDeviceTypeTestData => CtrlCodeTestData.Select(v => new object[] { v[0], v[1] });
        [TestMethod]
        [DynamicData(nameof(GetDeviceTypeTestData))]
        public void GetDeviceTypeTest(IOControlCode CtrlCode, FileDevice DeviceType)
            => Assert.AreEqual(DeviceType, CtrlCode.GetDeviceType());
        private static IEnumerable<object[]> GetFunctionTestData => CtrlCodeTestData.Select(v => new object[] { v[0], v[2] });
        [TestMethod]
        [DynamicData(nameof(GetFunctionTestData))]
        public void GetFunctionTest(IOControlCode CtrlCode, uint Function)
            => Assert.AreEqual(Function, CtrlCode.GetFunction());
        private static IEnumerable<object[]> GetMethodTestData => CtrlCodeTestData.Select(v => new object[] { v[0], v[3] });
        [TestMethod]
        [DynamicData(nameof(GetMethodTestData))]
        public void GetMethodTest(IOControlCode CtrlCode, Method Method)
            => Assert.AreEqual(Method, CtrlCode.GetMethod());
        private static IEnumerable<object[]> GetAccessTestData => CtrlCodeTestData.Select(v => new object[] { v[0], v[4] });
        [TestMethod]
        [DynamicData(nameof(GetAccessTestData))]
        public void GetAccessTest(IOControlCode CtrlCode, FileAccess Access)
            => Assert.AreEqual(Access, CtrlCode.GetAccess());
        
        [TestMethod]
        [DynamicData(nameof(CtrlCodeTestData))]
        public void DeconstructTest(IOControlCode CtrlCode, FileDevice DeviceType, uint Function, Method Method, FileAccess Access)
        {
            var (_DeviceType, _Function, _Method, _Access) = CtrlCode;
            Assert.AreEqual(DeviceType, _DeviceType);
            Assert.AreEqual(Function, _Function);
            Assert.AreEqual(Method, _Method);
            Assert.AreEqual(Access, _Access);
        }
    }
}
