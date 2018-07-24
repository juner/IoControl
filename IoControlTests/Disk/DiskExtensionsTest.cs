using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.IoControlTestUtils;
using System.Diagnostics;

namespace IoControl.Disk.Tests
{
    [TestClass]
    public class DiskExtensionsTest
    {
        [TestMethod]
        public void DiskGetCacheInformationTest()
        {
            foreach (var IoControl in GetPhysicalDrives(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(DiskExtensions.DiskGetCacheInformation)}: {IoControl.DiskGetCacheInformation()}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
        [TestMethod]
        public void DiskGetDriveLayoutExTest()
        {
            foreach (var IoControl in GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(DiskExtensions.DiskGetDriveLayoutEx)}: {IoControl.DiskGetDriveLayoutEx()}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
        [TestMethod]
        public void DiskGetLengthInfoTest()
        {
            foreach (var IoControl in GetPhysicalDrives(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(DiskExtensions.DiskGetLengthInfo)}: {IoControl.DiskGetLengthInfo()}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
        [TestMethod]
        public void DiskGetDriveGeometryExTest()
        {
            foreach (var IoControl in GetPhysicalDrives(FileAccess: System.IO.FileAccess.Read, CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(DiskExtensions.DiskGetDriveGeometryEx)}: {IoControl.DiskGetDriveGeometryEx()}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
        [TestMethod]
        public void DiskPerformanceTest()
        {
            foreach (var IoControl in GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(DiskExtensions.DiskPerformance)}: {IoControl.DiskPerformance()}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
    }
}
