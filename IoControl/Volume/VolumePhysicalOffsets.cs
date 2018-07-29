using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    /// <summary>
    /// VOLUME_PHYSICAL_OFFSETS structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ns-ntddvol-_volume_physical_offsets )
    /// The <see cref="VolumePhysicalOffsets"/> structure contains an array of physical offsets and accompanying physical disk numbers and is used with <see cref="IOControlCode.VolumeLogicalToPhysical"/> to request a series of pairs of physical offsets and disk numbers that correspond to a single logical offset.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct VolumePhysicalOffsets
    {
        /// <summary>
        /// Contains the number of physical offsets returned by the call to <see cref="IOControlCode.VolumeLogicalToPhysical"/>.
        /// </summary>
        public readonly uint NumberOfPhysicalOffsets;
        /// <summary>
        /// Contains an array of structures of type <see cref="VolumePhysicalOffset"/>. Each element of the array contains a pair consisting of a physical disk number and an accompanying physical offset &lt;disk number, disk offset&gt;.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        internal readonly VolumePhysicalOffset[] _PhysicalOffset;
        /// <summary>
        /// 
        /// </summary>
        public VolumePhysicalOffset[] PhysicalOffset => _PhysicalOffset.Take((int)NumberOfPhysicalOffsets).ToArray();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PhysicalOffset"></param>
        public VolumePhysicalOffsets(VolumePhysicalOffset[] PhysicalOffset)
            => (NumberOfPhysicalOffsets, this._PhysicalOffset) = ((uint)((PhysicalOffset?.Length ?? 0) == 0 ? 0 : PhysicalOffset.Length), (PhysicalOffset?.Length ?? 0) == 0 ? new VolumePhysicalOffset[1] : PhysicalOffset);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offsets"></param>
        public static implicit operator VolumePhysicalOffset[](in VolumePhysicalOffsets offsets) => offsets._PhysicalOffset.Take((int)offsets.NumberOfPhysicalOffsets).ToArray();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offsets"></param>
        public static implicit operator VolumePhysicalOffsets(VolumePhysicalOffset[] offsets) => new VolumePhysicalOffsets(offsets);
        public override string ToString()
            => $"{nameof(VolumePhysicalOffsets)}{{{nameof(NumberOfPhysicalOffsets)}:{NumberOfPhysicalOffsets}, {nameof(PhysicalOffset)}[{string.Join(", ", _PhysicalOffset.Take((int)NumberOfPhysicalOffsets).Select(v => $"{v}")) }]}}";
    }
}
