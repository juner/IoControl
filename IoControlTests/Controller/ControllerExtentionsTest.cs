﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IoControl.IoControlTestUtils;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl.Controller;
using static IoControl.Tests.CheckTool;

namespace IoControl.Controller.Tests
{
    [TestClass]
    public class ControllerExtentionsTest
    {
        private static IEnumerable<object[]> ScsiGetAddressTestData => PhysicalDrivePath.GetIoControls(CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(ScsiGetAddressTestData))]
        public void ScsiGetAddressTest(IoControl IoControl)
            => Trace.WriteLine($"{nameof(ControllerExtentions.ScsiGetAddress)}: {IoControl.ScsiGetAddress()}");
        private static IEnumerable<object[]> AtaPassThroughIdentifyDeviceTestData => PhysicalDrivePath.GetIoControls(FileAccess: System.IO.FileAccess.ReadWrite, CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(AtaPassThroughIdentifyDeviceTestData))]
        public void AtaPassThroughIdentifyDeviceTest(IoControl IoControl)
            => Trace.WriteLine($"{nameof(ControllerExtentions.AtaPassThroughIdentifyDevice)}: {IoControl.AtaPassThroughIdentifyDevice()}");
        private static IEnumerable<object[]> AtaPassThroughSmartAttributesTestData => PhysicalDrivePath.GetIoControls(FileAccess: System.IO.FileAccess.ReadWrite, CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(AtaPassThroughSmartAttributesTestData))]
        public void AtaPassThroughSmartAttributesTest(IoControl IoControl)
        {
            var result = IoControl.AtaPassThroughSmartAttributes();
            Trace.WriteLine($"{nameof(ControllerExtentions.AtaPassThroughSmartAttributes)}:");
            Trace.WriteLine(result.Header);
            foreach (var attribute in result.Data)
                Trace.WriteLine(attribute);
        }
    }
}
