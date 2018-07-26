using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static IoControl.IoControlTestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoControl.Volume.Tests
{
    [TestClass]
    public class VolumeExtensionsTest
    {
        private static IEnumerable<object[]> VolumeGetVolumeDiskExtentsTestData => GetLogicalDrives(CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(VolumeGetVolumeDiskExtentsTestData))]
        public void VolumeGetVolumeDiskExtentsTest(IoControl IoControl)
            => Trace.WriteLine($"{nameof(VolumeExtensions.VolumeGetVolumeDiskExtents)}: {IoControl.VolumeGetVolumeDiskExtents()}");

    }
}
