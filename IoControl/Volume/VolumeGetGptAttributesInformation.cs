using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct VolumeGetGptAttributesInformation
    {
        /// <summary>
        /// Specifies all the attributes associated with this volume.
        /// </summary>
        public Disk.EFIPartitionAttributes GptAttributes;
    }
}
