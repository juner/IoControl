using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
        public static void ScsiGetAddress(this IoControl IoControl, out ScsiAddress address)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.ScsiGetAddress, out address, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_SCSI_GET_ADDRESS IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_scsi_get_address )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static ScsiAddress ScsiGetAddress(this IoControl IoControl)
        {
            ScsiGetAddress(IoControl, out var address);
            return address;
        }
        /// <summary>
        /// IOCTL_ATA_PASS_THROUGH IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ni-ntddscsi-ioctl_ata_pass_through )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="id_query"></param>
        public static void AtaPassThrough(this IoControl IoControl,out AtaPassThroughEx Header, out byte[] Data, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, uint DataSize = default)
        {
            var _Size = Marshal.SizeOf<AtaPassThroughEx>();
            var DataTransferLength = DataSize;
            var DataBufferOffset =DataSize == 0 ? IntPtr.Zero : new IntPtr(_Size);
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
                if (!result)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                Header = (AtaPassThroughEx)Marshal.PtrToStructure(Ptr, typeof(AtaPassThroughEx));
                if (DataSize > 0)
                {
                    Data = new byte[DataSize];
                    Marshal.Copy(IntPtr.Add(Ptr, _Size), Data, 0, Data.Length);
                }
                else
                {
                    Data = null;
                }
            }
        }
        public static (AtaPassThroughEx Header, byte[] Data) AtaPassThrough(this IoControl IoControl, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, uint DataSize = default)
        {
            AtaPassThrough(IoControl,
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
                DataSize: DataSize);
            return (Header, Data);
        }
        public static void AtaPassThrough<T>(this IoControl IoControl, out AtaPassThroughEx Header, out T Data, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            where T : struct
        {
            var DataTransferLength = (uint)Marshal.SizeOf<T>();
            var DataBufferOffset = new IntPtr(Marshal.SizeOf<AtaPassThroughEx>());

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
                var result = IoControl.DeviceIoControl(IOControlCode.AtaPassThrough, Ptr, Size, out var _);
                if (!result)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                Header = (AtaPassThroughEx)Marshal.PtrToStructure(Ptr, typeof(AtaPassThroughEx));
                Data = (T)Marshal.PtrToStructure(IntPtr.Add(Ptr, Marshal.SizeOf<AtaPassThroughEx>()), typeof(T));
            }
        }
        public static (AtaPassThroughEx Header, T Data) AtaPassThrough<T>(this IoControl IoControl, AtaFlags AtaFlags, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            where T: struct
        {
            AtaPassThrough<T>(IoControl,
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
                Reserved: Reserved);
            return (Header, Data);
        }
        public static void AtaPassThroughIdentifyDevice(this IoControl IoControl, out AtaPassThroughEx Header,out AtaIdentifyDevice Data)
            => AtaPassThrough(
                IoControl: IoControl,
                Header: out Header,
                Data: out Data,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Command: 0xEC
            );
        public static (AtaPassThroughEx Header, AtaIdentifyDevice Data) AtaPassThroughIdentifyDevice(this IoControl IoControl)
            => AtaPassThrough<AtaIdentifyDevice>(
                IoControl: IoControl,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Command: 0xEC
            );
        public static void AtaPassThroughSmartAttributes(this IoControl IoControl, out AtaPassThroughEx Header, out SmartAttribute[] Data)
        {
            AtaPassThrough<SmartAttributes>(
                IoControl: IoControl,
                Header: out Header,
                Data: out var Data_,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Feature: 0xd0,
                Cylinder: 0xc24f,
                Command: 0xb0
            );
            Data = Data_;
        }
        public static (AtaPassThroughEx Header, SmartAttribute[] Data) AtaPassThroughSmartAttributes(this IoControl IoControl)
            => AtaPassThrough<SmartAttributes>(
                IoControl: IoControl,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Feature: 0xd0,
                Cylinder: 0xc24f,
                Command: 0xb0
            );
    }

}
