using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    public static class ControllerExtentions
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
        public static bool AtaPassThrough(this IoControl IoControl,out AtaPassThroughEx Header, out byte[] Data, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, uint DataSize = default)
        {
            var _Size = Marshal.SizeOf<AtaPassThroughEx>();
            var DataTransferLength = DataSize;
            var DataBufferOffset =DataSize == 0 ? 0 : _Size;
            Header = new AtaPassThroughEx(
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
                );
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
        public static (AtaPassThroughEx Header, byte[] Data) AtaPassThrough(this IoControl IoControl, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, uint DataSize = default)
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
        /// <returns></returns>
        public static bool AtaPassThrough<T>(this IoControl IoControl, out AtaPassThroughEx Header, out T Data, out uint ReturnBytes, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            where T : struct
        {
            var DataTransferLength = (uint)Marshal.SizeOf<T>();
            var DataBufferOffset = Marshal.SizeOf<AtaPassThroughEx>();

            Header = new AtaPassThroughEx(
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
                );
            var Size = (uint)(Marshal.SizeOf<AtaPassThroughEx>() + Marshal.SizeOf<T>());
            var Ptr = Marshal.AllocCoTaskMem((int)Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.StructureToPtr(Header, Ptr, false);
                var result = IoControl.DeviceIoControl(IOControlCode.AtaPassThrough, Ptr, Size, out ReturnBytes);
                Header = result ? (AtaPassThroughEx)Marshal.PtrToStructure(Ptr, typeof(AtaPassThroughEx)) : default;
                Data = result ? (T)Marshal.PtrToStructure(IntPtr.Add(Ptr, Marshal.SizeOf<AtaPassThroughEx>()), typeof(T)) : default;
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        /// <returns></returns>
        public static (AtaPassThroughEx Header, T Data) AtaPassThrough<T>(this IoControl IoControl, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            where T: struct
        {
            if (!AtaPassThrough<T>(IoControl,
                Header: out var Header,
                Data: out var Data,
                ReturnBytes: out var ReturnBytes,
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
                Reserved: Reserved) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return (Header, Data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Header"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static bool AtaPassThroughIdentifyDevice(this IoControl IoControl, out AtaPassThroughEx Header,out AtaIdentifyDevice Data, out uint ReturnBytes)
            => AtaPassThrough(
                IoControl: IoControl,
                Header: out Header,
                Data: out Data,
                ReturnBytes: out ReturnBytes,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Command: 0xEC
            );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static (AtaPassThroughEx Header, AtaIdentifyDevice Data) AtaPassThroughIdentifyDevice(this IoControl IoControl)
            => AtaPassThrough<AtaIdentifyDevice>(
                IoControl: IoControl,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Command: 0xEC
            );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Header"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static bool AtaPassThroughSmartAttributes(this IoControl IoControl, out AtaPassThroughEx Header, out SmartAttribute[] Data, out uint ReturnBytes)
        {
            var result = AtaPassThrough<SmartAttributes>(
                IoControl: IoControl,
                Header: out Header,
                Data: out var Data_,
                ReturnBytes: out ReturnBytes,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Feature: 0xd0,
                Cylinder: 0xc24f,
                Command: 0xb0
            );
            Data = Data_;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static (AtaPassThroughEx Header, SmartAttribute[] Data) AtaPassThroughSmartAttributes(this IoControl IoControl)
            => AtaPassThrough<SmartAttributes>(
                IoControl: IoControl,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Feature: 0xd0,
                Cylinder: 0xc24f,
                Command: 0xb0
            );
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
    }
}
