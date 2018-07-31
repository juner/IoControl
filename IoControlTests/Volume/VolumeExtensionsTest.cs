using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static IoControl.IoControlTestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.Utils.DeviceUtils;

namespace IoControl.Volume.Tests
{
    [TestClass]
    public class VolumeExtensionsTest
    {
        private static IEnumerable<object[]> VolumeGetVolumeDiskExtentsTestData => VolumePathGenerator().GetIoControls(CreationDisposition: System.IO.FileMode.Open)
            .Where(v => MassStorage.MassStorageExtensions.StorageGetDeviceNumber(v).DeviceType == FileDevice.Disk)
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(VolumeGetVolumeDiskExtentsTestData))]
        public void VolumeGetVolumeDiskExtentsTest(IoControl IoControl) => Trace.WriteLine(IoControl.VolumeGetVolumeDiskExtents());
        private static IEnumerable<object[]> VolumeIsOfflineTestData => VolumeGetVolumeDiskExtentsTestData;
        [TestMethod]
        [DynamicData(nameof(VolumeIsOfflineTestData))]
        public void VolumeIsOfflineTest(IoControl IoControl) => Trace.WriteLine(IoControl.VolumeIsOffline());
        private static IEnumerable<object[]> VolumeIsIoCapaleTestData => VolumeGetVolumeDiskExtentsTestData;
        [TestMethod]
        [DynamicData(nameof(VolumeIsIoCapaleTestData))]
        public void VolumeIsIoCapaleTest(IoControl IoControl) => Trace.WriteLine(IoControl.VolumeIsIoCapale());
        private static IEnumerable<object[]> VolumeIsPartitionTestData => VolumeGetVolumeDiskExtentsTestData;
        [TestMethod]
        [DynamicData(nameof(VolumeIsPartitionTestData))]
        public void VolumeIsPartitionTest(IoControl IoControl) => Trace.WriteLine(IoControl.VolumeIsPartition());
        private static IEnumerable<object[]> VolumeQueryVolumeNumberTestData => VolumeGetVolumeDiskExtentsTestData;
        [TestMethod]
        [DynamicData(nameof(VolumeQueryVolumeNumberTestData))]
        public void VolumeQueryVolumeNumberTest(IoControl IoControl) => Trace.WriteLine(IoControl.VolumeQueryVolumeNumber());
        private static IEnumerable<object[]> VolumeIsClusteredTestData => VolumeGetVolumeDiskExtentsTestData;
        [TestMethod]
        [DynamicData(nameof(VolumeIsClusteredTestData))]
        public void VolumeIsClusteredTest(IoControl IoControl) => Trace.WriteLine(IoControl.VolumeIsClustered());
        private static IEnumerable<object[]> VolumeLogicalToPhysicalTestData => VolumeGetVolumeDiskExtentsTestData;
        [TestMethod]
        [DynamicData(nameof(VolumeLogicalToPhysicalTestData))]
        public void VolumeLogicalToPhysicalTest(IoControl IoControl) => Trace.WriteLine(IoControl.VolumeLogicalToPhysical());
        private static IEnumerable<object[]> VolumePhysicalToLogicalTestData => VolumeGetVolumeDiskExtentsTestData;
        [TestMethod]
        [DynamicData(nameof(VolumePhysicalToLogicalTestData))]
        public void VolumePhysicalToLogicalTest(IoControl IoControl) => Trace.WriteLine(IoControl.VolumePhysicalToLogical(IoControl.VolumeLogicalToPhysical().First()));
    }
}
