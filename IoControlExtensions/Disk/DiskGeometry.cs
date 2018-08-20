using System;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_GEOMETRY structure ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_disk_geometry )
    /// Describes the geometry of disk devices and media.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DiskGeometry
    {
        /// <summary>
        /// The number of cylinders. See LARGE_INTEGER.
        /// </summary>
        public readonly long Cylinders;
        /// <summary>
        /// The type of media. For a list of values, see <see cref="MediaType"/>.
        /// </summary>
        public readonly MediaType MediaType;
        /// <summary>
        /// The number of tracks per cylinder.
        /// </summary>
        public readonly uint TrackPerCylinder;
        /// <summary>
        /// The number of sectors per track.
        /// </summary>
        public readonly uint SectorsPerTrack;
        /// <summary>
        /// The number of bytes per sector.
        /// </summary>
        public readonly uint BytesPerSector;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cylinders"></param>
        /// <param name="MediaType"></param>
        /// <param name="TrackPerCylinder"></param>
        /// <param name="SectorsPerTrack"></param>
        /// <param name="BytesPerSector"></param>
        public DiskGeometry(long Cylinders, MediaType MediaType, uint TrackPerCylinder, uint SectorsPerTrack, uint BytesPerSector)
            => (this.Cylinders, this.MediaType, this.TrackPerCylinder, this.SectorsPerTrack, this.BytesPerSector)
            = (Cylinders, MediaType, TrackPerCylinder, SectorsPerTrack, BytesPerSector);
        /// <summary>
        /// ptr to structure
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public DiskGeometry(IntPtr IntPtr, uint Size) => this = (DiskGeometry)Marshal.PtrToStructure(IntPtr, typeof(DiskGeometry));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cylinders"></param>
        /// <param name="MediaType"></param>
        /// <param name="TrackPerCylinder"></param>
        /// <param name="SectorsPerTrack"></param>
        /// <param name="BytesPerSector"></param>
        public void Deconstruct(out long Cylinders, out MediaType MediaType, out uint TrackPerCylinder, out uint SectorsPerTrack, out uint BytesPerSector)
            => (Cylinders, MediaType, TrackPerCylinder, SectorsPerTrack, BytesPerSector)
            = (this.Cylinders, this.MediaType, this.TrackPerCylinder, this.SectorsPerTrack, this.BytesPerSector);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DiskGeometry)}{{{nameof(Cylinders)}:{Cylinders}, {nameof(MediaType)}:{MediaType}, {nameof(TrackPerCylinder)}:{TrackPerCylinder}, {nameof(SectorsPerTrack)}:{SectorsPerTrack}, {nameof(BytesPerSector)}:{BytesPerSector}}}";
    }
}
