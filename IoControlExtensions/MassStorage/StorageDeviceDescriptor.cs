using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// _STORAGE_DEVICE_DESCRIPTOR structure 
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_device_descriptor
    /// The <see cref="StorageDeviceDescriptor"/> structure is used in conjunction with the <see cref="IOControlCode.StorageQueryProperty"/> request to retrieve the storage device descriptor data for a device.
    /// </summary>
    /// <remarks>
    /// Applications and storage class drivers issue a device-control request with the I/O control code <see cref="IOControlCode.StorageQueryProperty"/> to retrieve this structure, which contains information about a target device. The structure can be retrieved only from an FDO; attempting to retrieve device properties from an adapter causes an error.
    /// An application or driver can determine the required buffer size by casting the retrieved <see cref="StorageDeviceDescriptor"/> structure to a <see cref="StorageDescriptorHeader"/>, which contains only <see cref="Version"/> and <see cref="Size"/>.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageDeviceDescriptor : IStorageDescriptor, DataUtils.IPtrCreatable
    {
        uint IStorageDescriptor.Version => Version;
        uint IStorageDescriptor.Size => Size;
        /// <summary>
        /// Indicates the size of the STORAGE_DEVICE_DESCRIPTOR structure. The value of this member will change as members are added to the structure.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Specifies the total size of the descriptor in bytes, including ID strings which are appended to the structure.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// Specifies the device type as defined by the Small Computer Systems Interface (SCSI) specification.
        /// </summary>
        public readonly Scsi.DeviceType DeviceType;
        /// <summary>
        /// Specifies the device type modifier, if any, as defined by the SCSI specification. If no device type modifier exists, this member is zero.
        /// </summary>
        public readonly byte DeviceTypeModifier;
        /// <summary>
        /// Indicates when TRUE that the device's media (if any) is removable. If the device has no media, this member should be ignored. When FALSE the device's media is not removable.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool RemovableMedia;
        /// <summary>
        /// Indicates when TRUE that the device supports multiple outstanding commands (SCSI tagged queuing or equivalent). When FALSE, the device does not support SCSI-tagged queuing or the equivalent. The STORPORT driver is responsible for synchronizing the commands.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool CommandQueueing;
        /// <summary>
        /// Specifies the byte offset from the beginning of the structure to a NULL-terminated ASCII string that contains the device's vendor ID. If the device has no vendor ID, this member is zero.
        /// </summary>
        public readonly uint VendorIdOffset;
        /// <summary>
        /// Specifies the byte offset from the beginning of the structure to a NULL-terminated ASCII string that contains the device's product ID. If the device has no product ID, this member is zero.
        /// </summary>
        public readonly uint ProductIdOffset;
        /// <summary>
        /// Specifies the byte offset from the beginning of the structure to a NULL-terminated ASCII string that contains the device's product revision string. If the device has no product revision string, this member is zero.
        /// </summary>
        public readonly uint ProductRevisionOffset;
        /// <summary>
        /// Specifies the byte offset from the beginning of the structure to a NULL-terminated ASCII string that contains the device's serial number. If the device has no serial number, this member is zero.
        /// </summary>
        public readonly uint SerialNumberOffset;
        /// <summary>
        /// Specifies an enumerator value of type STORAGE_BUS_TYPE that indicates the type of bus to which the device is connected. This should be used to interpret the raw device properties at the end of this structure (if any).
        /// </summary>
        public readonly StorageBusType BusType;
        /// <summary>
        /// Indicates the number of bytes of bus-specific data that have been appended to this descriptor.
        /// </summary>
        public readonly uint RawPropertiesLength;
        /// <summary>
        /// Contains an array of length one that serves as a place holder for the first byte of the bus specific property data.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        private readonly byte[] _RawDeviceProperties;
        public byte[] RawDeviceProperties => (_RawDeviceProperties ?? Enumerable.Empty<byte>()).Take((int)RawPropertiesLength).ToArray();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeviceType"></param>
        /// <param name="DeviceTypeModifier"></param>
        /// <param name="RemovableMedia"></param>
        /// <param name="CommandQueueing"></param>
        /// <param name="VendorIdOffset"></param>
        /// <param name="ProductIdOffset"></param>
        /// <param name="ProductRevisionOffset"></param>
        /// <param name="SerialNumberOffset"></param>
        /// <param name="BusType"></param>
        /// <param name="RawDeviceProperties"></param>
        public StorageDeviceDescriptor(Scsi.DeviceType DeviceType, byte DeviceTypeModifier, bool RemovableMedia, bool CommandQueueing, uint VendorIdOffset, uint ProductIdOffset, uint ProductRevisionOffset, uint SerialNumberOffset, StorageBusType BusType, byte[] RawDeviceProperties)
            => (Version, this.Size, this.DeviceType, this.DeviceTypeModifier, this.RemovableMedia, this.CommandQueueing, this.VendorIdOffset, this.ProductIdOffset, this.ProductRevisionOffset, this.SerialNumberOffset, this.BusType, RawPropertiesLength, this._RawDeviceProperties)
                = ((uint)Marshal.SizeOf<StorageDeviceDescriptor>(), (uint)(Marshal.SizeOf<StorageDeviceDescriptor>() + (RawDeviceProperties?.Length ?? 1) - 1), DeviceType, DeviceTypeModifier, RemovableMedia, CommandQueueing, VendorIdOffset, ProductIdOffset, ProductRevisionOffset, SerialNumberOffset, BusType, (uint)(RawDeviceProperties?.Length ?? 0), (RawDeviceProperties?.Length ?? 0) == 0 ? new byte[1] : RawDeviceProperties);
        /// <summary>
        /// Ptr to Structure
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public StorageDeviceDescriptor(IntPtr IntPtr, uint Size)
        {
            this = (StorageDeviceDescriptor)Marshal.PtrToStructure(IntPtr, typeof(StorageDeviceDescriptor));
            var _Size = Marshal.SizeOf<StorageDeviceDescriptor>();
            if (Size == (uint)_Size)
                return;
            /// <see cref="RawPropertiesLength"/> がアテにならないので実際のサイズから長さを求める
            var offset = (int)Marshal.OffsetOf<StorageDeviceDescriptor>(nameof(_RawDeviceProperties));
            _RawDeviceProperties = new byte[Size - offset];
            RawPropertiesLength = (uint)_RawDeviceProperties.Length;
            Marshal.Copy(IntPtr.Add(IntPtr, (int)Marshal.OffsetOf<StorageDeviceDescriptor>(nameof(_RawDeviceProperties))), _RawDeviceProperties, 0, _RawDeviceProperties.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="ProductId"></param>
        /// <param name="ProductRevision"></param>
        /// <param name="SerialNumber"></param>
        public void Deconstruct(out string VendorId, out string ProductId, out string ProductRevision, out string SerialNumber)
        {
            using (CreatePtr(out var Ptr, out var Size))
            {
                VendorId = VendorIdOffset == 0u ? default : Marshal.PtrToStringAnsi(IntPtr.Add(Ptr, (int)VendorIdOffset));
                ProductId = ProductIdOffset == 0u ? default : Marshal.PtrToStringAnsi(IntPtr.Add(Ptr, (int)ProductIdOffset));
                ProductRevision = ProductRevisionOffset == 0u ? default : Marshal.PtrToStringAnsi(IntPtr.Add(Ptr, (int)ProductRevisionOffset));
                SerialNumber = SerialNumberOffset == 0u ? default : Marshal.PtrToStringAnsi(IntPtr.Add(Ptr, (int)SerialNumberOffset));
            }
        }
        /// <summary>
        /// Structure to Ptr
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public IDisposable CreatePtr(out IntPtr IntPtr, out uint Size)
        {
            var Dispose = PtrUtils.CreatePtr(this.Size, out IntPtr);
            try
            {
                Marshal.StructureToPtr(this, IntPtr, false);
                var offset = (int)Marshal.OffsetOf<StorageDeviceDescriptor>(nameof(_RawDeviceProperties));
                Marshal.Copy(_RawDeviceProperties, 0, IntPtr.Add(IntPtr,offset), _RawDeviceProperties.Length);
                Size = this.Size;
                return Dispose;
            }
            catch
            {
                Dispose.Dispose();
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeviceType"></param>
        /// <param name="DeviceTypeModifier"></param>
        /// <param name="RemovableMedia"></param>
        /// <param name="CommandQueueing"></param>
        /// <param name="VendorIdOffset"></param>
        /// <param name="ProductIdOffset"></param>
        /// <param name="ProductRevisionOffset"></param>
        /// <param name="SerialNumberOffset"></param>
        /// <param name="BusType"></param>
        /// <param name="RawDeviceProperties"></param>
        /// <returns></returns>
        public StorageDeviceDescriptor Set(Scsi.DeviceType? DeviceType = null, byte? DeviceTypeModifier = null, bool? RemovableMedia = null, bool? CommandQueueing = null, uint? VendorIdOffset = null, uint? ProductIdOffset = null, uint? ProductRevisionOffset = null, uint? SerialNumberOffset = null, StorageBusType? BusType = null, byte[] RawDeviceProperties = null)
            => DeviceType == null && DeviceTypeModifier == null && RemovableMedia == null && CommandQueueing == null && VendorIdOffset == null && ProductIdOffset == null && ProductRevisionOffset == null && SerialNumberOffset == null && BusType == null && RawDeviceProperties == null
            ? this : new StorageDeviceDescriptor(DeviceType ?? this.DeviceType, DeviceTypeModifier ?? this.DeviceTypeModifier, RemovableMedia ?? this.RemovableMedia, CommandQueueing ?? this.CommandQueueing, VendorIdOffset ?? this.VendorIdOffset, ProductIdOffset ?? this.ProductIdOffset, ProductRevisionOffset ?? this.ProductRevisionOffset, SerialNumberOffset ?? this.SerialNumberOffset, BusType ?? this.BusType, RawDeviceProperties ?? this._RawDeviceProperties);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Size"></param>
        /// <param name="DeviceType"></param>
        /// <param name="DeviceTypeModifier"></param>
        /// <param name="RemovableMedia"></param>
        /// <param name="CommandQueueing"></param>
        /// <param name="VendorIdOffset"></param>
        /// <param name="ProductIdOffset"></param>
        /// <param name="ProductRevisionOffset"></param>
        /// <param name="SerialNumberOffset"></param>
        /// <param name="BusType"></param>
        /// <param name="RawPropertiesLength"></param>
        /// <param name="RawDeviceProperties"></param>
        public void Deconstruct(out uint Version, out uint Size, out Scsi.DeviceType DeviceType, out byte DeviceTypeModifier, out bool RemovableMedia, out bool CommandQueueing, out uint VendorIdOffset, out uint ProductIdOffset, out uint ProductRevisionOffset, out uint SerialNumberOffset, out StorageBusType BusType, out uint RawPropertiesLength, out byte[] RawDeviceProperties)
            => (Version, Size, DeviceType, DeviceTypeModifier, RemovableMedia, CommandQueueing, VendorIdOffset, ProductIdOffset, ProductRevisionOffset, SerialNumberOffset, BusType, RawPropertiesLength, RawDeviceProperties)
            = (this.Version, this.Size, this.DeviceType, this.DeviceTypeModifier, this.RemovableMedia, this.CommandQueueing, this.VendorIdOffset, this.ProductIdOffset, this.ProductRevisionOffset, this.SerialNumberOffset, this.BusType, this.RawPropertiesLength, this._RawDeviceProperties);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var (VendorId, ProductId, ProductRevision, SerialNumber) = this;
            return $"{nameof(StorageDeviceDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(DeviceType)}:{DeviceType}, {nameof(DeviceTypeModifier)}:{DeviceTypeModifier}, {nameof(RemovableMedia)}:{RemovableMedia}, {nameof(CommandQueueing)}:{CommandQueueing}, {nameof(VendorIdOffset)}:{VendorIdOffset}{(VendorIdOffset == 0 ? "" : $", {nameof(VendorId)}:{VendorId}")}, {nameof(ProductIdOffset)}:{ProductIdOffset}{(ProductIdOffset == 0 ? "" : $", {nameof(ProductId)}:{ProductId}")}, {nameof(ProductRevisionOffset)}:{ProductRevisionOffset}{(ProductRevisionOffset == 0 ? "" : $", {nameof(ProductRevision)}:{ProductRevision}")}, {nameof(SerialNumberOffset)}:{SerialNumberOffset}{(SerialNumberOffset == 0 ? "" : $", {nameof(SerialNumber)}:{SerialNumber}")}, {nameof(BusType)}:{BusType}, {nameof(RawPropertiesLength)}:{RawPropertiesLength}, {nameof(_RawDeviceProperties)}:[{string.Join(" ", (_RawDeviceProperties ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}}}";
        }
    }
}
