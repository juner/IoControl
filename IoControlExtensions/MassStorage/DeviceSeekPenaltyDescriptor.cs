using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// DEVICE_SEEK_PENALTY_DESCRIPTOR structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_device_seek_penalty_descriptor
    /// The <see cref="DeviceSeekPenaltyDescriptor"/> structure is used in conjunction with the <see cref="IOControlCode.StorageQueryProperty"/> request to retrieve the seek penalty descriptor data for a device.
    /// </summary>
    /// <remarks>
    /// Storage class drivers issue a device-control request with the I/O control code <see cref="IOControlCode.StorageQueryProperty"/> to retrieve this structure, which contains seek penalty information for data transfer operations. The structure can be retrieved either from the device object for the bus or from an FDO, which forwards the request to the underlying bus.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceSeekPenaltyDescriptor : IStorageDescriptor
    {
        uint IStorageDescriptor.Size => Size;
        uint IStorageDescriptor.Version => Version;
        /// <summary>
        /// Contains the size of the structure <see cref="DeviceSeekPenaltyDescriptor"/>. The value of this member will change as members are added to the structure.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Specifies the total size of the descriptor, in bytes.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// Specifies whether the device incurs a seek penalty.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IncursSeekPenalty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Size"></param>
        /// <param name="IncursSeekPenalty"></param>
        public DeviceSeekPenaltyDescriptor(uint Version, uint Size, bool IncursSeekPenalty)
            => (this.Version, this.Size, this.IncursSeekPenalty) = (Version, Size, IncursSeekPenalty);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Size"></param>
        /// <param name="IncursSeekPenalty"></param>
        public void Deconstruct(out uint Version, out uint Size, out bool IncursSeekPenalty)
            => (Version, Size, IncursSeekPenalty) = (this.Version, this.Size, this.IncursSeekPenalty);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public DeviceSeekPenaltyDescriptor(IntPtr IntPtr, uint Size) => this = (DeviceSeekPenaltyDescriptor)Marshal.PtrToStructure(IntPtr, typeof(DeviceSeekPenaltyDescriptor));
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DeviceSeekPenaltyDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(IncursSeekPenalty)}:{IncursSeekPenalty}}}";
    }
}
