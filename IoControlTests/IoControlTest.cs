using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl;
using IoControl.Disk;
using IoControl.MassStorage;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace IoControlTests
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
                    Trace.WriteLine(nameof(IOControlCode.VolumeGetVolumeDiskExtents));
                    //var BaseSize = Marshal.SizeOf(typeof(_VolumeDiskExtent));
                    //var ExtentSize = Marshal.SizeOf(typeof(DiskExtent));
                    //var Size = (uint)(BaseSize + ExtentSize * 10);
                    //do
                    //{
                    //    var OutPtr = Marshal.AllocCoTaskMem((int)Size);
                    //    using (IoCtrl.Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
                    //    {
                    //        uint returnbytes;
                    //        bool result;
                    //        (result, returnbytes) = IoCtrl.DeviceIoControlOutOnly(file, IoCtrl.IOControlCode.VolumeGetVolumeDiskExtents, OutPtr, Size);
                    //        var hResult = Marshal.GetHRForLastWin32Error();
                    //        if (hResult == unchecked((int)0x8007007A))
                    //        {
                    //            Size *= 2;
                    //            continue;
                    //        }
                    //        if (!result)
                    //            Marshal.ThrowExceptionForHR(hResult);
                    //        var _VolumeDisk = (_VolumeDiskExtent)Marshal.PtrToStructure(OutPtr, typeof(_VolumeDiskExtent));
                    //        var VolumeDisk = new VolumeDiskExtent
                    //        {
                    //            NumberOfDiskExtents = _VolumeDisk.NumberOfDiskExtents,
                    //            Extents = Enumerable
                    //                .Range(0, (int)_VolumeDisk.NumberOfDiskExtents)
                    //                .Select(index => (DiskExtent)Marshal.PtrToStructure(OutPtr + BaseSize * ExtentSize, typeof(DiskExtent)))
                    //                .ToArray(),
                    //        };
                    //        Trace.WriteLine(VolumeDisk);
                    //    }
                    //    break;
                    //} while (true);
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.VolumeGetVolumeDiskExtents, out VolumeDiskExtent extent, out var ReturnBytes);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    Trace.WriteLine(extent);
                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.VolumeIsClustered));
                    var result = IoControl.DeviceIoControl(IOControlCode.VolumeIsClustered, out var _);
                    Trace.WriteLine($"Clustored:{result}");
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
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough) + " :IDENTIFY DEVICE");
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
                            CurrentTaskFile = new byte[8] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xEc, 0x0},
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
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough) + " :S.M.A.R.T");
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
                            CurrentTaskFile = new byte[8] { 0xd0, 0x0, 0x0, 0x4f, 0xc2, 0x0, 0xb0, 0x0 },
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
                    Trace.WriteLine(nameof(IOControlCode.DiskPerformance));
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskPerformance, out DiskPerformance performance, out var _);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
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
        IEnumerable<IoControl.IoControl> GetPhysicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default)
        {
            bool hasDrive = false;
            foreach (var PhysicalNumber in Enumerable.Range(0,10))
            {
                var Path = $@"\\.\PhysicalDrive{PhysicalNumber}";
                using (var file = new IoControl.IoControl(Path, FileAccess, FileShare, CreationDisposition, FlagAndAttributes))
                {
                    Trace.WriteLine($"Open {Path} ... {(file.IsInvalid ? "NG" : "OK")}.");
                    if (file.IsInvalid)
                        continue;
                    hasDrive = true;
                    yield return file;
                }
            }
            if (!hasDrive)
                throw new AssertInconclusiveException("対象となるドライブがありません。");
        }
        IEnumerable<IoControl.IoControl> GetLogicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default)
        {
            bool hasDrive = false;
            foreach (var drivePath in Environment.GetLogicalDrives())
            {
                var Path = @"\\.\" + drivePath.TrimEnd('\\');
                using (var file = new IoControl.IoControl(Path, FileAccess, FileShare, CreationDisposition, FlagAndAttributes))
                {
                    Trace.WriteLine($"Open {Path} ... {(file.IsInvalid ? "NG" : "OK")}.");
                    if (file.IsInvalid)
                        continue;
                    hasDrive = true;
                    yield return file;
                }
            }
            if (!hasDrive)
                throw new AssertInconclusiveException("対象となるドライブがありません。");
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
                    Trace.WriteLine(nameof(DiskExtensions.DiskGetCacheInformation));
                    var information = IoControl.DiskGetCacheInformation();
                    Trace.WriteLine(information);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }

            }
        }
        [Flags]
        enum AtaFlags : ushort
        {
            DrdyRequired = (1 << 0),
            DataIn = (1 << 1),
            DataOut = (1 << 2),
            AF_48BIT_COMMAND = (1 << 3),
            UseDma = (1 << 4),
            NoMultiple = (1 << 5),
        }
        [StructLayout(LayoutKind.Sequential)]
        struct DiskControllerNumber
        {
            public FileDevice DeviceType;
            public uint ControllerNumber;
            public uint DiskNumber;
            public override string ToString()
                => $"{nameof(DiskControllerNumber)}{{{nameof(ControllerNumber)}:{ControllerNumber},{nameof(DiskNumber)}:{DiskNumber}}}";
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
        private struct AtaPassThroughEx
        {
            public ushort Length;
            public AtaFlags AtaFlags;
            public byte PathId;
            public byte TargetId;
            public byte Lun;
            public byte ReservedAsUchar;
            public uint DataTransferLength;
            public uint TimeOutValue;
            public uint ReservedAsUlong;
            public IntPtr DataBufferOffset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] PreviousTaskFile;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] CurrentTaskFile;
            public override string ToString()
                => $"{nameof(AtaPassThroughEx)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", (PreviousTaskFile ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", (CurrentTaskFile ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ATAIdentifyDeviceQuery
        {
            public AtaPassThroughEx Header;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Data;
            public override string ToString()
                => $"{nameof(ATAIdentifyDeviceQuery)}{{ {nameof(Header)}:{Header}, {nameof(Data)}:[{string.Join(" ", (Data ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}] }}";
        }
        [StructLayout(LayoutKind.Sequential)]
        struct DiskPerformance
        {
            public long BytesRead;
            public long BytesWritten;
            public long ReadTime;
            public long WriteTime;
            public long IdleTime;
            public uint ReadCount;
            public uint WriteCount;
            public uint QueueDepth;
            public uint SplitCount;
            public long QueryTime;
            public uint StorageDeviceNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] StorageManagerName;
            public override string ToString()
                => $"{nameof(DiskPerformance)}{{{nameof(BytesRead)}:{BytesRead}, {nameof(BytesWritten)}:{BytesWritten}, {nameof(ReadTime)}:{ReadTime}, {nameof(WriteTime)}:{WriteTime}, {nameof(IdleTime)}:{IdleTime}, {nameof(ReadCount)}:{ReadCount}, {nameof(WriteCount)}:{WriteCount}, {nameof(QueueDepth)}:{QueueDepth}, {nameof(SplitCount)}:{SplitCount}, {nameof(QueryTime)}:{QueryTime}, {nameof(StorageDeviceNumber)}:{StorageDeviceNumber}, {nameof(StorageManagerName)}:[{string.Join(" ", (StorageManagerName ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X2}"))}]}}";
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
        [StructLayout(LayoutKind.Sequential)]
        struct ScsiAddress
        {
            public uint Length;
            public byte PortNumber;
            public byte PathId;
            public byte TargetId;
            public byte Lun;
            public override string ToString()
                => $"{nameof(ScsiAddress)}{{{nameof(Length)}:{Length}, {nameof(PortNumber)}:{PortNumber}, {nameof(PathId)},{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}}}";
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        struct DiskExtent
        {
            public uint DiskNumber;
            public long StartingOffset;
            public long ExtentLength;
            public override string ToString()
                => $"{nameof(DiskExtent)}{{{nameof(DiskNumber)}:{DiskNumber}, {nameof(StartingOffset)}:{StartingOffset}, {nameof(ExtentLength)}:{ExtentLength}}}";
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        struct VolumeDiskExtent
        {
            public uint NumberOfDiskExtents;
            [MarshalAs(UnmanagedType.ByValArray)]
            public DiskExtent[] Extents;
            public override string ToString()
                => $"{nameof(VolumeDiskExtent)}{{{nameof(NumberOfDiskExtents)}:{NumberOfDiskExtents}, [{string.Join(" , ", (Extents ?? Enumerable.Empty<DiskExtent>()).Take((int)NumberOfDiskExtents).Select(v => $"{v}"))}]}}";
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        struct _VolumeDiskExtent
        {
            public uint NumberOfDiskExtents;
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
