using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IoControl.Disk.Tests
{
    [TestClass]
    public class DiskDetectionInfoTest
    {
        [TestMethod]
        public void SizeOfTest() => Trace.WriteLine(Marshal.SizeOf<DiskDetectionInfo>());
    }
}
