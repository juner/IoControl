using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// _STORAGE_DEVICE_DESCRIPTOR structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_device_descriptor )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageDeviceDescriptor
    {
        public readonly uint Version;
        public readonly uint Size;
        public readonly byte DeviceType;
        public readonly byte DeviceTypeModifier;
        public readonly bool RemovableMedia;
        public readonly bool CommandQueueing;
        public readonly uint VendorIdOffset;
        public readonly uint ProductIdOffset;
        public readonly uint ProductRevisionOffset;
        public readonly uint SerialNumberOffset;
        public readonly StorageBusType BusType;
        public readonly uint RawPropertiesLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public readonly byte[] RawDeviceProperties;

        public StorageDeviceDescriptor(uint Version, uint Size, byte DeviceType, byte DeviceTypeModifier, bool RemovableMedia, bool CommandQueueing, uint VendorIdOffset, uint ProductIdOffset, uint ProductRevisionOffset, uint SerialNumberOffset, StorageBusType BusType, uint RawPropertiesLength, byte[] RawDeviceProperties)
            => (this.Version, this.Size, this.DeviceType, this.DeviceTypeModifier, this.RemovableMedia, this.CommandQueueing, this.VendorIdOffset, this.ProductIdOffset, this.ProductRevisionOffset, this.SerialNumberOffset, this.BusType, this.RawPropertiesLength, this.RawDeviceProperties)
                = (Version, Size, DeviceType, DeviceTypeModifier, RemovableMedia, CommandQueueing, VendorIdOffset, ProductIdOffset, ProductRevisionOffset, SerialNumberOffset, BusType, RawPropertiesLength, RawDeviceProperties);

        public void Deconstruct(out uint Version, out uint Size, out byte DeviceType, out byte DeviceTypeModifier, out bool RemovableMedia, out bool CommandQueueing, out uint VendorIdOffset, out uint ProductIdOffset, out uint ProductRevisionOffset, out uint SerialNumberOffset, out StorageBusType BusType, out uint RawPropertiesLength, out byte[] RawDeviceProperties)
            => (Version, Size, DeviceType, DeviceTypeModifier, RemovableMedia, CommandQueueing, VendorIdOffset, ProductIdOffset, ProductRevisionOffset, SerialNumberOffset, BusType, RawPropertiesLength, RawDeviceProperties)
            = (this.Version, this.Size, this.DeviceType, this.DeviceTypeModifier, this.RemovableMedia, this.CommandQueueing, this.VendorIdOffset, this.ProductIdOffset, this.ProductRevisionOffset, this.SerialNumberOffset, this.BusType, this.RawPropertiesLength, this.RawDeviceProperties);
        public override string ToString()
            => $"{nameof(StorageDeviceDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(DeviceType)}:{DeviceType}, {nameof(DeviceTypeModifier)}:{DeviceTypeModifier}, {nameof(RemovableMedia)}:{RemovableMedia}, {nameof(CommandQueueing)}:{CommandQueueing}, {nameof(VendorIdOffset)}:{VendorIdOffset}, {nameof(ProductIdOffset)}:{ProductIdOffset},{nameof(ProductRevisionOffset)}:{ProductRevisionOffset}, {nameof(SerialNumberOffset)}:{SerialNumberOffset},{nameof(BusType)}:{BusType}, {nameof(RawPropertiesLength)}:{RawPropertiesLength}, {nameof(RawDeviceProperties)}:[{string.Join(" ",(RawDeviceProperties ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}}}";
    }
}
