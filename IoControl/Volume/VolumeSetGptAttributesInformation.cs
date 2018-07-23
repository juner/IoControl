using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumeSetGptAttributesInformation
    {
        /// <summary>
        /// Specifies  the  attributes that are to be applied to the volume
        /// </summary>
        public Disk.EFIPartitionAttributes GptAttributes;
        /// <summary>
        /// Indicates whether  this is to be undone when the handle is closed
        /// </summary>
        public bool RevertOnClose;
        /// <summary>
        /// Indicates  whether the  attributes apply  to all the  volumes  on the disk that  this  volume resides on Required if the disk layout is MBR
        /// </summary>
        public bool ApplyToAllConnectedVolumes;
        /// <summary>
        /// For alignment purposes.
        /// </summary>
        public ushort Reserved1;
        /// <summary>
        /// For alignment purposes.
        /// </summary>
        public uint Reserved2;
        public void Deconstruct(out Disk.EFIPartitionAttributes GptAttributes, out bool RevertOnClose, out bool ApplyToAllConnectedVolumes)
            => (GptAttributes, RevertOnClose, ApplyToAllConnectedVolumes) = (this.GptAttributes, this.RevertOnClose, this.ApplyToAllConnectedVolumes);
        public void Deconstruct(out Disk.EFIPartitionAttributes GptAttributes, out bool RevertOnClose, out bool ApplyToAllConnectedVolumes, out ushort Reserved1, out uint Reserved2)
            => (GptAttributes, RevertOnClose, ApplyToAllConnectedVolumes, Reserved1, Reserved2) = (this.GptAttributes, this.RevertOnClose, this.ApplyToAllConnectedVolumes, this.Reserved1, this.Reserved2);
    }
}
