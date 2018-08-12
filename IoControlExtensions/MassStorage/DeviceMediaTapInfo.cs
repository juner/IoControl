using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceMediaTapInfo
    {
        public readonly StorageMediaType MediaType;
        public readonly MediaFlags MediaCharacteristics;
        public readonly uint CurrentBlockSize;
        public readonly StorageBusType BusType;
        public readonly DeviceMediaBusSpecificData BusSpecificData;
    }
}
