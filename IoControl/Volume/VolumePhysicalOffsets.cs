using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    /// <summary>
    /// VOLUME_PHYSICAL_OFFSETS structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ns-ntddvol-_volume_physical_offsets )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumePhysicalOffsets
    {
        /// <summary>
        /// Contains the number of physical offsets returned by the call to <see cref="IOControlCode.VolumeLogicalToPhysical"/>.
        /// </summary>
        public uint NumberOfPhysicalOffsets;
        /// <summary>
        /// Contains an array of structures of type <see cref="VolumePhysicalOffset"/>. Each element of the array contains a pair consisting of a physical disk number and an accompanying physical offset &lt;disk number, disk offset&gt;.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public VolumePhysicalOffset[] PhysicalOffset;
    }
}
