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
        private static IEnumerable<object[]> DiskGetPartitionInfoTestData => PhysicalDrivePathGenerator().Concat(LogicalDrivePathGenerator()).Concat(VolumePathGenerator()).GetIoControls(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetPartitionInfoTestData))]
        public void DiskGetPartitionInfoTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetPartitionInfo());
        private static IEnumerable<object[]> DiskGetPartitionInfoExTestData => PhysicalDrivePathGenerator().Concat(LogicalDrivePathGenerator()).Concat(VolumePathGenerator()).GetIoControls(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetPartitionInfoExTestData))]
        public void DiskGetPartitionInfoExTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetPartitionInfoEx());
        private static IEnumerable<object[]> DiskGetDriveLayoutTestData => GetPhysicalDrives(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open)
            .Where(v => {
                try
                {
                    // Mbr only 
                    return v.DiskGetDriveGeometryEx2().PartitionInfo.PartitionStyle == PartitionStyle.Mbr;
                } catch {
                    return false;
                }
            })
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveLayoutTestData))]
        public void DiskGetDriveLayoutTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveLayout());
        private static IEnumerable<object[]> DiskGetDriveLayoutExTestData => PhysicalDrivePathGenerator().Concat(VolumePathGenerator()).GetIoControls(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveLayoutExTestData))]
        public void DiskGetDriveLayoutExTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveLayoutEx());
        private static IEnumerable<object[]> DiskGetLengthInfoTestData => GetPhysicalDrives(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetLengthInfoTestData))]
        public void DiskGetLengthInfoTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetLengthInfo());
        private static IEnumerable<object[]> DiskGetDriveGeometryTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryTestData))]
        public void DiskGetDriveGeometryTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveGeometry());
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryTestData))]
        public void DiskGetDriveGeometryExTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveGeometryEx());
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryTestData))]
        public void DiskGetDriveGeometryEx2Test(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveGeometryEx2());
        private static IEnumerable<object[]> DiskPerformanceTestData => GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskPerformanceTestData))]
        public void DiskPerformanceTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskPerformance());
    }

}
