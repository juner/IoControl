using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.Utils.DeviceUtils;

namespace IoControl.Utils.Tests
{
    [TestClass]
    public class DeviceUtilsTest
    {
        private static IEnumerable<object[]> QueryDocDeviceTestData {
            get {
                yield return new object[] { null };
                foreach (var DriveName in GetLogicalDriveStrings().Select(v => v.TrimEnd('\\')))
                    yield return new object[] { DriveName };
                foreach (var VolumeGUIDString in GetVolumePathNames().Select(v => v.Substring(@"\\.\".Length).TrimEnd('\\')))
                    yield return new object[] { VolumeGUIDString };

            }
        }
        [TestMethod]
        [DynamicData(nameof(QueryDocDeviceTestData))]
        public void QueryDocDeviceTest(string DeviceName)
        {
            foreach (var Device in QueryDocDevice(DeviceName))
                Trace.WriteLine(Device);
        }
        [TestMethod]
        public void GetVolumePathNamesTest()
        {
            foreach (var volumeName in GetVolumePathNames())
                Trace.WriteLine(volumeName);
        }
        private static IEnumerable<object[]> GetVolumeMountPointTestData => GetLogicalDriveStrings().Select(v => new object[] { $@"\\.\{v}".Trim('\\Codeqqqqqqqqqqqqqqqqcccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc        １１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１１') });//GetVolumePathNames().Select(v => new object[] { v.Replace(@"\\?\",@"\\.\").TrimEnd('\\') });
        [TestMethod]
        [DynamicData(nameof(GetVolumeMountPointTestData))]
        public void GetVolumeMountPointTest(string RootPathName) {
            foreach (var volumeMountPoint in GetVolumeMountPoint(RootPathName))
                Trace.WriteLine(volumeMountPoint);
        }
        private static IEnumerable<object[]> GetVolumePathNamesForVolumeNameTestData => GetVolumePathNames().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(GetVolumePathNamesForVolumeNameTestData))]
        public void GetVolumePathNamesForVolumeNameTest(string VolumePathName)
        {
            foreach (var VolumeName in GetVolumePathNamesForVolumeName(VolumePathName))
                Trace.WriteLine(VolumeName);
;       }
        [TestMethod]
        public void GetLogicalDrivesTest()
        {
            var drives = GetLogicalDrives();
            foreach (var (index, bit) in Enumerable.Range(0, 32).Select(index => (index, bit: (drives >> index) == 0x1)))
                Trace.WriteLine($"{nameof(index)}: {index}{('A'+index <= 'Z' ? $" ({(char)('A' + index)})" : "")} :{bit}");
        }
        [TestMethod]
        public void GetLogicalDriveStringsTest()
        {
            foreach (var DriveString in GetLogicalDriveStrings())
                Trace.WriteLine(DriveString);
        }
        private static IEnumerable<object[]> GetVolumeNameForVolumeMountPointTestData => GetLogicalDriveStrings().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(GetVolumeNameForVolumeMountPointTestData))]
        public void GetVolumeNameForVolumeMountPointTest(string VolumeMountPoint) => Trace.WriteLine(GetVolumeNameForVolumeMountPoint(VolumeMountPoint));
    }
}
