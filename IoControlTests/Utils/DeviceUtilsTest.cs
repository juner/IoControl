using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
                foreach (var VolumeGUIDString in FindVolumes().Select(v => v.Substring(@"\\.\".Length).TrimEnd('\\')))
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
        public void FindVolumesTest()
        {
            foreach (var volumeName in FindVolumes())
                Trace.WriteLine(volumeName);
        }
        private static IEnumerable<object[]> FindVolumeMountPointsTestData
            => GetLogicalDriveStrings().Select(v => $@"\\.\{v}")
            .Concat(FindVolumes().Select(v => v.Replace(@"\\?\", @"\\.\").TrimEnd('\\')))
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(FindVolumeMountPointsTestData))]
        public void FindVolumeMountPointsTest(string RootPathName) {
            foreach (var volumeMountPoint in FindVolumeMountPoints(RootPathName))
                Trace.WriteLine(volumeMountPoint);
        }
        private static IEnumerable<object[]> GetVolumeInformationTestData
            => GetLogicalDriveStrings().Select(v => $@"{v}\")
            .Concat(FindVolumes())
            .Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(GetVolumeInformationTestData))]
        public void GetVolumeInformationTest(string RootPathName)
        {
            if (GetVolumeInformation(RootPathName, out var VolumeName, out var VolumeSerialNumber, out var MaximumComponentLength, out var FileSystemFlags, out var FileSystemName))
                Trace.WriteLine($"Return={true}: {nameof(VolumeName)}:{VolumeName}, {nameof(VolumeSerialNumber)}:{VolumeSerialNumber}, {nameof(MaximumComponentLength)}:{MaximumComponentLength}, {nameof(FileSystemFlags)}:{FileSystemFlags}, {nameof(FileSystemName)}:{FileSystemName}");
            else
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        private static IEnumerable<object[]> GetVolumePathNamesForVolumeNameTestData => FindVolumes().Select(v => new object[] { v });
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
