using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private static IEnumerable<object[]> FsctlIsVolumeMountedTestData {
            get {
                using (var IoControl = GetIoControl($@"\\.\{Path.GetPathRoot(Directory.GetCurrentDirectory()).TrimEnd('\\')}", FileAccess: FileAccess.Read, FileShare: FileShare.Read | FileShare.Write, CreationDisposition: FileMode.Open))
                    yield return new object[] { IoControl, true };
                foreach (var path in VolumePath.Concat(PhysicalDrivePath).Concat(LogicalDrivePath)
                .GetIoControls(FileAccess: FileAccess.Read, FileShare: FileShare.Read | FileShare.Write, CreationDisposition: FileMode.Open))
                    yield return new object[] { path, null };
            }
        }
        [TestMethod]
        [DynamicData(nameof(FsctlIsVolumeMountedTestData))]
        public void FsctlIsVolumeMountedTest(IoControl IoControl, bool? result)
        {
            var _result = IoControl.FsctlIsVolumeMounted();
            Trace.WriteLine(_result);
            if (result is bool Result)
                Assert.AreEqual(Result, _result);
        }
    }
}
