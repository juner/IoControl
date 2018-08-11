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
        private static IEnumerable<object[]> StorageGetDeviceNumberTestData => VolumePath.Concat(PhysicalDrivePath).GetIoControls(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageGetDeviceNumberTestData))]
        public void StorageGetDeviceNumberTest(IoControl IoControl) => Trace.WriteLine(IoControl.StorageGetDeviceNumber());
        [TestMethod]
        [DynamicData(nameof(StorageGetDeviceNumberTestData))]
        public void StorageGetDeviceNumberExTest(IoControl IoControl) => Trace.WriteLine(IoControl.StorageGetDeviceNumberEx());
        private static IEnumerable<object[]> StorageQueryPropertyTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v, StoragePropertyId.StorageDeviceIdProperty, StorageQueryType.StandardQuery });
        [TestMethod]
        [DynamicData(nameof(StorageQueryPropertyTestData))]
        public void StorageQueryPropertyTest(IoControl IoControl, StoragePropertyId StoragePropertyId, StorageQueryType StorageQueryType) => Trace.WriteLine(IoControl.StorageQueryProperty(StoragePropertyId, StorageQueryType));
        private static IEnumerable<object[]> StorageGetMediaSerialNumberTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageGetMediaSerialNumberTestData))]
        public void StorageGetMediaSerialNumberTest(IoControl IoControl) => Trace.WriteLine(IoControl.StorageGetMediaSerialNumber());
    }
}
