using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// DEVICE_SEEK_PENALTY_DESCRIPTOR structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_device_seek_penalty_descriptor
    /// The <see cref="DeviceSeekPenaltyDescriptor"/> structure is used in conjunction with the <see cref="IOControlCode.StorageQueryProperty"/> request to retrieve the seek penalty descriptor data for a device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceSeekPenaltyDescriptor
    {
        public readonly uint Version;
        public readonly uint Size;
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IncursSeekPenalty;
        public DeviceSeekPenaltyDescriptor(uint Version, uint Size, bool IncursSeekPenalty)
            => (this.Version, this.Size, this.IncursSeekPenalty) = (Version, Size, IncursSeekPenalty);
        public void Deconstruct(out uint Version, out uint Size, out bool IncursSeekPenalty)
            => (Version, Size, IncursSeekPenalty) = (this.Version, this.Size, this.IncursSeekPenalty);
        public override string ToString()
            => $"{nameof(DeviceSeekPenaltyDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(IncursSeekPenalty)}:{IncursSeekPenalty}}}";
    }
}
