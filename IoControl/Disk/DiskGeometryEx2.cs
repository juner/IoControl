using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_GEOMETRY_EX structure( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_disk_geometry_ex )
    /// The <see cref="DiskGeometryEx"/> structure is a variable-length structure composed of a <see cref="DiskGeometry"/> structure followed by a <see cref="DiskPartitionInfo"/> structure followed, in turn, by a <see cref="DiskDetectionInfo"/> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DiskGeometryEx2 {
        /// <summary>
        /// See <see cref="DiskGeometry"/> for a description of this member.
        /// </summary>
        public readonly DiskGeometry Geometry;
        /// <summary>
        /// Contains the size in bytes of the disk.
        /// </summary>
        public readonly long DiskSize;
        /// <summary>
        /// <see cref="DiskPartitionInfo"/>
        /// </summary>
        public readonly DiskPartitionInfo PartitionInfo;
        /// <summary>
        /// <see cref="DiskDetectionInfo"/>
        /// </summary>
        public readonly DiskDetectionInfo DetectionInfo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Geometry"></param>
        /// <param name="DiskSize"></param>
        /// <param name="PartitionInfo"></param>
        /// <param name="DetectionInfo"></param>
        public DiskGeometryEx2(DiskGeometry Geometry, long DiskSize, DiskPartitionInfo PartitionInfo, DiskDetectionInfo DetectionInfo)
            => (this.Geometry, this.DiskSize, this.PartitionInfo, this.DetectionInfo)
            = (Geometry, DiskSize, PartitionInfo, DetectionInfo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Geometry"></param>
        /// <param name="DiskSize"></param>
        /// <param name="PartitionInfo"></param>
        /// <param name="DetectionInfo"></param>
        public void Deconstruct(out DiskGeometry Geometry, out long DiskSize, out DiskPartitionInfo PartitionInfo, out DiskDetectionInfo DetectionInfo)
            => (Geometry, DiskSize, PartitionInfo, DetectionInfo)
            = (this.Geometry, this.DiskSize, this.PartitionInfo, this.DetectionInfo);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DiskGeometryEx2)}{{{nameof(Geometry)}:{Geometry}, {nameof(DiskSize)}:{DiskSize}, {nameof(PartitionInfo)}:{PartitionInfo}, {nameof(DetectionInfo)}:{DetectionInfo}}}";
    }
}
