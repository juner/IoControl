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
                yield return new object[] { Path.GetPathRoot(Environment.SystemDirectory).TrimEnd('\\') };
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
        private static IEnumerable<object[]> GetVolumePathNamesForVolumeNameTestData => GetVolumePathNames().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(GetVolumePathNamesForVolumeNameTestData))]
        public void GetVolumePathNamesForVolumeNameTest(string VolumePathName)
        {
            foreach (var VolumeName in GetVolumePathNamesForVolumeName(VolumePathName))
                Trace.WriteLine(VolumeName);
;        }
    }
}
