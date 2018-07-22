using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskGeometry
    {
        public long Cylinders;
        public MediaType MediaType;
        public uint TrackPerCylinder;
        public uint SectorsPerTrack;
        public uint BytesPerSector;
        public override string ToString()
            => $"{nameof(DiskGeometry)}{{{nameof(Cylinders)}:{Cylinders}, {nameof(MediaType)}:{MediaType}, {nameof(TrackPerCylinder)}:{TrackPerCylinder}, {nameof(SectorsPerTrack)}:{SectorsPerTrack}, {nameof(BytesPerSector)}:{BytesPerSector}}}";
    }
}
