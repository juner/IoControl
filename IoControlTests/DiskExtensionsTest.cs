using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using static IoControl.IoControlTestUtils;
using IoControl.Disk;

namespace IoControl.Tests
{
    [TestClass]
    public class DiskExtensionsTest
    {
        [TestMethod]
        public void DiskGeometryExTest()
        {
            foreach(var IoControl in GetPhysicalDrives(CreationDisposition : System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(DiskExtensions.DiskGetDriveGeometryEx)}: {IoControl.DiskGetDriveGeometryEx()}");
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
    }
}
