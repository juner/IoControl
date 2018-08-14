using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_DEVICE_LAYOUT_SIGNATURE
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/storduid/ns-storduid-_storage_device_layout_signature
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageDeviceLayoutSignature : IStorageDescriptor
    {
        uint IStorageDescriptor.Version => Version;
        uint IStorageDescriptor.Size => Size;
        /// <summary>
        /// The version of the DUID.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// The size, in bytes, of this STORAGE_DEVICE_LAYOUT_SIGNATURE structure.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// A Boolean value that indicates whether the partition table of the disk is formatted with a master boot record (MBR). If TRUE, the partition table of the disk is formatted with a master boot record (MBR). If FALSE, the disk has a GUID partition table (GPT).
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool Mbr;
        private readonly StorageDeviceLayoutSignatureDeviceSpecific DeviceSpecific;
        /// <summary>
        /// The signature value, which uniquely identifies the disk.
        /// </summary>
        public uint MbrSignature => DeviceSpecific.MbrSignature;
        /// <summary>
        /// The GUID that uniquely identifies the disk.
        /// </summary>
        public Guid GptDiskId => DeviceSpecific.GptDiskId;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mbr"></param>
        /// <param name="DeviceSpecific"></param>
        private StorageDeviceLayoutSignature(bool Mbr, StorageDeviceLayoutSignatureDeviceSpecific DeviceSpecific)
            => (Version, Size, this.Mbr, this.DeviceSpecific) = ((uint)Marshal.SizeOf<StorageDeviceLayoutSignature>(), (uint)Marshal.SizeOf<StorageDeviceLayoutSignature>(), Mbr, DeviceSpecific);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MbrSignature"></param>
        public StorageDeviceLayoutSignature(uint MbrSignature) : this(true, MbrSignature) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="GptDiskId"></param>
        public StorageDeviceLayoutSignature(Guid GptDiskId) : this(false, GptDiskId) { }
        public StorageDeviceLayoutSignature(IntPtr IntPtr, uint Size) => this = (StorageDeviceLayoutSignature)Marshal.PtrToStructure(IntPtr, typeof(StorageDeviceLayoutSignature));
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(StorageDeviceLayoutSignature)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(Mbr)}:{Mbr}{(Mbr ? $", {nameof(MbrSignature)}:{MbrSignature}" : $"{nameof(GptDiskId)}:{GptDiskId}")}}}";
        [StructLayout(LayoutKind.Explicit)]
        private struct StorageDeviceLayoutSignatureDeviceSpecific
        {
            /// <summary>
            /// 
            /// </summary>
            [FieldOffset(0)]
            public readonly uint MbrSignature;
            /// <summary>
            /// 
            /// </summary>
            [FieldOffset(0)]
            public readonly Guid GptDiskId;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="MbrSignature"></param>
            public StorageDeviceLayoutSignatureDeviceSpecific(uint MbrSignature) : this() => this.MbrSignature = MbrSignature;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="GptDiskId"></param>
            public StorageDeviceLayoutSignatureDeviceSpecific(Guid GptDiskId) : this() => this.GptDiskId = GptDiskId;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="self"></param>
            public static implicit operator uint(in StorageDeviceLayoutSignatureDeviceSpecific self)=> self.MbrSignature;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="MbrSignature"></param>
            public static implicit operator StorageDeviceLayoutSignatureDeviceSpecific(uint MbrSignature) => new StorageDeviceLayoutSignatureDeviceSpecific(MbrSignature);
            /// <summary>
            /// 
            /// </summary>
            /// <param name="self"></param>
            public static implicit operator Guid(in StorageDeviceLayoutSignatureDeviceSpecific self) => self.GptDiskId;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="GptDiskId"></param>
            public static implicit operator StorageDeviceLayoutSignatureDeviceSpecific(in Guid GptDiskId) => new StorageDeviceLayoutSignatureDeviceSpecific(GptDiskId);

        }
    }
}