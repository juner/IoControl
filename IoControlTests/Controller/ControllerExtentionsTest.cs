using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IoControl.IoControlTestUtils;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl.Controller;

namespace IoControl.Controller.Tests
{
    [TestClass]
    public class ControllerExtentionsTest
    {
        [TestMethod]
        public void ScsiGetAddressTest()
        {
            foreach(var IoControl in GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(ControllerExtentions.ScsiGetAddress)}: {IoControl.ScsiGetAddress()}");
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
        [TestMethod]
        public void AtaPassThroughIdentifyDeviceTest()
        {
            foreach (var IoControl in GetPhysicalDrives(FileAccess: System.IO.FileAccess.ReadWrite, CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    IoControl.AtaPassThroughIdentifyDevice(out var identity);
                    Trace.WriteLine($"{nameof(ControllerExtentions.AtaPassThroughIdentifyDevice)}: {identity}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
        [TestMethod]
        public void AtaPassThroughSmartAttributesTest()
        {
            foreach (var IoControl in GetPhysicalDrives(FileAccess: System.IO.FileAccess.ReadWrite, CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    IoControl.AtaPassThroughSmartAttributes(out var attributes);
                    Trace.WriteLine($"{nameof(ControllerExtentions.AtaPassThroughSmartAttributes)}:");
                    foreach (var attribute in attributes)
                        Trace.WriteLine(attribute);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
    }
}
