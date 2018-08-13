using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.IoControlTestUtils;

namespace IoControl.MassStorage.Tests
{
    [TestClass]
    public class MassStorageExtensionsTest
    {
        private static IEnumerable<string> Generator = VolumePath.Concat(PhysicalDrivePath).ToArray();
        private static IEnumerable<object[]> StorageCheckVerifyTestData => Generator.GetIoControls(FileAccess: System.IO.FileAccess.Read, FileShare: System.IO.FileShare.ReadWrite, CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageCheckVerifyTestData))]
        public void StorageCheckVerifyTest(IoControl IoControl) => Trace.WriteLine(IoControl.StorageCheckVerify());
        private static IEnumerable<object[]> StorageCheckVerify2TestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageCheckVerify2TestData))]
        public void StorageCheckVerify2Test(IoControl IoControl) => Trace.WriteLine(IoControl.StorageCheckVerify2());
        private static IEnumerable<object[]> StorageGetDeviceNumberTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageGetDeviceNumberTestData))]
        public void StorageGetDeviceNumberTest(IoControl IoControl) => Trace.WriteLine(IoControl.StorageGetDeviceNumber());
        [TestMethod]
        [DynamicData(nameof(StorageGetDeviceNumberTestData))]
        public void StorageGetDeviceNumberExTest(IoControl IoControl) => Trace.WriteLine(IoControl.StorageGetDeviceNumberEx());
        private static IEnumerable<object[]> StorageQueryPropertyTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v, StoragePropertyId.StorageDeviceIdProperty, StorageQueryType.StandardQuery });
        [TestMethod]
        [DynamicData(nameof(StorageQueryPropertyTestData))]
        public void StorageQueryPropertyTest(IoControl IoControl, StoragePropertyId StoragePropertyId, StorageQueryType StorageQueryType) => Trace.WriteLine(IoControl.StorageQueryProperty(StoragePropertyId, StorageQueryType));
        private static IEnumerable<object[]> StorageGetMediaSerialNumberTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageGetMediaSerialNumberTestData))]
        public void StorageGetMediaSerialNumberTest(IoControl IoControl) => Trace.WriteLine(IoControl.StorageGetMediaSerialNumber());
        private static IEnumerable<object[]> StorageGetMediaTypesTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageGetMediaTypesTestData))]
        public void StorageGetMediaTypesTest(IoControl IoControl) {
            foreach (var MediaType in IoControl.StorageGetMediaTypes())
                Trace.WriteLine(MediaType);
        }
        [TestMethod]
        [DynamicData(nameof(StorageGetMediaTypesTestData))]
        public void StorageGetMediaTypesExTest(IoControl IoControl) => Trace.WriteLine(IoControl.StorageGetMediaTypesEx());
    }
}
