using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;

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
                    Trace.WriteLine(nameof(IOControlCode.StorageGetDeviceNumber));
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetDeviceNumber, out StorageDeviceNumber number, out var _);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
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
                    Trace.WriteLine(nameof(IOControlCode.DiskGetDriveGeometryEx));
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveGeometryEx, out DiskGeometryEx geometry, out var _);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    Trace.WriteLine(geometry);
                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetLengthInfo));
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetLengthInfo, out long disksize, out var _);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    Trace.WriteLine(disksize);
                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetDriveLayoutEx));
                    var s1 = Marshal.SizeOf<PartitionInformationGpt>();
                    var s2 = Marshal.SizeOf<PartitionInformationMbr>();
                    var s3 = Marshal.SizeOf<PartitionInformationUnion>();
                    var s4 = Marshal.SizeOf<PartitionInformationEx>();
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveLayoutEx, out DriveLayoutInformationEx layout, out var outsize);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    Trace.WriteLine(layout);
                }catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.StorageQueryProperty));
                    var query = new StoragePropertyQuery
                    {
                        PropertyId = StoragePropertyId.StorageDeviceSeekPenaltyProperty,
                        QueryType = default,
                    };
                    var result = IoControl.DeviceIoControl(IOControlCode.StorageQueryProperty, ref query, out DeviceSeekPenaltyDescriptor dest, out uint penalty_size);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    Trace.WriteLine(query);
                    Trace.WriteLine(dest);
                    Trace.WriteLine(penalty_size);
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
                    Trace.WriteLine(nameof(IOControlCode.StorageGetDeviceNumber));
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetDeviceNumber, out StorageDeviceNumber number, out var _);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    Trace.WriteLine(number);
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetCacheInformation));
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetDeviceNumber, out DiskCacheInformation information, out var _);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
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
        struct StorageDeviceNumber
        {
            public FileDevice DeviceType;
            public uint DeviceNumber;
            public uint PartitionNumber;
            public override string ToString()
                => $"{nameof(StorageDeviceNumber)}{{{nameof(DeviceType)}:{DeviceType}, {nameof(DeviceNumber)}:{DeviceNumber}, {nameof(PartitionNumber)}:{PartitionNumber}}}";
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
        private struct StoragePropertyQuery
        {
            public StoragePropertyId PropertyId;
            public StorageQueryType QueryType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] AdditionalParameters;
            public override string ToString()
                => $"{nameof(StoragePropertyQuery)}{{{nameof(PropertyId)}:{PropertyId},{nameof(QueryType)}:{QueryType},[{string.Join(" ", (AdditionalParameters ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
        }
        enum StoragePropertyId : uint
        {
            StorageDeviceProperty = 0,
            StorageAdapterProperty = 1,
            StorageDeviceIdProperty = 2,
            StorageDeviceUniqueIdProperty = 3,
            StorageDeviceWriteCacheProperty = 4,
            StorageMiniportProperty = 5,
            StorageAccessAlignmentProperty = 6,
            StorageDeviceSeekPenaltyProperty = 7,
            StorageDeviceTrimProperty = 8,
            StorageDeviceWriteAggregationProperty = 9,
            StorageDeviceDeviceTelemetryProperty = 10, // 0xA
            StorageDeviceLBProvisioningProperty = 11, // 0xB
            StorageDevicePowerProperty = 12, // 0xC
            StorageDeviceCopyOffloadProperty = 13, // 0xD
            StorageDeviceResiliencyProperty = 14 // 0xE
        }
        enum StorageQueryType : uint
        {
            /// <summary>Instructs the driver to return an appropriate descriptor.</summary>
            PropertyStandardQuery = 0,
            /// <summary>Instructs the driver to report whether the descriptor is supported.</summary>
            PropertyExistsQuery = 1,
            /// <summary>Used to retrieve a mask of writeable fields in the descriptor. Not currently supported. Do not use.</summary>
            PropertyMaskQuery = 2,
            /// <summary>Specifies the upper limit of the list of query types. This is used to validate the query type.</summary>
            PropertyQueryMaxDefined = 3
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct DeviceSeekPenaltyDescriptor
        {
            public uint Version;
            public uint Size;
            [MarshalAs(UnmanagedType.U1)]
            public bool IncursSeekPenalty;
            public override string ToString()
                => $"{nameof(DeviceSeekPenaltyDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(IncursSeekPenalty)}:{IncursSeekPenalty}}}";
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
        [StructLayout(LayoutKind.Sequential)]
        struct DiskGeometry
        {
            public long Cylinders;
            public MediaType MediaType;
            public uint TrackPerCylinder;
            public uint SectorsPerTrack;
            public uint BytesPerSector;
            public override string ToString()
                => $"{nameof(DiskGeometry)}{{{nameof(Cylinders)}:{Cylinders}, {nameof(MediaType)}:{MediaType}, {nameof(TrackPerCylinder)}:{TrackPerCylinder}, {nameof(SectorsPerTrack)}:{SectorsPerTrack}, {nameof(BytesPerSector)}:{BytesPerSector}}}";
        }
        enum MediaType : uint
        {
            Unkowon = 0,
            RemovableMedia = 11,
            FixedMedia = 12,
        }
        [StructLayout(LayoutKind.Sequential)]
        struct DiskGeometryEx
        {
            public DiskGeometry Geometry;
            public long DiskSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Data;
            public override string ToString()
                => $"{nameof(DiskGeometryEx)}{{{nameof(Geometry)}:{Geometry}, {nameof(DiskSize)}:{DiskSize}, {nameof(Data)}:[{string.Join(" ", (Data ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
        }
        [StructLayout(LayoutKind.Explicit, Size =24)]
        struct DiskCacheInformation
        {
            [FieldOffset(0)]
            public bool ParametersSavable;
            [FieldOffset(1)]
            public bool ReadCacheEnabled;
            [FieldOffset(2)]
            public bool WriteCacheEnabled;
            [FieldOffset(4)]
            public DiskCacheRetentionPriority ReadRetentionPriority;
            [FieldOffset(8)]
            public DiskCacheRetentionPriority WriteRetentionPriority;
            [FieldOffset(12)]
            public ushort DisablePrefetchTransferLength;
            [FieldOffset(14)]
            public bool PrefetchScalar;
            [FieldOffset(16)]
            public DiskCacheInformationScalarPrefetch ScalarPrefetch;
            [FieldOffset(16)]
            public DiskCacheInformationBlockPrefetch BlockPrefetch;
            public override string ToString()
                => $"{nameof(DiskCacheInformation)}{{{nameof(ParametersSavable)}:{ParametersSavable}, {nameof(ReadCacheEnabled)}:{ReadCacheEnabled}, {nameof(WriteCacheEnabled)}:{WriteCacheEnabled}, {nameof(ReadRetentionPriority)}:{ReadRetentionPriority}, {nameof(WriteRetentionPriority)}:{WriteRetentionPriority}, {nameof(DisablePrefetchTransferLength)}:{DisablePrefetchTransferLength}, {nameof(PrefetchScalar)}:{PrefetchScalar}, {(PrefetchScalar ? $"{nameof(ScalarPrefetch)}:{ScalarPrefetch}" : $"{nameof(BlockPrefetch)}:{BlockPrefetch}")}}}";
        }
        [StructLayout(LayoutKind.Sequential)]
        struct DiskCacheInformationScalarPrefetch
        {
            public ushort Minimum;
            public ushort Maximum;
            public ushort MaximumBlocks;
            public override string ToString()
                => $"{nameof(DiskCacheInformationScalarPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}, {nameof(MaximumBlocks)}:{MaximumBlocks}}}";
        }
        [StructLayout(LayoutKind.Sequential)]
        struct DiskCacheInformationBlockPrefetch
        {
            public ushort Minimum;
            public ushort Maximum;
            public override string ToString()
                => $"{nameof(DiskCacheInformationBlockPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}}}";
        }
        enum DiskCacheRetentionPriority : int
        {
            EqualPriority,
            KeepPrefetchedData,
            KeepReadData
        };
        public enum PartitionStyle : uint
        {
            Mbr = 0,
            Gpt = 1,
            Raw = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DriveLayoutInformationEx
        {
            public PartitionStyle PartitionStyle;

            public uint PartitionCount;

            public DriveLayoutInformationUnion DriveLayoutInformaition;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 0x16)]
            public PartitionInformationEx[] PartitionEntry;
            public override string ToString()
                => $"{nameof(DriveLayoutInformationEx)}{{ {nameof(PartitionStyle)}:{PartitionStyle}, {nameof(PartitionCount)}:{PartitionCount}, {nameof(DriveLayoutInformaition)}:{(PartitionStyle == PartitionStyle.Gpt ? DriveLayoutInformaition.Gpt.ToString() : PartitionStyle == PartitionStyle.Mbr ? DriveLayoutInformaition.Mbr.ToString() : null)}, {nameof(PartitionEntry)}:[{string.Join(", ",(PartitionEntry ?? Enumerable.Empty<PartitionInformationEx>()).Take((int)PartitionCount).Select(v => $"{v}"))}] }}";
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct DriveLayoutInformationUnion
        {
            [FieldOffset(0)]
            public DriveLayoutInformationMbr Mbr;

            [FieldOffset(0)]
            public DriveLayoutInformationGpt Gpt;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DriveLayoutInformationMbr
        {
            public uint Signature;
            public uint CheckSum;
            public override string ToString()
                => $"{nameof(DriveLayoutInformationMbr)}{{{nameof(Signature)}:{Signature}, {nameof(CheckSum)}:{CheckSum}}}";
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DriveLayoutInformationGpt
        {
            public Guid DiskId;
            public long StartingUsableOffset;
            public long UsableLength;
            public uint MaxPartitionCount;
            public override string ToString()
                => $"{nameof(DriveLayoutInformationGpt)}:{{{nameof(DiskId)}:{DiskId}, {nameof(StartingUsableOffset)}:{StartingUsableOffset}, {nameof(UsableLength)}:{UsableLength}, {nameof(MaxPartitionCount)}:{MaxPartitionCount}}}";
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PartitionInformationEx
        {
            [MarshalAs(UnmanagedType.U4)]
            public PartitionStyle PartitionStyle;
            public long StartingOffset;
            public long PartitionLength;
            public uint PartitionNumber;
            public bool RewritePartition;
            public PartitionInformationUnion Info;
            public PartitionInformationMbr Mbr { get => Info.Mbr; set => Info.Mbr = value; }
            public PartitionInformationGpt Gpt { get => Info.Gpt; set => Info.Gpt = value; }
            public override string ToString()
            {
                return $"{nameof(PartitionInformationEx)}{{" +
                    $" {nameof(PartitionStyle)}:{PartitionStyle}" +
                    $", {nameof(StartingOffset)}:{StartingOffset}" +
                    $", {nameof(PartitionLength)}:{PartitionLength}" +
                    $", {nameof(PartitionNumber)}:{PartitionNumber}" +
                    $", {nameof(RewritePartition)}:{RewritePartition}" +
                    $", " + (
                        PartitionStyle == PartitionStyle.Gpt ? $"{nameof(Gpt)}:{Gpt}" :
                        PartitionStyle == PartitionStyle.Mbr ? $"{nameof(Mbr)}:{Mbr}" : "null") +
                    $"}}";
            }
        }
        [StructLayout(LayoutKind.Explicit, Pack = 4)]
        public struct PartitionInformationUnion
        {
            [FieldOffset(0)]
            public PartitionInformationGpt Gpt;
            [FieldOffset(0)]
            public PartitionInformationMbr Mbr;
        }
        [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 1)]
        public struct PartitionInformationMbr
        {
            /// <summary>
            /// The type of partition. For a list of values, see Disk Partition Types.
            /// </summary>
            [MarshalAs(UnmanagedType.U1)]
            public PartitionType PartitionType;

            /// <summary>
            /// If this member is TRUE, the partition is bootable.
            /// </summary>
            [MarshalAs(UnmanagedType.I1)]
            public bool BootIndicator;

            /// <summary>
            /// If this member is TRUE, the partition is of a recognized type.
            /// </summary>
            [MarshalAs(UnmanagedType.I1)]
            public bool RecognizedPartition;

            /// <summary>
            /// The number of hidden sectors in the partition.
            /// </summary>
            public uint HiddenSectors;
            public override string ToString() => $"{nameof(PartitionInformationMbr)}{{" +
                $" {nameof(PartitionType)}:{PartitionType}" +
                $", {nameof(BootIndicator)}:{BootIndicator}" +
                $", {nameof(RecognizedPartition)}:{RecognizedPartition}" +
                $", {nameof(HiddenSectors)}:{HiddenSectors}" +
                $"}}";
        }


        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 4)]
        public struct PartitionInformationGpt
        {
            [FieldOffset(0)]
            public Guid PartitionType;
            [FieldOffset(16)]
            public Guid PartitionId;
            [FieldOffset(32)]
            [MarshalAs(UnmanagedType.U8)]
            public EFIPartitionAttributes Attributes;
            [FieldOffset(40)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
            public string Name;
            public override string ToString() => $"{nameof(PartitionInformationGpt)}:{{" +
                $" {nameof(PartitionType)}:{PartitionType}" +
                $", {nameof(PartitionId)}:{PartitionId}" +
                $", {nameof(Attributes)}:{Attributes}" +
                $", {nameof(Name)}:{Name}" +
                $"}}";
        }
        public enum PartitionType : byte
        {
            EntryUnused = 0x00, // Entry unused
            Fat12 = 0x01, // 12-bit FAT entries
            Xenix1 = 0x02, // Xenix
            Xenix2 = 0x03, // Xenix
            Fat16 = 0x04, // 16-bit FAT entries
            Extended = 0x05, // Extended partition entry
            Huge = 0x06, // Huge partition MS-DOS V4
            Ifs = 0x07, // IFS Partition
            OS2BOOTMGR = 0x0A, // OS/2 Boot Manager/OPUS/Coherent swap
            Fat32 = 0x0B, // FAT32
            Fat32Xint13 = 0x0C, // FAT32 using extended int13 services
            Xint13 = 0x0E, // Win95 partition using extended int13 services
            Xint13Extend = 0x0F, // Same as type 5 but uses extended int13 services
            Prep = 0x41, // PowerPC Reference Platform (PReP) Boot Partition
            Ldm = 0x42, // Logical Disk Manager partition
            Unix = 0x63, // Unix
            ValidNtft = 0xC0, // NTFT uses high order bits
            Ntft = 0x80,  // NTFT partition
            LinuxSwap = 0x82, //An ext2/ext3/ext4 swap partition
            LinuxNative = 0x83 //An ext2/ext3/ext4 native partition
        }
        [Flags]
        public enum EFIPartitionAttributes : ulong
        {
            GetAttributePlatformRequired = 0x0000_0000_0000_0001,
            LegacyBIOSBootale = 0x0000_0000_0000_0004,
            GptBasicDataAttributeNoDriveLetter = 0x8000_0000_0000_0000,
            GetBasicDataAttributeHidden = 0x4000_0000_0000_0000,
            GetBasicDataAttributeShadowCopy = 0x2000_0000_0000_0000,
            GetBasicDataAttributeReadOnly = 0x1000_0000_0000_0000,
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
