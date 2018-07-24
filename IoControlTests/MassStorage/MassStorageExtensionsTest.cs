using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IoControl.IoControlTestUtils;
using System.Diagnostics;
using IoControl.MassStorage;

namespace IoControl.MassStorage.Tests
{
    [TestClass]
    public class MassStorageExtensionsTest
    {
        [TestMethod]
        public void StorageGetDeviceNumberTest()
        {
            foreach (var IoControl in GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(MassStorageExtensions.StorageGetDeviceNumber)}: {IoControl.StorageGetDeviceNumber()}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
        [TestMethod]
        [DataRow(StoragePropertyId.StorageDeviceIdProperty, StorageQueryType.StandardQuery)]
        public void StorageQueryPropertyTest(StoragePropertyId StoragePropertyId, StorageQueryType StorageQueryType)
        {
            foreach (var IoControl in GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    Trace.WriteLine($"{nameof(MassStorageExtensions.StorageQueryProperty)}: {IoControl.StorageQueryProperty(StoragePropertyId, StorageQueryType)}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
        [TestMethod]
        public void StorageGetMediaSerialNumberTest()
        {
            foreach (var IoControl in GetPhysicalDrives(CreationDisposition: System.IO.FileMode.Open))
                try
                {
                    IoControl.StorageGetMediaSerialNumber(out var serial);
                    Trace.WriteLine($"{nameof(MassStorageExtensions.StorageGetMediaSerialNumber)}: {serial}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
        }
    }
}
