using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumePhysicalOffset
    {
        /// <summary>
        /// Specifies the physical location that needs to be  translated to a logical location.
        /// </summary>
        public uint DiskNumber;

        public long Offset;
        public VolumePhysicalOffset(uint DiskNumber, long Offset)
            => (this.DiskNumber, this.Offset) = (DiskNumber, Offset);
        public void Deconstruct(out uint DiskNumber, out long Offset)
            => (DiskNumber, Offset) = (this.DiskNumber, this.Offset);
        public override string ToString()
            => $"{nameof(VolumePhysicalOffset)}{{{nameof(DiskNumber)}:{DiskNumber}, {nameof(Offset)}:{Offset}}}";
    }
}
