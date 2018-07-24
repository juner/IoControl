﻿using System;
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

namespace IoControl.Tests
{
    [TestClass]
    public class IoControlTest
    {
        [TestMethod]
        public void PhysicalDriveOpenTest()
        {
            foreach (var IoControl in GetPhysicalDrives(
                FileAccess: FileAccess.ReadWrite,
                    FileShare: FileShare.ReadWrite,
                    CreationDisposition: FileMode.Open,
                    FlagAndAttributes: FileAttributes.Normal))
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
                    IoControl.AtaPassThroughIdentifyDevice(out var Identity);
                    Trace.WriteLine(Identity);

                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
                try
                {
                    Trace.WriteLine(nameof(ControllerExtentions.AtaPassThroughSmartAttributes));
                    IoControl.AtaPassThroughSmartAttributes(out var attributes);
                    foreach(var attribute in attributes)
                        Trace.WriteLine(attribute);

                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough) + " :STANDBY IMMEDIATE");
                    var Length = (ushort)Marshal.SizeOf(typeof(AtaPassThroughEx));
                    var id_query = new ATAIdentifyDeviceQuery
                    {
                        Header = new AtaPassThroughEx
                        {
                            Length = Length,
                            AtaFlags = AtaFlags.DataIn | AtaFlags.NoMultiple,
                            DataTransferLength = (uint)256 * sizeof(ushort),
                            TimeOutValue = 3,
                            DataBufferOffset = Marshal.OffsetOf(typeof(ATAIdentifyDeviceQuery), nameof(ATAIdentifyDeviceQuery.Data)),
                            PreviousTaskFile = new byte[8],
                            CurrentTaskFile = new byte[8] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0 },
                        },
                        Data = new ushort[256],
                    };
                    var result = IoControl.DeviceIoControl(IOControlCode.AtaPassThrough, ref id_query, out var retval_size);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    Trace.WriteLine(retval_size);
                    Trace.WriteLine(id_query);

                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough) + " :CHECK POWER MODE");
                    var Length = (ushort)Marshal.SizeOf(typeof(AtaPassThroughEx));
                    var id_query = new ATAIdentifyDeviceQuery
                    {
                        Header = new AtaPassThroughEx
                        {
                            Length = Length,
                            AtaFlags = AtaFlags.DataIn | AtaFlags.NoMultiple,
                            DataTransferLength = (uint)256 * sizeof(ushort),
                            TimeOutValue = 3,
                            DataBufferOffset = Marshal.OffsetOf(typeof(ATAIdentifyDeviceQuery), nameof(ATAIdentifyDeviceQuery.Data)),
                            PreviousTaskFile = new byte[8],
                            CurrentTaskFile = new byte[8] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE5, 0x0 },
                        },
                        Data = new ushort[256],
                    };
                    var result = IoControl.DeviceIoControl(IOControlCode.AtaPassThrough, ref id_query, out var retval_size);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    Trace.WriteLine(retval_size);
                    Trace.WriteLine(id_query);

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
        }
        [TestMethod]
        public void DriveOpenTest()
        {
            foreach(var IoControl in GetLogicalDrives(FileShare: FileShare.ReadWrite, CreationDisposition: FileMode.Open))
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
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct AtaPassThroughDirect
        {
            public ushort Length;
            public ushort AtaFlags;
            public byte PathId;
            public byte TargetId;
            public byte Lun;
            public byte ReservedAsUchar;
            public uint DataTransferLength;
            public uint TimeOutValue;
            public uint ReservedAsUlong;
            public IntPtr DataBuffer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] PreviousTaskFile;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] CurrentTaskFile;
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
        internal class Disposable : IDisposable
        {
            Action Action;
            internal Disposable(Action Action) => this.Action = Action;
            public static Disposable Create(Action Action) => new Disposable(Action);
            public void Dispose()
            {
                try
                {
                    Action?.Invoke();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
                Action = null;
            }
        }
    }
}
