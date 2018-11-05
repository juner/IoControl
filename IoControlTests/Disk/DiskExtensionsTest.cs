using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.IoControlTestUtils;
using System.Diagnostics;
using IoControl.Disk;
using System.Threading;
using IoControl.Tests;
using IoControl.MassStorage;
using System.Runtime.InteropServices;

namespace IoControl.Disk.Tests
{
    [TestClass]
    public class DiskExtensionsTest
    {
        private static IEnumerable<string> Generator => LogicalDrivePath.Concat(PhysicalDrivePath).Concat(HarddiskVolumePath).Concat(HardidiskPartitionPath);
        private static IEnumerable<object[]> DiskAreVolumesReadyAsyncTestData => Generator
            .GetIoControls(FileAccess: System.IO.FileAccess.Read, FileShare: System.IO.FileShare.ReadWrite, CreationDisposition: System.IO.FileMode.Open, FlagAndAttributes: FileFlagAndAttributesExtensions.Create(FileFlags.Overlapped)).Using()
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskAreVolumesReadyAsyncTestData))]
        public async Task DiskAreVolumesReadyAsyncTest(IoControl IoControl) {
            using (var source = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
                Trace.WriteLine(await IoControl.DiskAreVolumesReadyAsync(source.Token));
        }
        private static IEnumerable<object[]> DiskGetCacheInformationTestData => Generator.GetIoControls(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Using()
            .Where(v => v.StorageCheckVerify2())
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetCacheInformationTestData))]
        public void DiskGetCacheInformationTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetCacheInformation());
        private static IEnumerable<object[]> DiskGetPartitionInfoTestData => Generator.GetIoControls(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetPartitionInfoTestData))]
        public void DiskGetPartitionInfoTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetPartitionInfo());
        private static IEnumerable<object[]> DiskGetPartitionInfoExTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetPartitionInfoExTestData))]
        public void DiskGetPartitionInfoExTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetPartitionInfoEx());
        private static IEnumerable<object[]> DiskGetDriveLayoutTestData => Generator.GetIoControls(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Using()
            .Where(v => v.StorageCheckVerify2())
            .Where(v => v.DiskGetDriveGeometryEx(out _, out _ ,out var Partition, out _ , out _) && Partition.PartitionStyle == PartitionStyle.Gpt)
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveLayoutTestData))]
        public void DiskGetDriveLayoutTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveLayout());
        private static IEnumerable<object[]> DiskGetDriveLayoutExTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveLayoutExTestData))]
        public void DiskGetDriveLayoutExTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveLayoutEx());
        private static IEnumerable<object[]> DiskGetLengthInfoTestData => Generator.GetIoControls(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open).Using()
            .Where(v => v.StorageCheckVerify2())
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetLengthInfoTestData))]
        public void DiskGetLengthInfoTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetLengthInfo());
        private static IEnumerable<object[]> DiskGetDriveGeometryTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using()
            .Where(v => v.StorageCheckVerify2())
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryTestData))]
        public void DiskGetDriveGeometryTest(IoControl IoControl)
        {
            if (IoControl.DiskGetDriveGeometry() is DiskGeometry DiskGeometry)
                Trace.WriteLine(DiskGeometry);
            else
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        private static IEnumerable<object[]> DiskGetDriveGeometryAsyncTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open, FlagAndAttributes: FileFlagAndAttributesExtensions.Create(FileFlags.Overlapped)).Using()
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryAsyncTestData))]
        public async Task DiskGetDriveGeometryAsyncTest(IoControl IoControl)
        {
            using (var Source = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
                if (await IoControl.DiskGetDriveGeometryAsync(Source.Token) is DiskGeometry DiskGeometry)
                    Trace.WriteLine(DiskGeometry);
                else
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryTestData))]
        public void DiskGetDriveGeometryExTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveGeometryEx());
        [TestMethod]
        [DynamicData(nameof(DiskGetDriveGeometryTestData))]
        public void DiskGetDriveGeometryEx2Test(IoControl IoControl) => Trace.WriteLine(IoControl.DiskGetDriveGeometryEx2());
        private static IEnumerable<object[]> DiskPerformanceTestData => Generator.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using()
            .Where(v => v.StorageCheckVerify2())
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(DiskPerformanceTestData))]
        public void DiskPerformanceTest(IoControl IoControl) => Trace.WriteLine(IoControl.DiskPerformance());
        private static IEnumerable<object> SmartGetVersionTestData => PhysicalDrivePath.GetIoControls(FileAccess: System.IO.FileAccess.ReadWrite, FileShare: System.IO.FileShare.ReadWrite, CreationDisposition: System.IO.FileMode.Open).Using()
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(SmartGetVersionTestData))]
        public void SmartGetVersionTest(IoControl IoControl)
        {
            var result = IoControl.SmartGetVersion(out var Version);
            Trace.WriteLine($"{nameof(result)}:{result}");
            Trace.WriteLine(Version);
        }
        [TestMethod]
        [DynamicData(nameof(SmartGetVersionTestData))]
        public void SmartRcvDriveDataIdentifyDeviceTest(IoControl IoControl)
        {
            Trace.WriteLine(IoControl.SmartRcvDriveDataIdentifyDevice(0xA0));
        }
    }

}
