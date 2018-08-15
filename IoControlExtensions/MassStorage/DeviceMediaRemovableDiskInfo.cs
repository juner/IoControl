using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceMediaRemovableDiskInfo
    {
        public readonly long Cylinders;
        public readonly StorageMediaType MediaType;
        public readonly uint TracksPerCylinder;
        public readonly uint SectorsPerTrack;
        public readonly uint BytesPerSector;
        public readonly uint NumberMediaSides;
        public readonly MediaFlags MediaCharacteristics;
        public DeviceMediaRemovableDiskInfo(long Cylinders, StorageMediaType MediaType, uint TracksPerCylinder, uint SectorsPerTrack, uint BytesPerSector, uint NumberMediaSides, MediaFlags MediaCharacteristics)
            => (this.Cylinders, this.MediaType, this.TracksPerCylinder, this.SectorsPerTrack, this.BytesPerSector, this.NumberMediaSides, this.MediaCharacteristics)
            = (Cylinders, MediaType, TracksPerCylinder, SectorsPerTrack, BytesPerSector, NumberMediaSides, MediaCharacteristics);
        public DeviceMediaRemovableDiskInfo Set(long? Cylinders, StorageMediaType? MediaType, uint? TracksPerCylinder, uint? SectorsPerTrack, uint? BytesPerSector, uint? NumberMediaSides, MediaFlags? MediaCharacteristics)
            => Cylinders == null && MediaType == null && TracksPerCylinder == null && SectorsPerTrack == null && BytesPerSector == null && NumberMediaSides == null && MediaCharacteristics == null ? this
            : new DeviceMediaRemovableDiskInfo(Cylinders ?? this.Cylinders, MediaType ?? this.MediaType, TracksPerCylinder ?? this.TracksPerCylinder, SectorsPerTrack ?? this.SectorsPerTrack, BytesPerSector ?? this.BytesPerSector, NumberMediaSides ?? this.NumberMediaSides, MediaCharacteristics ?? this.MediaCharacteristics);
        public override string ToString()
            => $"{nameof(DeviceMediaRemovableDiskInfo)}{{{nameof(Cylinders)}:{Cylinders}, {nameof(MediaType)}:{MediaType}, {nameof(TracksPerCylinder)}:{TracksPerCylinder}, {nameof(SectorsPerTrack)}:{SectorsPerTrack}, {nameof(BytesPerSector)}:{BytesPerSector}, {nameof(NumberMediaSides)}:{NumberMediaSides}, {nameof(MediaCharacteristics)}:{MediaCharacteristics}}}";
    }
}
