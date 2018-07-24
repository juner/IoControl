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
        public static void AtaPassThrough(this IoControl IoControl, ref AtaIdentifyDeviceQuery id_query)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.AtaPassThrough, ref id_query, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static void AtaPassThroughIdentifyDevice(this IoControl IoControl, out AtaIdentifyDevice identity)
        {
            var Length = (ushort)Marshal.SizeOf(typeof(AtaPassThroughEx));
            var id_query = new AtaIdentifyDeviceQuery {
                Header = new AtaPassThroughEx {
                    Length = Length,
                    AtaFlags = AtaFlags.DataIn | AtaFlags.NoMultiple,
                    DataTransferLength = (uint)256 * sizeof(ushort),
                    TimeOutValue = 3,
                    DataBufferOffset = Marshal.OffsetOf(typeof(AtaIdentifyDeviceQuery), nameof(AtaIdentifyDeviceQuery.Data)),
                    PreviousTaskFile = new byte[8],
                    CurrentTaskFile = new byte[8] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xEc, 0x0 },
                },
                Data = new ushort[256],
            };

            AtaPassThrough(IoControl, ref id_query);

            var identifyDeviceSize = Marshal.SizeOf<AtaIdentifyDevice>();
            var dataSize = id_query.Data.Length * sizeof(ushort);
            System.Diagnostics.Debug.Assert(dataSize >= identifyDeviceSize, $"サイズ不一致 {dataSize} < {identifyDeviceSize}");
            var outHandle = GCHandle.Alloc(id_query.Data, GCHandleType.Pinned);
            using (Disposable.Create(outHandle.Free))
            {
                var outPtr = outHandle.AddrOfPinnedObject();
                identity = (AtaIdentifyDevice)Marshal.PtrToStructure(outPtr, typeof(AtaIdentifyDevice));
            }
        }
        public static void AtaPassThroughSmartAttributes(this IoControl IoControl, out SmartAttribute[] attributes)
        {
            var Length = (ushort)Marshal.SizeOf(typeof(AtaPassThroughEx));
            var id_query = new AtaIdentifyDeviceQuery {
                Header = new AtaPassThroughEx {
                    Length = Length,
                    AtaFlags = AtaFlags.DataIn | AtaFlags.NoMultiple,
                    DataTransferLength = (uint)256 * sizeof(ushort),
                    TimeOutValue = 3,
                    DataBufferOffset = Marshal.OffsetOf(typeof(AtaIdentifyDeviceQuery), nameof(AtaIdentifyDeviceQuery.Data)),
                    PreviousTaskFile = new byte[8],
                    CurrentTaskFile = new byte[8] { 0xd0, 0x0, 0x0, 0x4f, 0xc2, 0x0, 0xb0, 0x0 },
                },
                Data = new ushort[256],
            };

            AtaPassThrough(IoControl, ref id_query);

            var SmartAttributeSize = Marshal.SizeOf<SmartAttribute>();
            const int ATTRIBUTE_MAX = 30;
            var attributesSize = SmartAttributeSize * ATTRIBUTE_MAX;
            var dataSize = id_query.Data.Length * sizeof(ushort);
            System.Diagnostics.Debug.Assert(dataSize >= attributesSize, $"サイズ不一致 {dataSize} < {attributesSize}");
            var outHandle = GCHandle.Alloc(id_query.Data, GCHandleType.Pinned);
            using (Disposable.Create(outHandle.Free))
            {
                var outPtr = IntPtr.Add(outHandle.AddrOfPinnedObject(), sizeof(ushort));
                attributes = Enumerable.Range(0, ATTRIBUTE_MAX)
                    .Select(index => (SmartAttribute)Marshal.PtrToStructure(IntPtr.Add(outPtr, SmartAttributeSize * index), typeof(SmartAttribute)))
                    .ToArray();
            }
            System.Diagnostics.Debug.WriteLine($"[{string.Join(" ", (id_query.Data ?? Enumerable.Empty<ushort>()).SelectMany(BitConverter.GetBytes).Select(v => $"{v:X2}"))}]");
            return;
        }
    }

}
