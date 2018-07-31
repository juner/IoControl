using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.IoControlTestUtils;

namespace IoControl.FileSystem.Tests
{
    [TestClass]
    public class FileSystemExtensionsTest
    {
        private static IEnumerable<object[]> FsctlIsVolumeMountedTestData => VolumePathGenerator().Concat(PhysicalDrivePathGenerator()).Concat(LogicalDrivePathGenerator())
            .GetIoControls(FileAccess: System.IO.FileAccess.Read, FileShare: System.IO.FileShare.Read | System.IO.FileShare.Write, CreationDisposition: System.IO.FileMode.Open).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(FsctlIsVolumeMountedTestData))]
        public void FsctlIsVolumeMountedTest(IoControl IoControl) => Trace.WriteLine(IoControl.FsctlIsVolumeMounted());
    }
}
