using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.IoControlTestUtils;
using System.Diagnostics;
using IoControl.Disk;

namespace IoControl.Disk.Tests
{
    [TestClass]
    public class DiskExtensionsTest
    {
        private static IEnumerable<object[]> DiskGetCacheInformationTestData => GetPhysicalDrives(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetCacheInformationTestData))]
        public void DiskGetCacheInformationTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetCacheInformation());
        private static IEnumerable<object[]> DiskGetDriveLayoutExTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveLayoutExTestData))]
        public void DiskGetDriveLayoutExTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveLayoutEx());
        private static IEnumerable<object[]> DiskGetLengthInfoTestData => GetPhysicalDrives(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetLengthInfoTestData))]
        public void DiskGetLengthInfoTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetLengthInfo());
        private static IEnumerable<object[]> DiskGetDriveGeometryExTestData => GetPhysicalDrives(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryExTestData))]
        public void DiskGetDriveGeometryExTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveGeometryEx());
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryExTestData))]
        public void DiskGetDriveGeometryEx2Test(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveGeometryEx2());
        private static IEnumerable<object[]> DiskPerformanceTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskPerformanceTestData))]
        public void DiskPerformanceTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskPerformance());
    }

}
