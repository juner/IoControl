using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoControl;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace IoControlTests
{
    [TestClass]
    public class IoControlTest
    {
        [TestMethod]
        public void SampleTest()
        {
            try
            {
                foreach (var Number in Enumerable.Range(0, 10))
                {
                    var PhysicalDriveName = $@"\\.\PhysicalDrive{Number}";
                    Trace.Write(PhysicalDriveName);
                    using (var file = IoControl.IoControl.CreateFile(PhysicalDriveName,
                        FileAccess: FileAccess.ReadWrite,
                        FileShare: FileShare.ReadWrite,
                        CreationDisposition: FileMode.Open,
                        FlagsAndAttributes: FileAttributes.Normal))
                    {
                        Trace.WriteLine("..." + (file.IsInvalid ? "NG." : "OK."));
                        if (file.IsInvalid)
                            continue;
                        try
                        {
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.StorageGetDeviceNumber));
                            var result = IoControl.IoControl.DeviceIoControlOutOnly(file, IoControl.IoControl.IOControlCode.StorageGetDeviceNumber, out StorageDeviceNumber number, out var _);
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
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.VolumeGetVolumeDiskExtents));
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
                            var result = IoControl.IoControl.DeviceIoControlOutOnly(file, IoControl.IoControl.IOControlCode.VolumeGetVolumeDiskExtents, out VolumeDiskExtent extent, out var ReturnBytes);
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
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.DiskGetDriveGeometryEx));
                            var result = IoControl.IoControl.DeviceIoControlOutOnly(file, IoControl.IoControl.IOControlCode.DiskGetDriveGeometryEx, out DiskGeometryEx geometry, out var _);
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
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.DiskGetLengthInfo));
                            var result = IoControl.IoControl.DeviceIoControlOutOnly(file, IoControl.IoControl.IOControlCode.DiskGetLengthInfo, out long disksize, out var _);
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
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.StorageQueryProperty));
                            var query = new StoragePropertyQuery
                            {
                                PropertyId = StoragePropertyId.StorageDeviceSeekPenaltyProperty,
                                QueryType = default,
                            };
                            var result = IoControl.IoControl.DeviceIoControl(file, IoControl.IoControl.IOControlCode.StorageQueryProperty, ref query, out DeviceSeekPenaltyDescriptor dest, out var penalty_size, IntPtr.Zero);
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
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.AtaPassThrough));
                            var Length = (ushort)Marshal.SizeOf(typeof(AtaPassThroughEx));
                            var id_query = new ATAIdentifyDeviceQuery
                            {
                                Header = new AtaPassThroughEx
                                {
                                    Length = Length,
                                    AtaFlags = AtaFlags.DataIn,
                                    DataTransferLength = (uint)256 * 2,
                                    TimeOutValue = 3,
                                    DataBufferOffset = Marshal.OffsetOf(typeof(ATAIdentifyDeviceQuery), nameof(ATAIdentifyDeviceQuery.Data)),
                                    PreviousTaskFile = new byte[8],
                                    CurrentTaskFile = new byte[8],
                                },
                                Data = new ushort[256],
                            };
                            id_query.Header.CurrentTaskFile[6] = 0xEC;
                            var result = IoControl.IoControl.DeviceIoControl(file, IoControl.IoControl.IOControlCode.AtaPassThrough, ref id_query, out var retval_size);
                            if (!result)
                                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                            Trace.WriteLine(id_query);

                        }
                        catch (Exception e2)
                        {
                            Trace.WriteLine(e2);
                        }
                        //try
                        //{
                        //    Trace.WriteLine(nameof(IoCtrl.EIOControlCode.DiskControllerNumber));
                        //    var result = IoCtrl.DeviceIoControlOutOnly(file, IoCtrl.EIOControlCode.DiskControllerNumber, out DiskControllerNumber number);
                        //    if (!result)
                        //        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                        //    Trace.WriteLine(number);
                        //}
                        //catch (Exception e2)
                        //{
                        //    Trace.WriteLine(e2);
                        //}
                        //try
                        //{
                        //    Trace.WriteLine(nameof(IoCtrl.EIOControlCode.DiskControllerNumber) + "2");
                        //    var bytes = new byte[256];
                        //    var result = IoCtrl.DeviceIoControlOutOnly(file, IoCtrl.EIOControlCode.DiskControllerNumber, bytes,out var outsize);
                        //    if (!result)
                        //        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                        //    Trace.WriteLine(string.Join(" ",(bytes ?? Enumerable.Empty<byte>()).Take((int)outsize).Select(v => $"{v:X2}")));
                        //}
                        //catch (Exception e2)
                        //{
                        //    Trace.WriteLine(e2);
                        //}
                        try
                        {
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.DiskPerformance));
                            var result = IoControl.IoControl.DeviceIoControlOutOnly(file, IoControl.IoControl.IOControlCode.DiskPerformance, out DiskPerformance performance, out var _);
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
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.ScsiGetAddress));
                            var result = IoControl.IoControl.DeviceIoControlOutOnly(file, IoControl.IoControl.IOControlCode.ScsiGetAddress, out ScsiAddress address, out var _);
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
                            Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.ScsiGetInquiryData));
                            var Size = (uint)(Marshal.SizeOf(typeof(_ScsiAdapterBusInfo)) + Marshal.SizeOf(typeof(ScsiBusData)) + Marshal.SizeOf(typeof(ScsiInquiryData)));
                            var OutPtr = Marshal.AllocCoTaskMem((int)Size);
                            using (Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
                            {
                                (var result, var returnBytes) = IoControl.IoControl.DeviceIoControlOutOnly(file, IoControl.IoControl.IOControlCode.ScsiGetInquiryData, OutPtr, Size);
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
                try
                {
                    var PathName = @"\\.\C:";//Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
                    Trace.WriteLine($"File... {PathName}");
                    using (var file = IoControl.IoControl.CreateFile(PathName, FileShare: FileShare.ReadWrite, CreationDisposition: FileMode.Open))
                    {
                        if (file.IsInvalid)
                            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                        Trace.WriteLine(nameof(IoControl.IoControl.IOControlCode.StorageGetDeviceNumber));
                        var result = IoControl.IoControl.DeviceIoControlOutOnly(file, IoControl.IoControl.IOControlCode.StorageGetDeviceNumber, out StorageDeviceNumber number, out var _);
                        if (!result)
                            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                        Trace.WriteLine(number);

                    }
                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
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
            public IoControl.IoControl.FileDevice DeviceType;
            public uint DeviceNumber;
            public uint PartitionNumber;
            public override string ToString()
                => $"{nameof(StorageDeviceNumber)}{{{nameof(DeviceType)}:{DeviceType}, {nameof(DeviceNumber)}:{DeviceNumber}, {nameof(PartitionNumber)}:{PartitionNumber}}}";
        }
        [StructLayout(LayoutKind.Sequential)]
        struct DiskControllerNumber
        {
            public IoControl.IoControl.FileDevice DeviceType;
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
