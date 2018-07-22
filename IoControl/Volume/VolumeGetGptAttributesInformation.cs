using System.Runtime.InteropServices;
using IoControl.Disk;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct VolumeGetGptAttributesInformation
    {
        /// <summary>
        /// Specifies all the attributes associated with this volume.
        /// </summary>
        public readonly Disk.EFIPartitionAttributes GptAttributes;

        public VolumeGetGptAttributesInformation(EFIPartitionAttributes GptAttributes) => this.GptAttributes = GptAttributes;
    }
}
