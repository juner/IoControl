using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoControl.Utils.Tests
{
    [TestClass]
    public class DeviceUtilsTest
    {
        private static IEnumerable<object[]> QueryDocDeviceTestData {
            get {
                yield return new object[] { null };
                yield return new object[] { Path.GetPathRoot(Environment.SystemDirectory).Replace("\\", "") };
            }
        }
        [TestMethod]
        [DynamicData(nameof(QueryDocDeviceTestData))]
        public void QueryDocDeviceTest(string DeviceName)
            => Trace.WriteLine(string.Join(" , ", DeviceUtils.QueryDocDevice(DeviceName)));
    }
}
