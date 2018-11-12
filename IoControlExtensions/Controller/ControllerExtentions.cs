using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IoControl.DataUtils;

namespace IoControl.Controller
{
    public static partial class ControllerExtentions
    {
        private class NativeMethods
        {

        }
        /// <summary>
        /// IOCTL_SCSI_GET_ADDRESS IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_scsi_get_address )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="address"></param>
        public static bool ScsiGetAddress(this IoControl IoControl, out ScsiAddress address) => IoControl.DeviceIoControlOutOnly(IOControlCode.ScsiGetAddress, out address, out var _);
        /// <summary>
        /// IOCTL_SCSI_GET_ADDRESS IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_scsi_get_address )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static ScsiAddress ScsiGetAddress(this IoControl IoControl)
        {
            if(!ScsiGetAddress(IoControl, out var address))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return address;
        }
        /// <summary>
        /// IOCTL_SCSI_GET_INQUIRY_DATA IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_scsi_get_inquiry_data )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool ScsiGetInquiryData(this IoControl IoControl, out ScsiAdapterBusInfo info)
        {
            var data = new DataUtils.AnySizeStruct<ScsiAdapterBusInfo>();
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.ScsiGetInquiryData, data, out var ReturnBytes);
            info = data.Get();
            return result;
        }
        /// <summary>
        /// IOCTL_SCSI_GET_INQUIRY_DATA IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_scsi_get_inquiry_data )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static ScsiAdapterBusInfo ScsiGetInquiryData(this IoControl IoControl)
        {
            if (!IoControl.ScsiGetInquiryData(out var info))
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return info;
        }
        /// <summary>
        /// IOCTL_ATA_PASS_THROUGH IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_ata_pass_through )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Header"></param>
        /// <param name="Data"></param>
        /// <param name="AtaFlags"></param>
        /// <param name="PathId"></param>
        /// <param name="TargetId"></param>
        /// <param name="Lun"></param>
        /// <param name="ReservedAsUchar"></param>
        /// <param name="TimeOutValue"></param>
        /// <param name="ReservedAsUlong"></param>
        /// <param name="Feature"></param>
        /// <param name="SectorCouont"></param>
        /// <param name="SectorNumber"></param>
        /// <param name="Cylinder"></param>
        /// <param name="DeviceHead"></param>
        /// <param name="Command"></param>
        /// <param name="Reserved"></param>
        /// <param name="DataSize"></param>
        /// <returns></returns>
        public static bool AtaPassThrough(this IoControl IoControl, out IAtaPassThroughEx Header, out byte[] Data, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, uint DataSize = default)
        {
            var _Size = Marshal.SizeOf<AtaPassThroughEx>();
            var DataTransferLength = DataSize;
            var DataBufferOffset = DataSize == 0 ? 0 : _Size;
            Header = 
                IntPtr.Size == 8 ? (IAtaPassThroughEx)new AtaPassThroughEx(
                    AtaFlags: AtaFlags,
                    PathId: PathId,
                    TargetId: TargetId,
                    Lun: Lun,
                    ReservedAsUchar: ReservedAsUchar,
                    TimeOutValue: TimeOutValue,
                    ReservedAsUlong: ReservedAsUlong,
                    Feature: Feature,
                    SectorCouont: SectorCouont,
                    SectorNumber: SectorNumber,
                    Cylinder: Cylinder,
                    DeviceHead: DeviceHead,
                    Command: Command,
                    Reserved: Reserved,
                    DataTransferLength: DataTransferLength,
                    DataBufferOffset: DataBufferOffset
                ): IntPtr.Size == 4 ? new AtaPassThroughEx32(
                    AtaFlags: AtaFlags,
                    PathId: PathId,
                    TargetId: TargetId,
                    Lun: Lun,
                    ReservedAsUchar: ReservedAsUchar,
                    TimeOutValue: TimeOutValue,
                    ReservedAsUlong: ReservedAsUlong,
                    Feature: Feature,
                    SectorCouont: SectorCouont,
                    SectorNumber: SectorNumber,
                    Cylinder: Cylinder,
                    DeviceHead: DeviceHead,
                    Command: Command,
                    Reserved: Reserved,
                    DataTransferLength: DataTransferLength,
                    DataBufferOffset: DataBufferOffset
                ): throw new NotSupportedException();
            var Size = (uint)(_Size + DataSize);
            var Ptr = Marshal.AllocCoTaskMem((int)Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.StructureToPtr(Header, Ptr, false);
                var result = IoControl.DeviceIoControl(IOControlCode.AtaPassThrough, Ptr, Size, out var _);
                Header = result ? (AtaPassThroughEx)Marshal.PtrToStructure(Ptr, typeof(AtaPassThroughEx)) : default;
                if (DataSize > 0)
                {
                    Data = new byte[DataSize];
                    Marshal.Copy(IntPtr.Add(Ptr, _Size), Data, 0, Data.Length);
                }
                else
                {
                    Data = default;
                }
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="AtaFlags"></param>
        /// <param name="PathId"></param>
        /// <param name="TargetId"></param>
        /// <param name="Lun"></param>
        /// <param name="ReservedAsUchar"></param>
        /// <param name="TimeOutValue"></param>
        /// <param name="ReservedAsUlong"></param>
        /// <param name="Feature"></param>
        /// <param name="SectorCouont"></param>
        /// <param name="SectorNumber"></param>
        /// <param name="Cylinder"></param>
        /// <param name="DeviceHead"></param>
        /// <param name="Command"></param>
        /// <param name="Reserved"></param>
        /// <param name="DataSize"></param>
        /// <returns></returns>
        public static (IAtaPassThroughEx Header, byte[] Data) AtaPassThrough(this IoControl IoControl, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, uint DataSize = default)
        {
            if (!AtaPassThrough(IoControl,
                Header: out var Header,
                Data: out var Data,
                AtaFlags: AtaFlags,
                PathId: PathId,
                TargetId: TargetId,
                Lun: Lun,
                ReservedAsUchar: ReservedAsUchar,
                TimeOutValue: TimeOutValue,
                ReservedAsUlong: ReservedAsUlong,
                Feature: Feature,
                SectorCouont: SectorCouont,
                SectorNumber: SectorNumber,
                Cylinder: Cylinder,
                DeviceHead: DeviceHead,
                Command: Command,
                Reserved: Reserved,
                DataSize: DataSize))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return (Header, Data);
        }
        /// <summary>
        /// IOCTL_ATA_PASS_THROUGH IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_ata_pass_through )
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="IoControl"></param>
        /// <param name="InOutBuffer"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public static bool AtaPassThrough<T>(this IoControl IoControl, StructPtr<T> InOutBuffer, out uint ReturnBytes)
            where T : struct, IAtaPassThroughEx
            => IoControl.DeviceIoControl(IOControlCode.AtaPassThrough, InOutBuffer, out ReturnBytes);
        /// <summary>
        /// IOCTL_ATA_PASS_THROUGH IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_ata_pass_through )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Header"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static bool AtaPassThroughIdentifyDevice(this IoControl IoControl, out IAtaPassThroughEx<IdentifyDevice> value, out uint ReturnBytes)
        {
            if (IntPtr.Size == 4)
            {
                var ptr = new StructPtr<AtaPassThroughEx32WithIdentifyDevice>(new AtaPassThroughEx32WithIdentifyDevice(
                    AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                    TimeOutValue: 3,
                    Feature: 0,
                    Cylinder: 0,
                    Command: 0xEC
                ));
                var result = IoControl.AtaPassThrough(ptr, out ReturnBytes);
                value = ptr.Get();
                return result;
            }
            else if (IntPtr.Size == 8)
            {
                var ptr = new StructPtr<AtaPassThroughExWithIdentifyDevice>(new AtaPassThroughExWithIdentifyDevice(
                    AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                    TimeOutValue: 3,
                    Feature: 0,
                    Cylinder: 0,
                    Command: 0xEC
                ));
                var result = IoControl.AtaPassThrough(ptr, out ReturnBytes);
                value = ptr.Get();
                return result;
            }
            throw new NotSupportedException();
        }
        /// <summary>
        /// IOCTL_ATA_PASS_THROUGH IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_ata_pass_through )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static IAtaPassThroughEx<IdentifyDevice> AtaPassThroughIdentifyDevice(this IoControl IoControl)
        {
            if (!IoControl.AtaPassThroughIdentifyDevice(out var result, out var ReturnBytes))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return result;
        }
        /// <summary>
        /// IOCTL_ATA_PASS_THROUGH IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_ata_pass_through )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Header"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static bool AtaPassThroughSmartData(this IoControl IoControl, out IAtaPassThroughEx<SmartData> value, out uint ReturnBytes)
        {
            if (IntPtr.Size == 4)
            {
                var request = new AtaPassThroughEx32WithSmartData(
                    AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                    TimeOutValue: 3,
                    Feature: 0xd0,
                    Cylinder: 0xc24f,
                    Command: 0xb0
                );
                System.Diagnostics.Debug.Assert(request.DataTransferLength == 512, "データが不正");
                var ptr = new StructPtr<AtaPassThroughEx32WithSmartData>(request);
                var result = IoControl.AtaPassThrough(ptr, out ReturnBytes);
                value = ptr.Get();
                return result;
            }
            else if (IntPtr.Size == 8)
            {
                var request = new AtaPassThroughExWithSmartData(
                    AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                    TimeOutValue: 3,
                    Feature: 0xd0,
                    Cylinder: 0xc24f,
                    Command: 0xb0
                );
                System.Diagnostics.Debug.Assert(request.DataTransferLength == 512, "データが不正");
                var ptr = new StructPtr<AtaPassThroughExWithSmartData>(request);
                var result = IoControl.AtaPassThrough(ptr, out ReturnBytes);
                value = ptr.Get();
                return result;
            }
            throw new NotSupportedException();
        }
        /// <summary>
        /// IOCTL_ATA_PASS_THROUGH IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_ata_pass_through )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>

        public static IAtaPassThroughEx<SmartData> AtaPassThroughSmartData(this IoControl IoControl)
        {
            if (!IoControl.AtaPassThroughSmartData(out var result, out var ReturnBytes))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return result;
        }
        public static bool AtaPassThroughCheckPowerMode(this IoControl IoControl, out IAtaPassThroughEx value, out uint ReturnBytes)
        {
            if (IntPtr.Size == 4)
            {
                var _value = new AtaPassThroughEx32(
                    AtaFlags: AtaFlags.DataOut,
                    TimeOutValue: 100,
                    DataTransferLength: 0,
                    DataBufferOffset: Marshal.SizeOf<AtaPassThroughEx>(),
                    Feature: 0,
                    Cylinder: 0,
                    DeviceHead: 0x10,
                    Command: 0xE5
                );
                var ptr = new StructPtr<AtaPassThroughEx32>(_value);
                var result = IoControl.AtaPassThrough(ptr, out ReturnBytes);
                value = ptr.Get();
                return result;
            } else if (IntPtr.Size == 8)
            {

                var _value = new AtaPassThroughEx(
                    AtaFlags: AtaFlags.DataOut,
                    TimeOutValue: 100,
                    DataTransferLength: 0,
                    DataBufferOffset: Marshal.SizeOf<AtaPassThroughEx>(),
                    Feature: 0,
                    Cylinder: 0,
                    DeviceHead: 0x10,
                    Command: 0xE5
                );
                var ptr = new StructPtr<AtaPassThroughEx>(_value);
                var result = IoControl.AtaPassThrough(ptr, out ReturnBytes);
                value = ptr.Get();
                return result;
            }
            throw new NotSupportedException();
        }
        public static IAtaPassThroughEx AtaPassThroughCheckPowerMode(this IoControl IoControl)
        {
            if (!IoControl.AtaPassThroughCheckPowerMode(out var result, out var ReturnBytes))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return result;

        }
        /// <summary>
        /// IOCTL_ATA_PASS_THROUGH_DIRECT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_ata_pass_through_direct )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Header"></param>
        /// <param name="AtaFlags"></param>
        /// <param name="PathId"></param>
        /// <param name="TargetId"></param>
        /// <param name="Lun"></param>
        /// <param name="ReservedAsUchar"></param>
        /// <param name="TimeOutValue"></param>
        /// <param name="ReservedAsUlong"></param>
        /// <param name="DataTransferLength"></param>
        /// <param name="DataBuffer"></param>
        /// <param name="Feature"></param>
        /// <param name="SectorCouont"></param>
        /// <param name="SectorNumber"></param>
        /// <param name="Cylinder"></param>
        /// <param name="DeviceHead"></param>
        /// <param name="Command"></param>
        /// <param name="Reserved"></param>
        /// <returns></returns>
        public static bool AtaPassThroughDirect(this IoControl IoControl, out AtaPassThroughDirect Header,AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, IntPtr DataBuffer = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
        {
            Header = new AtaPassThroughDirect(
                    AtaFlags: AtaFlags,
                    PathId: PathId,
                    TargetId: TargetId,
                    Lun: Lun,
                    ReservedAsUchar: ReservedAsUchar,
                    TimeOutValue: TimeOutValue,
                    ReservedAsUlong: ReservedAsUlong,
                    Feature: Feature,
                    SectorCouont: SectorCouont,
                    SectorNumber: SectorNumber,
                    Cylinder: Cylinder,
                    DeviceHead: DeviceHead,
                    Command: Command,
                    Reserved: Reserved,
                    DataTransferLength: DataTransferLength,
                    DataBuffer: DataBuffer
                );
            return IoControl.DeviceIoControl(IOControlCode.AtaPassThroughDirect, ref Header, out var _);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="AtaFlags"></param>
        /// <param name="PathId"></param>
        /// <param name="TargetId"></param>
        /// <param name="Lun"></param>
        /// <param name="ReservedAsUchar"></param>
        /// <param name="TimeOutValue"></param>
        /// <param name="ReservedAsUlong"></param>
        /// <param name="DataTransferLength"></param>
        /// <param name="DataBuffer"></param>
        /// <param name="Feature"></param>
        /// <param name="SectorCouont"></param>
        /// <param name="SectorNumber"></param>
        /// <param name="Cylinder"></param>
        /// <param name="DeviceHead"></param>
        /// <param name="Command"></param>
        /// <param name="Reserved"></param>
        /// <returns></returns>
        public static AtaPassThroughDirect AtaPassThroughDirect(this IoControl IoControl, AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, IntPtr DataBuffer = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
        {
            if (!AtaPassThroughDirect(IoControl,out var Header, AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBuffer, Feature, SectorCouont, SectorNumber, Cylinder, DeviceHead, Command, Reserved))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return Header;
        }
        internal const int IDENTIFY_BUFFER_SIZE = 512;
        [StructLayout(LayoutKind.Sequential, Pack =1)]
        internal readonly struct IdentifyDeviceOutData
        {
            public readonly Disk.Sendcmdoutparams Outparams;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDENTIFY_BUFFER_SIZE - 1)]
            public readonly byte[] Datas;
        }
        public static bool ScsiMiniportIdentify(this IoControl IoControl, byte TargetId, out IdentifyDevice IdentifyDevice)
        {
            const byte ID_CMD = 0xEC;
            var SrbIoControl = new SrbIoControl(
                Signagure: "SCSIDISK",
                ControlCode: IOControlCode.ScsiMiniportIdentify,
                Timeout: 2
            );
            var inparams = new Disk.Sendcmdinparams(
                DriveRegs: new Disk.Ideregs(
                    CommandReg: ID_CMD
                ),
                DriveNumber: TargetId
            );
            bool Result = IoControl.ScsiMiniport<Disk.Sendcmdinparams, IdentifyDeviceOutData>(ref SrbIoControl, inparams, out var outparams, out var ReturnBytes);
            var buffer = new byte[IDENTIFY_BUFFER_SIZE];
            using (PtrUtils.CreatePtr(outparams, out var IntPtr, out var Size))
            {
                IdentifyDevice = (IdentifyDevice)Marshal.PtrToStructure(IntPtr.Add(IntPtr,(int)Marshal.OffsetOf<Disk.Sendcmdoutparams>(nameof(Disk.Sendcmdoutparams._Buffer))),typeof(IdentifyDevice));
                return Result;
            }
        }
        public static bool ScsiMiniportIdentify(this IoControl IoControl, out IdentifyDevice IdentifyDevice)
            => IoControl.ScsiMiniportIdentify(IoControl.ScsiGetAddress().TargetId, out IdentifyDevice);
        public static IdentifyDevice ScsiMiniportIdentify(this IoControl IoControl, byte TargetId)
        {
            if (!IoControl.ScsiMiniportIdentify(TargetId, out var IdentifyDevice))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return IdentifyDevice;
        }
        public static IdentifyDevice ScsiMiniportIdentify(this IoControl IoControl)
            => IoControl.ScsiMiniportIdentify(IoControl.ScsiGetAddress().TargetId);
        /// <summary>
        /// IOCTL_SCSI_MINIPORT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_scsi_miniport )
        /// </summary>
        /// <typeparam name="IN"></typeparam>
        /// <typeparam name="OUT"></typeparam>
        /// <param name="IoControl"></param>
        /// <param name="SrbIoControl"></param>
        /// <param name="InOutStruct"></param>
        /// <param name="OutStruct"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public static bool ScsiMiniport<IN,OUT>(this IoControl IoControl, ref SrbIoControl SrbIoControl, in IN InOutStruct, out OUT OutStruct, out uint ReturnBytes)
            where IN : struct 
            where OUT : struct
        {
            int SrbIoControlSize = Marshal.SizeOf<SrbIoControl>();
            int InStructSize = Marshal.SizeOf<IN>();
            int OutStructSize = Marshal.SizeOf<OUT>();
            int MaxStructSize = Math.Max(InStructSize, OutStructSize);
            int Size = SrbIoControlSize + MaxStructSize;
            IntPtr IntPtr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(IntPtr)))
            {
                SrbIoControl = SrbIoControl.Set(
                    HeaderLength: (uint)SrbIoControlSize,
                    Timeout: 2,
                    Length: (uint)MaxStructSize
                );
                Marshal.StructureToPtr(SrbIoControl, IntPtr, false);
                Marshal.StructureToPtr(InOutStruct, IntPtr.Add(IntPtr, SrbIoControlSize), false);
                var result = IoControl.DeviceIoControl(IOControlCode.ScsiMiniport
                    , IntPtr, (uint)(SrbIoControlSize + InStructSize)
                    , IntPtr, (uint)(SrbIoControlSize + OutStructSize)
                    , out ReturnBytes);
                OutStruct = result
                    ? (OUT)Marshal.PtrToStructure(IntPtr.Add(IntPtr, SrbIoControlSize), typeof(OUT))
                    : default;
                return result;
            }
        }
        public static bool ScsiPassThrough<T>(this IoControl IoControl, StructPtr<T> inOutPtr, out uint ReturnBytes)
            where T : struct, IScsiPathThrough
            => IoControl.DeviceIoControl(IOControlCode.ScsiPassThrough, inOutPtr, out ReturnBytes);
        public static IdentifyDevice ScsiPassThroughIdentifyDevice(this IoControl IoControl, byte Target, CommandType type)
        {
            if (!IoControl.ScsiPassThroughIdentifyDevice(Target, out var IdentifyDevice, type))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return IdentifyDevice;
        }
        public static bool ScsiPassThroughIdentifyDevice(this IoControl IoControl, byte Target, out IdentifyDevice IdentifyDevice, CommandType type)
        {
            const byte ID_CMD = 0xEC;
            var Spt = new ScsiPassThrough(
                Length: (ushort)Marshal.SizeOf<ScsiPassThrough>(),
                PathId: 0,
                TargetId: 0,
                Lun: 0,
                SenseInfoLength: 24,
                DataIn: ScsiData.In,
                DataTransferLength: (uint)Marshal.SizeOf<IdentifyDevice>(),
                DataBufferOffset: Marshal.OffsetOf<ScsiPassThroughIdentifyDevice>(nameof(Controller.ScsiPassThroughIdentifyDevice.DataBuf)),
                SenseInfoOffset: (uint)Marshal.OffsetOf<ScsiPassThroughIdentifyDevice>(nameof(Controller.ScsiPassThroughIdentifyDevice.SenseBuf))
            );
            if (type == CommandType.CmdTypeSat)
            {
                var Cdb = new byte[] {
                    0xA1,//ATA PASS THROUGH(12) OPERATION CODE(A1h)
                    (4 << 1) | 0, //MULTIPLE_COUNT=0,PROTOCOL=4(PIO Data-In),Reserved
                    (1 << 3) | (1 << 2) | 2,//OFF_LINE=0,CK_COND=0,Reserved=0,T_DIR=1(ToDevice),BYTE_BLOCK=1,T_LENGTH=2
                    0,//FEATURES (7:0)
                    1,//SECTOR_COUNT (7:0)
                    0,//LBA_LOW (7:0)
                    0,//LBA_MID (7:0)
                    0,//LBA_HIGH (7:0)
                    Target,
                    ID_CMD//COMMAND
                };
                Spt = Spt.Set(
                    CdbLength: (byte)Cdb.Length,
                    Cdb: Cdb
                );
            }
            else
            if (type == CommandType.CmdTypeSunplus)
            {
                var Cdb = new byte[] {
                    0xF8,
                    0x00,
                    0x22,
                    0x10,
                    0x01,
                    0x00,
                    0x01,
                    0x00,
                    0x00,
                    0x00,
                    Target,
                    0xEC, // ID_CMD
                };
                Spt = Spt.Set(
                    CdbLength: (byte)Cdb.Length,
                    Cdb: Cdb
                );
            }
            else
            if (type == CommandType.CmdTypeIoData)
            {
                var Cdb = new byte[] {
                    0xE3,
                    0x00,
                    0x00,
                    0x01,
                    0x01,
                    0x00,
                    0x00,
                    Target,
                    0xEC,  // ID_CMD
                    0x00,
                    0x00,
                    0x00,
                };
                Spt = Spt.Set(
                    CdbLength: (byte)Cdb.Length,
                    Cdb: Cdb
                );
            }
            else throw new ArgumentException(nameof(type));
            var SpTwb = new ScsiPassThroughIdentifyDevice(
                Spt: Spt
            );
            var InOutPtr = new StructPtr<ScsiPassThroughIdentifyDevice>(SpTwb);
            var result = IoControl.ScsiPassThrough(InOutPtr, out var ReturnBytes);
            IdentifyDevice = result ? InOutPtr.Get().DataBuf : default;
            return result;
        }
    }
}
