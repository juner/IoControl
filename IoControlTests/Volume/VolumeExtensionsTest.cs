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
        [TestMethod]
        public void VolumeGetVolumeDiskExtentsTest()
        {
            foreach (var IoControl in GetLogicalDrives(CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(VolumeExtensions.VolumeGetVolumeDiskExtents)}: {IoControl.VolumeGetVolumeDiskExtents()}");
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
        }

    }
}
