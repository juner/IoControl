using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// DEVICE_COPY_OFFLOAD_DESCRIPTOR structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_device_copy_offload_descriptor )
    /// </summary>
    public readonly struct DeviceCopyOffloadDescriptor
    {
        public readonly uint Version;
        public readonly uint Size;
        public readonly uint MaximumTokenLifetime;
        public readonly uint DefaultTokenLifetime;
        public readonly ulong MaximumTransferSize;
        public readonly ulong OptimalTransferCount;
        public readonly uint MaximumDataDescriptors;
        public readonly uint MaximumTransferLengthPerDescriptor;
        public readonly uint OptimalTransferLengthPerDescriptor;
        public readonly ushort OptimalTransferLengthGranularity;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =2)]
        public readonly byte[] Reserved;
    }
}
