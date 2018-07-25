using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IoControl.Controller
{
    public static class ControllerExtentions
    {
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
        private static void AtaPassThrough(this IoControl IoControl, ref AtaIdentifyDeviceQuery id_query)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.AtaPassThrough, ref id_query, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static (AtaPassThroughEx Header, ushort[] Data) AtaPassThrough(this IoControl IoControl, AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, ushort DeviceHead = default, ushort Command = default, ushort Reserved = default)
        {
            var id_query = new AtaIdentifyDeviceQuery(
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
                    Data: new ushort[256]
                );
            AtaPassThrough(IoControl, ref id_query);
            return (id_query.Header, id_query.Data);
        }

        public static (AtaPassThroughEx Header, T Data) AtaPassThrough<T>(this IoControl IoControl, AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, ushort DeviceHead = default, ushort Command = default, ushort Reserved = default)
            where T: struct
        {
            var id_query = AtaPassThrough(
                    IoControl: IoControl,
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
                    Reserved: Reserved
                );
            var identifyDeviceSize = Marshal.SizeOf<T>();
            var dataSize = id_query.Data.Length * sizeof(ushort);
            System.Diagnostics.Debug.Assert(dataSize >= identifyDeviceSize, $"サイズ不一致 {dataSize} < {identifyDeviceSize}");
            var outHandle = GCHandle.Alloc(id_query.Data, GCHandleType.Pinned);
            using (Disposable.Create(outHandle.Free))
            {
                var outPtr = outHandle.AddrOfPinnedObject();
                return (id_query.Header, (T)Marshal.PtrToStructure(outPtr, typeof(T)));
            }
        }
        public static (AtaPassThroughEx Header, AtaIdentifyDevice Data) AtaPassThroughIdentifyDevice(this IoControl IoControl)
            => AtaPassThrough<AtaIdentifyDevice>(
                IoControl: IoControl,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Command: 0xEC
            );
        public static (AtaPassThroughEx Header, SmartAttribute[] Data) AtaPassThroughSmartAttributes(this IoControl IoControl)
        {
            var result = AtaPassThrough<SmartAttributes>(
                IoControl: IoControl,
                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                TimeOutValue: 3,
                Feature: 0xd0,
                Cylinder: 0xc24f,
                Command: 0xb0
            );
            return (result.Header, result.Data.Attributes);
        }
    }

}
