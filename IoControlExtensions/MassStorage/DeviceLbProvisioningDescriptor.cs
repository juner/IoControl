using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{

    /// <summary>
    /// DEVICE_LB_PROVISIONING_DESCRIPTOR structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_device_lb_provisioning_descriptor
    /// The DEVICE_LB_PROVISIONING_DESCRIPTOR structure is one of the query result structures returned from an IOCTL_STORAGE_QUERY_PROPERTY request. This structure contains the thin provisioning capabilities for a storage device.
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceLbProvisioningDescriptor 
        : IStorageDescriptor
    {
        uint IStorageDescriptor.Size => Size;
        uint IStorageDescriptor.Version => Version;
        /// <summary>
        /// The version of this structure.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// The size of this structure. This is set to sizeof(<see cref="DeviceLbProvisioningDescriptor"/>).
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// The thin provisioning–enabled status.
        /// </summary>
        /// <remarks>
        /// 0: Thin provisioning is disabled. 
        /// 1: Thin provisioning is enabled. 
        /// </remarks>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool ThinProvisioningEnabled;
        /// <summary>
        /// Reads to unmapped regions return zeros.
        /// </summary>
        /// <remarks>
        /// 0: Data read from unmapped regions is undefined. 
        /// 1: Reads return zeros. 
        /// </remarks>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool ThinProvisioningReadZeros;
        /// <summary>
        /// Support for the anchored LBA mapping state.
        /// </summary>
        /// <remarks>
        /// 0: The anchored LBA mapping state is not supported. 
        /// 1: The anchored LBA mapping state is supported. 
        /// </remarks>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool AnchorSupported;
        /// <summary>
        /// The validity of unmap granularity alignment for the device.
        /// </summary>
        /// <remarks>
        /// 0: Unmap granularity alignment is not valid. 
        /// 1: Unmap granularity alignment is valid. 
        /// </remarks>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool UnmapGranularityAlignmentValid;
        readonly byte Reserved0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        readonly byte[] Reserved1;
        /// <summary>
        /// The optimal number of blocks for unmap granularity for the device.
        /// </summary>
        public readonly ulong OptimalUnmapGranularity;
        /// <summary>
        /// The current value, in blocks, set for unmap granularity alignment on the device. The value <see cref="UnmapGranularityAlignmentValid"/> indicates the validity of this member.
        /// </summary>
        public readonly ulong UnmapGranularityAlignment;
        /// <summary>
        /// Maximum amount of LBAs that can be unmapped in a single UNMAP command, in units of logical blocks. This is valid only in Windows 10 and above.
        /// </summary>
        public readonly uint MaxUnmapLbaCount;
        /// <summary>
        /// Maximum number of descriptors allowed in a single UNMAP command. This is valid only in Windows 10 and above.
        /// </summary>
        public readonly uint MaxUnmapBlockDescriptorCount;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ThinProvisioningEnabled"></param>
        /// <param name="ThinProvisioningReadZeros"></param>
        /// <param name="AnchorSupported"></param>
        /// <param name="UnmapGranularityAlignmentValid"></param>
        /// <param name="Reserved0"></param>
        /// <param name="Reserved1"></param>
        /// <param name="OptimalUnmapGranularity"></param>
        /// <param name="UnmapGranularityAlignment"></param>
        /// <param name="MaxUnmapLbaCount"></param>
        /// <param name="MaxUnmapBlockDescriptorCount"></param>
        public DeviceLbProvisioningDescriptor(bool ThinProvisioningEnabled, bool ThinProvisioningReadZeros, bool AnchorSupported, bool UnmapGranularityAlignmentValid, byte Reserved0, byte[] Reserved1, ulong OptimalUnmapGranularity, ulong UnmapGranularityAlignment, uint MaxUnmapLbaCount, uint MaxUnmapBlockDescriptorCount)
            => (this.Version, this.Size, this.ThinProvisioningEnabled, this.ThinProvisioningReadZeros, this.AnchorSupported, this.UnmapGranularityAlignmentValid, this.Reserved0, this.Reserved1, this.OptimalUnmapGranularity, this.UnmapGranularityAlignment, this.MaxUnmapLbaCount, this.MaxUnmapBlockDescriptorCount)
            = ((uint)Marshal.SizeOf<DeviceLbProvisioningDescriptor>(), (uint)Marshal.SizeOf<DeviceLbProvisioningDescriptor>(), ThinProvisioningEnabled, ThinProvisioningReadZeros, AnchorSupported, UnmapGranularityAlignmentValid, Reserved0, Reserved1, OptimalUnmapGranularity, UnmapGranularityAlignment, MaxUnmapLbaCount, MaxUnmapBlockDescriptorCount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Size"></param>
        /// <param name="ThinProvisioningEnabled"></param>
        /// <param name="ThinProvisioningReadZeros"></param>
        /// <param name="AnchorSupported"></param>
        /// <param name="UnmapGranularityAlignmentValid"></param>
        /// <param name="OptimalUnmapGranularity"></param>
        /// <param name="UnmapGranularityAlignment"></param>
        /// <param name="MaxUnmapLbaCount"></param>
        /// <param name="MaxUnmapBlockDescriptorCount"></param>
        public DeviceLbProvisioningDescriptor(uint Version, uint Size, bool ThinProvisioningEnabled, bool ThinProvisioningReadZeros, bool AnchorSupported, bool UnmapGranularityAlignmentValid, ulong OptimalUnmapGranularity, ulong UnmapGranularityAlignment, uint MaxUnmapLbaCount, uint MaxUnmapBlockDescriptorCount) 
            : this(ThinProvisioningEnabled, ThinProvisioningReadZeros, AnchorSupported, UnmapGranularityAlignmentValid, 0, new byte[7], OptimalUnmapGranularity, UnmapGranularityAlignment, MaxUnmapLbaCount, MaxUnmapBlockDescriptorCount) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public DeviceLbProvisioningDescriptor(IntPtr IntPtr, uint Size) => this = (DeviceLbProvisioningDescriptor)Marshal.PtrToStructure(IntPtr, typeof(DeviceLbProvisioningDescriptor));
    }
}
