using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IoControl.IoControlTestUtils;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl.Controller;

namespace IoControl.Tests.Controller
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
    }
}
