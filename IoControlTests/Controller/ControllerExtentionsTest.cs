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
                    Trace.WriteLine($"{nameof(ControllerExtentions.AtaPassThroughIdentifyDevice)}: {IoControl.AtaPassThroughIdentifyDevice()}");
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
                    var result = IoControl.AtaPassThroughSmartAttributes();
                    Trace.WriteLine($"{nameof(ControllerExtentions.AtaPassThroughSmartAttributes)}:");
                    Trace.WriteLine(result.Header);
                    foreach (var attribute in result.Data)
                        Trace.WriteLine(attribute);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
    }
}
