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
            => Trace.WriteLine(IoControl.ScsiGetAddress());
        [TestMethod]
        [DynamicData(nameof(ScsiGetAddressTestData))]
        public void ScsiGetInquiryDataTest(IoControl IoControl)
            => Trace.WriteLine(IoControl.ScsiGetInquiryData());
        private static IEnumerable<object[]> AtaPassThroughIdentifyDeviceTestData => PhysicalDrivePath.GetIoControls(FileAccess: System.IO.FileAccess.ReadWrite, CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(AtaPassThroughIdentifyDeviceTestData))]
        public void AtaPassThroughIdentifyDeviceTest(IoControl IoControl)
            => Trace.WriteLine(IoControl.AtaPassThroughIdentifyDevice());
        private static IEnumerable<object[]> AtaPassThroughSmartDataTestData => PhysicalDrivePath.GetIoControls(FileAccess: System.IO.FileAccess.ReadWrite, CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(AtaPassThroughSmartDataTestData))]
        public void AtaPassThroughSmartDataTest(IoControl IoControl)
            => Trace.WriteLine(IoControl.AtaPassThroughSmartData());
        [TestMethod]
        [DynamicData(nameof(AtaPassThroughSmartDataTestData))]
        public void AtaPassThroughCheckPowerMode(IoControl IoControl)
            => Trace.WriteLine(IoControl.AtaPassThroughCheckPowerMode());
        private static IEnumerable<object[]> ScsiMiniportIdentifyTestData => ScsiDrivePath.GetIoControls(FileAccess: System.IO.FileAccess.ReadWrite, FileShare: System.IO.FileShare.ReadWrite, CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(ScsiMiniportIdentifyTestData))]
        public void ScsiMiniportIdentifyTest(IoControl IoControl)
            => Trace.WriteLine(IoControl.ScsiMiniportIdentify());

        private static IEnumerable<object[]> ScsiPassThroughIdentifyDeviceTestData => PhysicalDrivePath.GetIoControls(FileAccess: System.IO.FileAccess.ReadWrite, CreationDisposition: System.IO.FileMode.Open).Using().Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(ScsiPassThroughIdentifyDeviceTestData))]
        public void ScsiPassThroughIdentifyDeviceTest(IoControl IoControl)
            => Trace.WriteLine(IoControl.ScsiPassThroughIdentifyDevice(0xA0, CommandType.CmdTypeSat));
    }
}
