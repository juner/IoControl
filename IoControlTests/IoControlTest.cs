using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl;
using IoControl.Disk;
using IoControl.MassStorage;
using System.Linq;
using System.Diagnostics;
using System.IO;
using IoControl.Volume;
using IoControl.Controller;
using static IoControl.IoControlTestUtils;
using static IoControl.Utils.DeviceUtils;
using System.Collections.Generic;

namespace IoControl.Tests
{
    [TestClass]
    public class IoControlTest
    {
        private static IEnumerable<object[]> NewIoControlTestData {
            get {
                foreach (var LogicalDrivePath in GetLogicalDriveStrings().Select(path => $@"\\.\{path.TrimEnd('\\')}"))
                    yield return new object[] { LogicalDrivePath, default(FileAccess), default(FileShare), FileMode.Open, default(FileAttributes) };
                foreach (var PhysicalDrivePath in QueryDocDevice().Where(DeviceName => DeviceName.IndexOf("PhysicalDrive") == 0).Select(DeviceName => $@"\\.\{DeviceName}"))
                    yield return new object[] { PhysicalDrivePath, default(FileAccess), default(FileShare), FileMode.Open, default(FileAttributes) };
                foreach (var VolumeName in FindVolumes()
                    .Select(v => v.Replace(@"\\?\",@"\\.\").TrimEnd('\\')))
                    yield return new object[] { VolumeName, default(FileAccess), default(FileShare), FileMode.Open, default(FileAttributes) };
                foreach (var VolumeName in FindVolumes()
                    // \\?\Volume{GUDI}\ -> Volume{GUID}
                    .Select(v => v.Substring(@"\\?\".Length).TrimEnd('\\'))
                    // Volume{GUID} -> \Device\HarddiskVolumeN
                    .SelectMany(v => QueryDocDevice(v))
                    // \Device\HarddiskVolumeN -> \\.\HarddiskVolumeN
                    .Select(v => $@"\\.\{v.Substring(@"\Device\".Length)}")
               )
                    yield return new object[] { VolumeName, default(FileAccess), default(FileShare), FileMode.Open, default(FileAttributes) };
                foreach (var VolumeName in QueryDocDevice()
                        .Where(v => v.StartsWith("HarddiskVolume"))
                        .Select(v => $@"\\.\{v}"))
                    yield return new object[] { VolumeName, default(FileAccess), default(FileShare), FileMode.Open, default(FileAttributes) };
            }
        }
        [TestMethod]
        [DynamicData(nameof(NewIoControlTestData))]
        public void NewIoControlTest(string Path, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileFlagAndAttributes FlagAndAttributes = default)
        {
            using (var IoControl = new IoControl(Path, FileAccess, FileShare, CreationDisposition, FlagAndAttributes))
            {
                if (IoControl.IsInvalid)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                Trace.WriteLine(IoControl);
            }
        }
        private static IEnumerable<object[]> PhysicalDriveOpenTestData => GetPhysicalDrives(
                FileAccess: FileAccess.ReadWrite,
                    FileShare: FileShare.ReadWrite,
                    CreationDisposition: FileMode.Open,
                    FlagAndAttributes: FileFlagAndAttributesExtensions.Create(FileAttributes.Normal)).Select(v => new object[] { v });
        [TestMethod]
        [DynamicData(nameof(PhysicalDriveOpenTestData))]
        public void PhysicalDriveOpenTest(IoControl IoControl)
        {
            try
            {
                Trace.WriteLine(nameof(MassStorageExtensions.StorageGetDeviceNumber));
                var number = IoControl.StorageGetDeviceNumber();
                Trace.WriteLine(number);
            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(DiskExtensions.DiskGetDriveGeometryEx));
                var geometry = IoControl.DiskGetDriveGeometryEx();
                Trace.WriteLine(geometry);
            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(DiskExtensions.DiskGetLengthInfo));
                var disksize = IoControl.DiskGetLengthInfo();
                Trace.WriteLine(disksize);
            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(DiskExtensions.DiskGetDriveLayoutEx));
                var layout = IoControl.DiskGetDriveLayoutEx();
                Trace.WriteLine(layout);
            }catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            try
            {
                Trace.WriteLine(nameof(IOControlCode.StorageQueryProperty));
                var dest = IoControl.StorageQueryProperty(StoragePropertyId.StorageDeviceSeekPenaltyProperty, default);
                Trace.WriteLine(dest);
            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(ControllerExtentions.AtaPassThroughIdentifyDevice));
                Trace.WriteLine(IoControl.AtaPassThroughIdentifyDevice());

            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(ControllerExtentions.AtaPassThroughSmartData));
                var result = IoControl.AtaPassThroughSmartData();
                Trace.WriteLine(result.Header);
                Trace.WriteLine(result.Data);

            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(IOControlCode.AtaPassThrough) + " :STANDBY IMMEDIATE");
                var Length = (ushort)Marshal.SizeOf(typeof(AtaPassThroughEx));
                var id_query = IoControl.AtaPassThrough(
                        AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                        Command: 0xE0,
                        DataSize: 512
                    );
                Trace.WriteLine(id_query.Header);

            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(IOControlCode.AtaPassThrough) + " :CHECK POWER MODE");
                var Length = (ushort)Marshal.SizeOf(typeof(AtaPassThroughEx));
                IoControl.AtaPassThrough(
                        out var Header,
                        out var Data,
                        AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                        TimeOutValue: 3,
                        Command: 0xE5,
                        DataSize: 512
                    );
                Trace.WriteLine(Header);
                Trace.WriteLine(Data);

            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(DiskExtensions.DiskPerformance));
                IoControl.DiskPerformance(out var performance);
                Trace.WriteLine(performance);
            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(IOControlCode.ScsiGetAddress));
                var result = IoControl.DeviceIoControlOutOnly(IOControlCode.ScsiGetAddress, out ScsiAddress address, out var _);
                if (!result)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                Trace.WriteLine(address);
            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
            try
            {
                Trace.WriteLine(nameof(IOControlCode.ScsiGetInquiryData));
                var Size = (uint)(Marshal.SizeOf(typeof(_ScsiAdapterBusInfo)) + Marshal.SizeOf(typeof(ScsiBusData)) + Marshal.SizeOf(typeof(ScsiInquiryData)));
                var OutPtr = Marshal.AllocCoTaskMem((int)Size);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
                {
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.ScsiGetInquiryData, OutPtr, Size, out var returnBytes);
                    var lasterror = Marshal.GetHRForLastWin32Error();
                    if (!result)
                        Marshal.ThrowExceptionForHR(lasterror);
                    Trace.WriteLine(returnBytes);
                }
            }
            catch (Exception e2)
            {
                Trace.WriteLine(e2);
            }
        }
        private static IEnumerable<object[]> DriveOpenTestData => GetLogicalDrives(FileShare: FileShare.ReadWrite, CreationDisposition: FileMode.Open).Select(v => new object[] { v });

        [TestMethod]
        [DynamicData(nameof(DriveOpenTestData))]
        public void DriveOpenTest(IoControl IoControl)
    {
            try
            {
                Trace.WriteLine(nameof(MassStorageExtensions.StorageGetDeviceNumber));
                var number = IoControl.StorageGetDeviceNumber();
                Trace.WriteLine(number);
            }catch(Exception e)
            {
                Trace.WriteLine(e);
            }
            try
            {
                Trace.WriteLine(nameof(VolumeExtensions.VolumeIsClustered));
                var result = IoControl.VolumeIsClustered();
                Trace.WriteLine($"Clustored:{result}");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            try
            {
                Trace.WriteLine(nameof(VolumeExtensions.VolumeGetVolumeDiskExtents));
                var extent = IoControl.VolumeGetVolumeDiskExtents();
                Trace.WriteLine(extent);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }

            try
            {
                Trace.WriteLine(nameof(VolumeExtensions.VolumeSupportsOnlineOffline));
                var result = IoControl.VolumeSupportsOnlineOffline();
                Trace.WriteLine(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            try
            {
                Trace.WriteLine(nameof(VolumeExtensions.VolumeIsOffline));
                var result = IoControl.VolumeIsOffline();
                Trace.WriteLine(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            try
            {
                Trace.WriteLine(nameof(VolumeExtensions.VolumeIsIoCapale));
                var result = IoControl.VolumeIsIoCapale();
                Trace.WriteLine(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            try
            {
                Trace.WriteLine(nameof(VolumeExtensions.VolumeQueryVolumeNumber));
                var result = IoControl.VolumeQueryVolumeNumber();
                Trace.WriteLine(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            try
            {
                Trace.WriteLine(nameof(VolumeExtensions.VolumeGetGptAttribute));
                var attribute = IoControl.VolumeGetGptAttribute();
                Trace.WriteLine(attribute);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        struct ScsiAdapterBusInfo
        {
            public ushort NumberOfBuses;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public ScsiBusData[] BusData;
            public ScsiInquiryData[] InquiryData;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct _ScsiAdapterBusInfo
        {
            public byte NumberOfBuses;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct ScsiBusData
        {
            public byte NumberOfLogicalUnits;
            public byte InitiatorBusId;
            public IntPtr InquiryDataOffset;
            public override string ToString()
                => $"{nameof(ScsiBusData)}{{{nameof(NumberOfLogicalUnits)}:{NumberOfLogicalUnits}, {nameof(InitiatorBusId)}:{InitiatorBusId}, {nameof(InquiryDataOffset)}:{InquiryDataOffset}}}";
        }
        [StructLayout(LayoutKind.Sequential)]
        struct ScsiInquiryData
        {
            public byte PathId;
            public byte TargetId;
            public byte Lun;
            public bool DeviceClaimed;
            public uint InquiryDataLength;
            public IntPtr NextInquirydataOffset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] InquiryData;
            public override string ToString()
                => $"{nameof(ScsiInquiryData)}{{{nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(DeviceClaimed)}:{DeviceClaimed}, {nameof(InquiryDataLength)}:{InquiryDataLength}, {nameof(NextInquirydataOffset)}:{NextInquirydataOffset}, {nameof(InquiryData)}:[{string.Join(" ", (InquiryData ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
        }
    }
}
