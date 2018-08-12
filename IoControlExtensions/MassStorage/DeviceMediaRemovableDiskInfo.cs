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
    }
}
