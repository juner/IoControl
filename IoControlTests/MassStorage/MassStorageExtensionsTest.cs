using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.IoControlTestUtils;
using System.Diagnostics;
using IoControl.MassStorage;

namespace IoControl.MassStorage.Tests
{
    [TestClass]
    public class MassStorageExtensionsTest
    {
        private static IEnumerable<object[]> StorageGetDeviceNumberTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageGetDeviceNumberTestData))]
        public void StorageGetDeviceNumberTest(IoControl IoControl)
            => Trace.WriteLine($"{nameof(MassStorageExtensions.StorageGetDeviceNumber)}: {IoControl.StorageGetDeviceNumber()}");
        private static IEnumerable<object[]> StorageQueryPropertyTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v, StoragePropertyId.StorageDeviceIdProperty, StorageQueryType.StandardQuery });
        [TestMethod]
        [DynamicData(nameof(StorageQueryPropertyTestData))]
        public void StorageQueryPropertyTest(IoControl IoControl, StoragePropertyId StoragePropertyId, StorageQueryType StorageQueryType)
            => Trace.WriteLine($"{nameof(MassStorageExtensions.StorageQueryProperty)}: {IoControl.StorageQueryProperty(StoragePropertyId, StorageQueryType)}");
        private static IEnumerable<object[]> StorageGetMediaSerialNumberTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(StorageGetMediaSerialNumberTestData))]
        public void StorageGetMediaSerialNumberTest(IoControl IoControl)
            => Trace.WriteLine($"{nameof(MassStorageExtensions.StorageGetMediaSerialNumber)}: {IoControl.StorageGetMediaSerialNumber()}");
    }
}
