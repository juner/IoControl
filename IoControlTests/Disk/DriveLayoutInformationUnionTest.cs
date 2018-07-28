﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IoControl.Disk.Tests
{
    [TestClass]
    public class DriveLayoutInformationUnionTest
    {
        [TestMethod]
        public void SizeOfTest() => Trace.WriteLine(Marshal.SizeOf<DriveLayoutInformationUnion>());
    }
}